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
    // ⭐ 중요: Form1 클래스가 파일 최상단에 있어야 디자이너 에러가 발생하지 않습니다.
    public partial class Form1 : Form
    {
        // [필드 변수 선언]
        private List<DrivingData> _allData = new List<DrivingData>();
        private List<DrivingData> _deletedDataBuffer = new List<DrivingData>(); // 삭제 취소(복구)용
        private int _currentIndex = -1;
        private bool _isReversed = false;
        private bool _isRangeSettingMode = false;
        private readonly List<Panel> _imageRangeMarkers = new List<Panel>();
        private System.Windows.Forms.Timer _playTimer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();

            // 1. 트랙바 범위 설정 (Value 대입보다 먼저 실행되어야 에러가 안 남)
            tbPlaybackSpeed.Minimum = 25;
            tbPlaybackSpeed.Maximum = 200;
            tbPlaybackSpeed.Value = 100; // 초기 배속 x1

            // 2. 초기 UI 및 타이머 설정
            InitializeDataInfoGrid();
            UpdatePlaybackSpeedLabel();
            _playTimer.Tick += PlayTimer_Tick;

            // 3. 폼 로드 후 실행될 이벤트
            this.Shown += Form1_Shown;
        }

        #region [1. 초기화 및 차트 안전 장치]

        private void Form1_Shown(object? sender, EventArgs e)
        {
            dgvDataInfo.ClearSelection();
            _isRangeSettingMode = false;
            pnlImageRangeMarker.Visible = false;

            // 차트 뼈대 강제 생성 (EnsureChartVisible 로직)
            SetupChartSafe(chtSteeringValue, "Steering", Color.DodgerBlue);
            SetupChartSafe(chtSpeedValue, "Speed", Color.OrangeRed);
            SetupChartSafe(chtTestSteeringValue, "Predict", Color.Blue, "Actual", Color.Green);
            SetupChartSafe(chtTestSpeedValue, "Predict", Color.Red, "Actual", Color.Green);
        }

        private void SetupChartSafe(Chart? chart, string s1Name, Color c1, string? s2Name = null, Color? c2 = null)
        {
            if (chart == null) return;
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(new ChartArea("Main"));
            chart.Series.Clear();

            var s1 = chart.Series.Add(s1Name);
            s1.ChartType = SeriesChartType.Line;
            s1.Color = c1;
            s1.BorderWidth = 2;

            if (s2Name != null)
            {
                var s2 = chart.Series.Add(s2Name);
                s2.ChartType = SeriesChartType.Line;
                s2.Color = c2 ?? Color.Green;
                s2.BorderWidth = 2;
            }

            // 영역 확보용 더미 포인트
            chart.Series[0].Points.AddXY(0, 0);
            chart.Visible = true;
            chart.BringToFront();
        }

        #endregion

        #region [2. 데이터 로드 및 무결성 검사]

        private void btnSelectAdd_Click(object sender, EventArgs e) // Designer의 btnSelectFolder에 연결됨
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

        private void LoadData(string path)
        {
            var files = Directory.GetFiles(path, "*.jpg").OrderBy(f => f).ToList();
            if (files.Count == 0) return;

            _allData.Clear();
            lvDataItems.Items.Clear();

            for (int i = 0; i < files.Count; i++)
            {
                _allData.Add(new DrivingData
                {
                    Index = i,
                    ImagePath = files[i],
                    Steering = (new Random().NextDouble() * 2) - 1, // 샘플 데이터
                    Speed = new Random().Next(20, 120)
                });

                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(Path.GetFileName(files[i]));
                lvDataItems.Items.Add(item);
            }

            _currentIndex = 0;
            tbImageNavigator.Maximum = _allData.Count - 1;
            UpdateFrame();
            UpdateCharts();
        }

        private void btnCheckDataIntegrity_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0) return;

            bool isDup = _allData.GroupBy(x => x.Index).Any(g => g.Count() > 1);
            bool isMissingFile = _allData.Any(x => !File.Exists(x.ImagePath));

            string result = "무결성 검사 완료:\n";
            result += isDup ? "[경고] 인덱스 중복 발견\n" : "[정상] 인덱스 고유함\n";
            result += isMissingFile ? "[경고] 이미지 파일 누락 발견\n" : "[정상] 모든 이미지 존재\n";

            MessageBox.Show(result, "데이터 무결성 확인");
        }

        #endregion

        #region [3. 탐색 및 재생 제어]

        private void UpdateFrame()
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
            double speed = tbPlaybackSpeed.Value / 100.0;
            _playTimer.Interval = (int)(200 / speed);
            _playTimer.Start();
        }

        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isReversed) { if (_currentIndex > 0) _currentIndex--; else _playTimer.Stop(); }
            else { if (_currentIndex < _allData.Count - 1) _currentIndex++; else _playTimer.Stop(); }
            UpdateFrame();
        }

        #endregion

        #region [4. 데이터 필터링 및 삭제/복구]

        private void btnFilter_Click(object sender, EventArgs e) // 조향/속도 0인 데이터 삭제
        {
            var targets = _allData.Where(x => x.Steering == 0 || x.Speed == 0).ToList();
            if (targets.Count > 0)
            {
                _deletedDataBuffer = new List<DrivingData>(targets);
                _allData.RemoveAll(x => x.Steering == 0 || x.Speed == 0);
                RefreshAfterEdit();
                MessageBox.Show($"{targets.Count}개의 데이터가 필터링되었습니다.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0 || _currentIndex < 0) return;

            if (_imageRangeMarkers.Count == 2) // 범위 삭제
            {
                int start = (int)_imageRangeMarkers[0].Tag;
                int end = (int)_imageRangeMarkers[1].Tag;
                int min = Math.Min(start, end);
                int max = Math.Max(start, end);

                _deletedDataBuffer = _allData.GetRange(min, max - min + 1);
                _allData.RemoveRange(min, max - min + 1);
                ClearMarkers();
            }
            else // 단일 삭제
            {
                _deletedDataBuffer = new List<DrivingData> { _allData[_currentIndex] };
                _allData.RemoveAt(_currentIndex);
            }
            RefreshAfterEdit();
        }

        private void btnCancelDelete_Click(object sender, EventArgs e)
        {
            if (_deletedDataBuffer.Count > 0)
            {
                _allData.AddRange(_deletedDataBuffer);
                _allData = _allData.OrderBy(x => x.Index).ToList();
                _deletedDataBuffer.Clear();
                RefreshAfterEdit();
            }
        }

        private void RefreshAfterEdit()
        {
            _currentIndex = Math.Max(0, Math.Min(_currentIndex, _allData.Count - 1));
            tbImageNavigator.Maximum = Math.Max(0, _allData.Count - 1);
            UpdateFrame();
            UpdateCharts();
        }

        #endregion

        #region [5. 학습 연동 및 차트 업데이트]

        private void btnTrain_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("python", "manage.py train")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                Process.Start(psi);
                txtTrainingLog.AppendText("Training Process Started...\r\n");
            }
            catch (Exception ex) { MessageBox.Show("Python 실행 오류: " + ex.Message); }
        }

        private void UpdateCharts()
        {
            if (chtSteeringValue?.Series.Count == 0 || _allData.Count == 0) return;

            chtSteeringValue.Series[0].Points.Clear();
            chtSpeedValue.Series[0].Points.Clear();

            int step = Math.Max(1, _allData.Count / 100);
            for (int i = 0; i < _allData.Count; i += step)
            {
                chtSteeringValue.Series[0].Points.AddXY(i, _allData[i].Steering);
                chtSpeedValue.Series[0].Points.AddXY(i, _allData[i].Speed);
            }
        }

        #endregion

        #region [6. 마커 위치 계산 및 디자이너 빈 메서드]

        private int GetImageNavigatorMarkerLeft(int value, Size markerSize)
        {
            int min = tbImageNavigator.Minimum, max = tbImageNavigator.Maximum;
            double ratio = (max == min) ? 0 : (double)(value - min) / (max - min);
            int usableWidth = tbImageNavigator.Width - 20;
            int markerCenterX = tbImageNavigator.Left + 10 + (int)(usableWidth * ratio);
            return markerCenterX - (markerSize.Width / 2);
        }

        private void tbImageNavigator_MouseUp(object sender, MouseEventArgs e)
        {
            _currentIndex = tbImageNavigator.Value;
            UpdateFrame();
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

        private void InitializeDataInfoGrid() { dgvDataInfo.Rows.Clear(); dgvDataInfo.Rows.Add("데이터 수", "0"); dgvDataInfo.Rows.Add("이미지 번호", "0"); dgvDataInfo.Rows.Add("조향값", "0"); dgvDataInfo.Rows.Add("속도값", "0"); }
        private void UpdatePlaybackSpeedLabel() { lblPlaybackSpeed.Text = $"x{tbPlaybackSpeed.Value / 100.0:0.##}"; }
        private void tbPlaybackSpeed_Scroll(object sender, EventArgs e) => UpdatePlaybackSpeedLabel();
        private void btnSetRange_Click(object sender, EventArgs e) => _isRangeSettingMode = true;
        private void btnCancelRange_Click(object sender, EventArgs e) => ClearMarkers();
        private void gbDataContent_Resize(object sender, EventArgs e) { if (_allData.Count > 0) UpdateCharts(); }

        // 디자이너 에러 방지용 빈 메서드들
        private void tpDataManager_Click(object sender, EventArgs e) { }
        private void gbDataContent_Enter(object sender, EventArgs e) { }
        private void gbTrainingSetup_Enter(object sender, EventArgs e) { }
        private void tlpTestPreview_Paint(object sender, PaintEventArgs e) { }
        #endregion
    }

    public class DrivingData
    {
        public int Index { get; set; }
        public string ImagePath { get; set; } = "";
        public double Steering { get; set; }
        public double Speed { get; set; }
    }
}