using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataManager
{
    // ⭐ 중요: Form1 클래스가 파일 최상단에 위치해야 디자이너 에러가 방지됩니다.
    public partial class Form1 : Form
    {
        // [필드 변수 선언]
        private List<DrivingData> _allData = new List<DrivingData>();
        private List<DrivingData> _deletedDataBuffer = new List<DrivingData>(); // 복구용 버퍼
        private int _currentIndex = -1;
        private bool _isReversed = false;
        private bool _isRangeSettingMode = false;
        private readonly List<Panel> _imageRangeMarkers = new List<Panel>();
        private System.Windows.Forms.Timer _playTimer = new System.Windows.Forms.Timer();


        public Form1()
        {
            InitializeComponent();

            // 1. 트랙바 범위 설정 (Value 대입보다 먼저 실행되어야 에러 안 남)
            tbPlaybackSpeed.Minimum = 25;
            tbPlaybackSpeed.Maximum = 200;
            tbPlaybackSpeed.Value = 100;

            InitializeDataInfoGrid();
            UpdatePlaybackSpeedLabel();

            _playTimer.Tick += PlayTimer_Tick;
            this.Shown += Form1_Shown;

            lvDataItems.SelectedIndexChanged += lvDataItems_SelectedIndexChanged;
        }

        #region [1. 초기화 및 차트 안전 장치]

        private void Form1_Shown(object? sender, EventArgs e)
        {
            dgvDataInfo.ClearSelection();
            _isRangeSettingMode = false;
            pnlImageRangeMarker.Visible = false;

            // 차트 영역 공간 확보용 스크롤바 허용
            this.AutoScroll = true;

            // 📍 하단 빈 공간 절대 좌표 및 배치 규격 설정
            int chartStartX = 20;
            int chartWidth = 460;
            int chartHeight = 200;
            int row1Y = 640;
            int col2X = 510;

            // 🛠️ 코드로 하드코딩 레이아웃 정렬
            // 1. [chtSteeringValue] - 이미지 번호에 따른 조향값 차트
            if (chtSteeringValue == null)
            {
                chtSteeringValue = new Chart();
                chtSteeringValue.Location = new Point(chartStartX, row1Y);
                chtSteeringValue.Size = new Size(chartWidth, chartHeight);
                chtSteeringValue.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                this.Controls.Add(chtSteeringValue);
            }

            // 2. [chtSpeedValue] - 이미지 번호에 따른 속도값 차트
            if (chtSpeedValue == null)
            {
                chtSpeedValue = new Chart();
                chtSpeedValue.Location = new Point(col2X, row1Y);
                chtSpeedValue.Size = new Size(chartWidth, chartHeight);
                chtSpeedValue.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                this.Controls.Add(chtSpeedValue);
            }

            // 필수 차트 2개 시리즈 플롯 커스텀 빌드
            SetupSafeChart(chtSteeringValue, "Steering", Color.DodgerBlue);
            SetupSafeChart(chtSpeedValue, "Speed", Color.OrangeRed);

            // 초기 기본 더미 포인트 배치로 형태 확보
            if (chtSteeringValue.Series[0].Points.Count == 0) chtSteeringValue.Series[0].Points.AddXY(0, 0);
            if (chtSpeedValue.Series[0].Points.Count == 0) chtSpeedValue.Series[0].Points.AddXY(0, 0);
        }

        private void SetupSafeChart(Chart? chart, string s1Name, Color c1, string? s2Name = null, Color? c2 = null)
        {
            if (chart == null) return;

            ChartArea ca = chart.ChartAreas.Count > 0 ? chart.ChartAreas[0] : chart.ChartAreas.Add("Main");
            ca.AxisX.Title = s1Name;
            string areaName = ca.Name;

            // 차트 최상단 메인 타이틀 명시
            chart.Titles.Clear();
            var mainTitle = chart.Titles.Add($"[{s1Name} Data Graph]");
            mainTitle.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
            mainTitle.ForeColor = Color.FromArgb(50, 50, 50);

            chart.Series.Clear();

            var s1 = chart.Series.Add(s1Name);
            s1.ChartArea = areaName;
            s1.ChartType = SeriesChartType.Line;
            s1.Color = c1;
            s1.BorderWidth = 2;

            if (s2Name != null)
            {
                var s2 = chart.Series.Add(s2Name);
                s2.ChartArea = areaName;
                s2.ChartType = SeriesChartType.Line;
                s2.Color = c2 ?? Color.Green;
                s2.BorderWidth = 2;
            }

            chart.Visible = true;
            chart.BringToFront();
        }

        #endregion

        #region [2. 데이터 로드 및 무결성 검사]

        private void btnSelectAdd_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = fbd.SelectedPath;
                    LoadData(fbd.SelectedPath);
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("shlwapi.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

        private void LoadData(string path)
        {
            var files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories).ToList();
            if (files.Count == 0)
            {
                MessageBox.Show("선택한 경로 또는 하위 폴더 내에 이미지(*.jpg) 파일이 존재하지 않습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            files.Sort((x, y) => StrCmpLogicalW(x, y));
            _allData.Clear();

            lvDataItems.Items.Clear();
            lvDataItems.Columns.Clear();
            lvDataItems.View = View.Details;
            lvDataItems.FullRowSelect = true;
            lvDataItems.GridLines = true;

            lvDataItems.Columns.Add("No", 60, HorizontalAlignment.Center);
            lvDataItems.Columns.Add("파일명 (Image Name)", 200, HorizontalAlignment.Left);

            var catalogFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                        .Where(f => f.EndsWith(".csv") || f.EndsWith(".txt") || f.EndsWith(".json"))
                                        .ToList();

            for (int i = 0; i < files.Count; i++)
            {
                _allData.Add(new DrivingData
                {
                    Index = i,
                    ImagePath = files[i],
                    Steering = (new Random().NextDouble() * 2) - 1,
                    Speed = new Random().Next(20, 100)
                });

                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(Path.GetFileName(files[i]));
                lvDataItems.Items.Add(item);
            }

            _currentIndex = 0;
            tbImageNavigator.Maximum = _allData.Count - 1;
            UpdateDisplay();
            UpdateCharts();
        }

        private void btnCheckDataIntegrity_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0)
            {
                MessageBox.Show("검사할 데이터가 로드되지 않았습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isDup = _allData.GroupBy(x => x.Index).Any(g => g.Count() > 1);
            bool isMissingFile = _allData.Any(x => string.IsNullOrEmpty(x.ImagePath) || !File.Exists(x.ImagePath));
            bool isMissingValue = _allData.Any(x => x.Steering == 0.0 && x.Speed == 0.0);

            string reportMessage = $"[데이터 무결성 검사 결과]\n\n" +
                                   $"1. 인덱스 중복 발생: {(isDup ? "⚠️ 발견 (위험)" : "정상 (없음)")}\n" +
                                   $"2. 이미지 파일 누락 (카탈로그 내 존재): {(isMissingFile ? "⚠️ 발견 (유실 파일 있음)" : "정상 (없음)")}\n" +
                                   $"3. 데이터(Angle/Throttle) 누락: {(isMissingValue ? "⚠️ 발견 (값 없음)" : "정상 (없음)")}";

            var alertIcon = (isDup || isMissingFile || isMissingValue) ? MessageBoxIcon.Warning : MessageBoxIcon.Information;

            MessageBox.Show(reportMessage, "무결성 검사 완료", MessageBoxButtons.OK, alertIcon);
        }

        #endregion

        #region [3. 탐색 및 재생 제어]

        private void UpdateDisplay()
        {
            if (_currentIndex < 0 || _currentIndex >= _allData.Count) return;
            var data = _allData[_currentIndex];

            if (File.Exists(data.ImagePath))
            {
                using (var fs = new FileStream(data.ImagePath, FileMode.Open, FileAccess.Read))
                {
                    pbDataPreview.Image = Image.FromStream(fs);
                }
            }

            dgvDataInfo.Rows[0].Cells[1].Value = _allData.Count;
            dgvDataInfo.Rows[1].Cells[1].Value = _currentIndex;
            dgvDataInfo.Rows[2].Cells[1].Value = data.Steering.ToString("F2");
            dgvDataInfo.Rows[3].Cells[1].Value = data.Speed.ToString("F0");
            tbImageNavigator.Value = _currentIndex;
        }

        private void btnPlay_Click_1(object sender, EventArgs e) { _isReversed = false; StartPlayback(); }
        private void btnReverse_Click(object sender, EventArgs e) { _isReversed = true; StartPlayback(); }
        private void btnStop_Click(object sender, EventArgs e) { _playTimer.Stop(); }

        private void StartPlayback()
        {
            if (_allData.Count == 0) return;

            double speedRatio = tbPlaybackSpeed.Value / 100.0;
            _playTimer.Interval = (int)(150 / speedRatio);

            _playTimer.Start();
        }

        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isReversed) { if (_currentIndex > 0) _currentIndex--; else _playTimer.Stop(); }
            else { if (_currentIndex < _allData.Count - 1) _currentIndex++; else _playTimer.Stop(); }
            UpdateDisplay();
        }

        #endregion

        #region [4. 에러 대처 로직]

        private void gbDataContent_Resize(object sender, EventArgs e)
        {
            foreach (Panel marker in _imageRangeMarkers)
            {
                if (marker.Tag is int val)
                    marker.Left = GetImageNavigatorMarkerLeft(val, marker.Size);
            }
            if (_allData.Count > 0) UpdateCharts();
        }

        private void btnCancelDelete_Click(object sender, EventArgs e)
        {
            if (_deletedDataBuffer.Count > 0)
            {
                _allData.AddRange(_deletedDataBuffer);
                _allData = _allData.OrderBy(x => x.Index).ToList();
                _deletedDataBuffer.Clear();
                RefreshUI();
                MessageBox.Show("데이터가 복구되었습니다.");
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("python", "manage.py train");
                txtTrainingLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 학습 프로세스 시작...\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Python run error: " + ex.Message);
            }
        }

        #endregion

        #region [5. 필터링 및 삭제]

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var targets = _allData.Where(x => x.Steering == 0 || x.Speed == 0).ToList();
            if (targets.Count > 0)
            {
                _deletedDataBuffer = new List<DrivingData>(targets);
                _allData.RemoveAll(x => x.Steering == 0 || x.Speed == 0);
                RefreshUI();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0 || _currentIndex < 0) return;
            if (_imageRangeMarkers.Count == 2)
            {
                int s = (int)_imageRangeMarkers[0].Tag, f = (int)_imageRangeMarkers[1].Tag;
                int min = Math.Min(s, f), max = Math.Max(s, f);
                _deletedDataBuffer = _allData.GetRange(min, max - min + 1);
                _allData.RemoveRange(min, max - min + 1);
                ClearMarkers();
            }
            else
            {
                _deletedDataBuffer = new List<DrivingData> { _allData[_currentIndex] };
                _allData.RemoveAt(_currentIndex);
            }
            RefreshUI();
        }

        private void RefreshUI()
        {
            _currentIndex = Math.Max(0, Math.Min(_currentIndex, _allData.Count - 1));
            tbImageNavigator.Maximum = Math.Max(0, _allData.Count - 1);
            UpdateDisplay();
            UpdateCharts();
        }

        #endregion

        #region [6. 마커 및 유틸리티]

        private int GetImageNavigatorMarkerLeft(int value, Size markerSize)
        {
            int min = tbImageNavigator.Minimum, max = tbImageNavigator.Maximum;
            double ratio = (max == min) ? 0 : (double)(value - min) / (max - min);
            int markerCenterX = tbImageNavigator.Left + 10 + (int)((tbImageNavigator.Width - 20) * ratio);
            return markerCenterX - (markerSize.Width / 2);
        }

        private void tbImageNavigator_MouseUp(object sender, MouseEventArgs e)
        {
            _currentIndex = tbImageNavigator.Value;
            UpdateDisplay();
            if (_isRangeSettingMode) AddMarker();
        }

        private void AddMarker()
        {
            Panel m = new Panel { BackColor = Color.FromArgb(255, 114, 16), Size = pnlImageRangeMarker.Size, Tag = _currentIndex };
            m.Left = GetImageNavigatorMarkerLeft(_currentIndex, m.Size);
            m.Top = tbImageNavigator.Top + 13;
            _imageRangeMarkers.Add(m);
            if (_imageRangeMarkers.Count > 2) { gbDataContent.Controls.Remove(_imageRangeMarkers[0]); _imageRangeMarkers.RemoveAt(0); }
            gbDataContent.Controls.Add(m);
            m.BringToFront();
        }

        private void ClearMarkers() { foreach (var m in _imageRangeMarkers) gbDataContent.Controls.Remove(m); _imageRangeMarkers.Clear(); _isRangeSettingMode = false; }
        private void UpdatePlaybackSpeedLabel() { lblPlaybackSpeed.Text = $"x{tbPlaybackSpeed.Value / 100.0:0.##}"; }
        private void tbPlaybackSpeed_Scroll(object sender, EventArgs e)
        {
            UpdatePlaybackSpeedLabel();

            if (_playTimer.Enabled)
            {
                double speedRatio = tbPlaybackSpeed.Value / 100.0;
                _playTimer.Interval = (int)(150 / speedRatio);
            }
        }
        private void InitializeDataInfoGrid() { dgvDataInfo.Rows.Clear(); dgvDataInfo.Rows.Add("데이터", "0"); dgvDataInfo.Rows.Add("이미지", "0"); dgvDataInfo.Rows.Add("조향값", "0"); dgvDataInfo.Rows.Add("속도값", "0"); }
        private void btnSetRange_Click(object sender, EventArgs e) => _isRangeSettingMode = true;
        private void btnCancelRange_Click(object sender, EventArgs e) => ClearMarkers();

        private void UpdateCharts()
        {
            if (_allData.Count == 0) return;

            int step = Math.Max(1, _allData.Count / 100);
            int maxImgNo = _allData.Count - 1;

            // 📊 1. [chtSteeringValue] 이미지 번호에 따른 데이터 조향값 바인딩
            if (chtSteeringValue != null && chtSteeringValue.Series.Count > 0)
            {
                chtSteeringValue.Series[0].Points.Clear();
                for (int i = 0; i < _allData.Count; i += step)
                {
                    chtSteeringValue.Series[0].Points.AddXY(_allData[i].Index, _allData[i].Steering);
                }
                if (chtSteeringValue.ChartAreas.Count > 0)
                {
                    var ca = chtSteeringValue.ChartAreas[0];
                    ca.RecalculateAxesScale();
                    ca.AxisX.Minimum = 0;
                    ca.AxisX.Maximum = maxImgNo;
                    ca.AxisX.LabelStyle.Format = "0;0;0"; // 🛠️ FIX: 음수 오차로 인해 생기는 '-0' 표기를 '0'으로 강제 보정
                    ca.AxisX.Title = $"[Steering] (0 ~ {maxImgNo})";
                }
                chtSteeringValue.Invalidate();
            }

            // 📊 2. [chtSpeedValue] 이미지 번호에 따른 데이터 속도값 바인딩
            if (chtSpeedValue != null && chtSpeedValue.Series.Count > 0)
            {
                chtSpeedValue.Series[0].Points.Clear();
                for (int i = 0; i < _allData.Count; i += step)
                {
                    chtSpeedValue.Series[0].Points.AddXY(_allData[i].Index, _allData[i].Speed);
                }
                if (chtSpeedValue.ChartAreas.Count > 0)
                {
                    var ca = chtSpeedValue.ChartAreas[0];
                    ca.RecalculateAxesScale();
                    ca.AxisX.Minimum = 0;
                    ca.AxisX.Maximum = maxImgNo;
                    ca.AxisX.LabelStyle.Format = "0;0;0"; // 🛠️ FIX: 음수 오차로 인해 생기는 '-0' 표기를 '0'으로 강제 보정
                    ca.AxisX.Title = $"[Speed] (0 ~ {maxImgNo})";
                }
                chtSpeedValue.Invalidate();
            }
        }

        private void tpDataManager_Click(object sender, EventArgs e) { }
        private void gbDataContent_Enter(object sender, EventArgs e) { }
        private void gbTrainingSetup_Enter(object sender, EventArgs e) { }
        private void tlpTestPreview_Paint(object sender, PaintEventArgs e) { }

        #endregion

        private void dgvDataInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lvDataItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDataItems.SelectedItems.Count == 0) return;

            string strIndex = lvDataItems.SelectedItems[0].SubItems[0].Text;
            if (int.TryParse(strIndex, out int targetIndex))
            {
                if (_playTimer.Enabled)
                {
                    _playTimer.Stop();
                }

                int listPosition = _allData.FindIndex(x => x.Index == targetIndex);

                if (listPosition != -1)
                {
                    _currentIndex = listPosition;
                    UpdateDisplay();

                    if (_isRangeSettingMode)
                    {
                        AddMarker();
                    }
                }
            }
        }

        private void btnFilter_Click_1(object sender, EventArgs e)
        {
            if (_allData.Count == 0) return;

            var targetData = _allData.Where(x => x.Steering == 0 || x.Speed == 0).ToList();

            if (targetData.Count > 0)
            {
                _deletedDataBuffer = new List<DrivingData>(targetData);
                _allData.RemoveAll(x => x.Steering == 0 || x.Speed == 0);
                RefreshUI();

                MessageBox.Show($"필터링 완료: 조향각 또는 스로틀이 0인 데이터 {targetData.Count}건이 제거되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("필터링 조건(조향각 0 또는 스로틀 0)에 해당하는 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlpTestCharts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class DrivingData
    {
        public int Index { get; set; }
        public string ImagePath { get; set; } = "";
        public double Steering { get; set; }
        public double Speed { get; set; }
    }
}