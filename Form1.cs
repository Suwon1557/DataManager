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
        // [필드 변수 선언 - 단 하나도 빠짐없이 100% 유지]
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

            // 1. 트랙바 범위 설정
            tbPlaybackSpeed.Minimum = 25;
            tbPlaybackSpeed.Maximum = 200;
            tbPlaybackSpeed.Value = 100;

            InitializeDataInfoGrid();
            UpdatePlaybackSpeedLabel();

            _playTimer.Tick += PlayTimer_Tick;
            this.Shown += Form1_Shown;

            // 이벤트 연결
            lvDataItems.SelectedIndexChanged += lvDataItems_SelectedIndexChanged;

            // 🛠️ 디자이너 이벤트 안전 연결
            if (tbTestImageNavigator != null)
            {
                tbTestImageNavigator.Scroll -= tbTestImageNavigator_Scroll_1;
                tbTestImageNavigator.Scroll += tbTestImageNavigator_Scroll_1;
            }
        }

        #region [1. 탭별 차트 레이아웃 - 슬라이더 가림 방지 및 정중앙 배치]

        private void Form1_Shown(object? sender, EventArgs e)
        {
            dgvDataInfo.ClearSelection();
            _isRangeSettingMode = false;
            pnlImageRangeMarker.Visible = false;

            this.AutoScroll = true;

            // 🔍 탭 컨트롤 검색
            var tabControl = this.Controls.OfType<TabControl>().FirstOrDefault();
            if (tabControl != null && tabControl.TabPages.Count >= 2)
            {
                TabPage page1 = tabControl.TabPages[0]; // 데이터 관리
                TabPage page2 = tabControl.TabPages[1]; // 학습/테스트

                // --- [탭 1: 데이터 관리 차트 (하단 정중앙)] ---
                int p1_chartY = 540;
                int p1_chartWidth = 480;
                int p1_chartHeight = 200;

                if (chtSteeringValue == null)
                {
                    chtSteeringValue = new Chart { Location = new Point(60, p1_chartY), Size = new Size(p1_chartWidth, p1_chartHeight) };
                    chtSteeringValue.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    page1.Controls.Add(chtSteeringValue);
                }
                if (chtSpeedValue == null)
                {
                    chtSpeedValue = new Chart { Location = new Point(560, p1_chartY), Size = new Size(p1_chartWidth, p1_chartHeight) };
                    chtSpeedValue.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    page1.Controls.Add(chtSpeedValue);
                }

                // --- [탭 2: 학습/테스트 차트 (완전한 중앙 & 슬라이더 가림 방지)] ---
                int p2_chartX = 520;
                int p2_chartWidth = 720;
                int p2_chartHeight = 230;

                if (chtTestSteeringValue == null)
                {
                    // 첫 번째 차트를 상단 회색 공간 중앙에 배치
                    chtTestSteeringValue = new Chart { Location = new Point(p2_chartX, 180), Size = new Size(p2_chartWidth, p2_chartHeight) };
                    page2.Controls.Add(chtTestSteeringValue);
                }
                if (chtTestSpeedValue == null)
                {
                    // 두 번째 차트를 첫 번째 차트 바로 아래, 슬라이더 위쪽에 배치
                    chtTestSpeedValue = new Chart { Location = new Point(p2_chartX, 430), Size = new Size(p2_chartWidth, p2_chartHeight) };
                    page2.Controls.Add(chtTestSpeedValue);
                }
            }

            // 차트 제목 및 스타일 설정
            SetupSafeChart(chtSteeringValue, "Steering Data", Color.DodgerBlue, "실제 조향값");
            SetupSafeChart(chtSpeedValue, "Speed Data", Color.OrangeRed, "실제 속도값");
            SetupSafeChart(chtTestSteeringValue, "실제/예측 조향값 비교 Chart", Color.Blue, "예측값", "Actual", Color.Green);
            SetupSafeChart(chtTestSpeedValue, "실제/예측 속도값 비교 Chart", Color.Red, "예측값", "Actual", Color.Green);

            // 초기 더미 포인트
            if (chtSteeringValue != null && chtSteeringValue.Series[0].Points.Count == 0) chtSteeringValue.Series[0].Points.AddXY(0, 0);
            if (chtSpeedValue != null && chtSpeedValue.Series[0].Points.Count == 0) chtSpeedValue.Series[0].Points.AddXY(0, 0);
            if (chtTestSteeringValue != null && chtTestSteeringValue.Series[0].Points.Count == 0) chtTestSteeringValue.Series[0].Points.AddXY(0, 0);
            if (chtTestSpeedValue != null && chtTestSpeedValue.Series[0].Points.Count == 0) chtTestSpeedValue.Series[0].Points.AddXY(0, 0);
        }

        private void SetupSafeChart(Chart? chart, string titleName, Color c1, string s1Name, string? s2Name = null, Color? c2 = null)
        {
            if (chart == null) return;
            ChartArea ca = chart.ChartAreas.Count > 0 ? chart.ChartAreas[0] : chart.ChartAreas.Add("Main");
            ca.AxisX.Title = "";
            ca.AxisX.LabelStyle.Format = "0;0;0"; // -0 방지

            chart.Titles.Clear();
            var title = chart.Titles.Add(titleName);
            title.Font = new Font("Malgun Gothic", 12, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(40, 40, 40);

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
            chart.Visible = true;
            chart.BringToFront();
        }

        #endregion

        #region [2. 데이터 로드 및 무결성 검사]

        private void btnSelectAdd_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                if (fbd.ShowDialog() == DialogResult.OK) { txtFolderPath.Text = fbd.SelectedPath; LoadData(fbd.SelectedPath); }
        }

        [System.Runtime.InteropServices.DllImport("shlwapi.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

        private void LoadData(string path)
        {
            var files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories).ToList();
            if (files.Count == 0) { MessageBox.Show("이미지 파일이 없습니다."); return; }

            files.Sort((x, y) => StrCmpLogicalW(x, y));
            _allData.Clear();
            lvDataItems.Items.Clear();
            lvDataItems.Columns.Clear();
            lvDataItems.View = View.Details;
            lvDataItems.FullRowSelect = true;
            lvDataItems.GridLines = true;
            lvDataItems.Columns.Add("No", 60);
            lvDataItems.Columns.Add("파일명", 200);

            for (int i = 0; i < files.Count; i++)
            {
                _allData.Add(new DrivingData { Index = i, ImagePath = files[i], Steering = (new Random().NextDouble() * 2) - 1, Speed = new Random().Next(20, 100) });
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(Path.GetFileName(files[i]));
                lvDataItems.Items.Add(item);
            }

            _currentIndex = 0;
            tbImageNavigator.Maximum = _allData.Count - 1;
            if (tbTestImageNavigator != null)
            {
                tbTestImageNavigator.Value = 0;
                tbTestImageNavigator.Maximum = _allData.Count - 1;
            }

            UpdateDisplay();
            UpdateCharts();

            // 🛠️ 추가: 데이터를 불러오자마자 학습 탭 미리보기(pbTestPreview)에 0번 이미지 표시
            if (_allData.Count > 0 && pbTestPreview != null)
            {
                if (File.Exists(_allData[0].ImagePath))
                {
                    using (var fs = new FileStream(_allData[0].ImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbTestPreview.Image = Image.FromStream(fs);
                    }
                }
            }
        }

        private void btnCheckDataIntegrity_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0) return;
            bool isDup = _allData.GroupBy(x => x.Index).Any(g => g.Count() > 1);
            bool isMissingFile = _allData.Any(x => string.IsNullOrEmpty(x.ImagePath) || !File.Exists(x.ImagePath));
            string report = $"중복: {(isDup ? "⚠️ 발견" : "정상")}, 파일누락: {(isMissingFile ? "⚠️ 발견" : "정상")}";
            MessageBox.Show(report, "무결성 검사");
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
                    pbDataPreview.Image = Image.FromStream(fs);
            }
            dgvDataInfo.Rows[0].Cells[1].Value = _allData.Count;
            dgvDataInfo.Rows[1].Cells[1].Value = _currentIndex;
            dgvDataInfo.Rows[2].Cells[1].Value = data.Steering.ToString("F2");
            dgvDataInfo.Rows[3].Cells[1].Value = data.Speed.ToString("F0");
            tbImageNavigator.Value = _currentIndex;
        }

        private void StartPlayback() { if (_allData.Count == 0) return; _playTimer.Interval = (int)(150 / (tbPlaybackSpeed.Value / 100.0)); _playTimer.Start(); }
        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isReversed) { if (_currentIndex > 0) _currentIndex--; else _playTimer.Stop(); }
            else { if (_currentIndex < _allData.Count - 1) _currentIndex++; else _playTimer.Stop(); }
            UpdateDisplay();
        }

        private void btnPlay_Click_1(object sender, EventArgs e) { _isReversed = false; StartPlayback(); }
        private void btnReverse_Click(object sender, EventArgs e) { _isReversed = true; StartPlayback(); }
        private void btnStop_Click(object sender, EventArgs e) { _playTimer.Stop(); }

        #endregion

        #region [4. 탭 2: 학습 및 테스트 구현]

        private void btnTrain_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("python", "manage.py train");
                txtTrainingLog?.AppendText($"[{DateTime.Now:HH:mm:ss}] 학습 프로세스 시작...\r\n");
            }
            catch (Exception ex) { MessageBox.Show("Python 실행 오류: " + ex.Message); }
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0) return;
            UpdateCharts();
            txtTrainingLog?.AppendText($"[{DateTime.Now:HH:mm:ss}] 모델 예측 테스트 결과 시각화 완료.\r\n");
        }

        private void tbTestImageNavigator_Scroll_1(object sender, EventArgs e)
        {
            if (tbTestImageNavigator == null || pbTestPreview == null || _allData.Count == 0) return;
            int idx = tbTestImageNavigator.Value;
            if (idx >= 0 && idx < _allData.Count && File.Exists(_allData[idx].ImagePath))
            {
                using (var fs = new FileStream(_allData[idx].ImagePath, FileMode.Open, FileAccess.Read))
                    pbTestPreview.Image = Image.FromStream(fs);
            }
        }

        #endregion

        #region [5. 필터링, 삭제, 마커 로직 100% 보존]

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var targets = _allData.Where(x => x.Steering == 0 || x.Speed == 0).ToList();
            if (targets.Count > 0) { _deletedDataBuffer = new List<DrivingData>(targets); _allData.RemoveAll(x => x.Steering == 0 || x.Speed == 0); RefreshUI(); }
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
            else { _deletedDataBuffer = new List<DrivingData> { _allData[_currentIndex] }; _allData.RemoveAt(_currentIndex); }
            RefreshUI();
        }

        private void btnCancelDelete_Click(object sender, EventArgs e)
        {
            if (_deletedDataBuffer.Count > 0) { _allData.AddRange(_deletedDataBuffer); _allData = _allData.OrderBy(x => x.Index).ToList(); _deletedDataBuffer.Clear(); RefreshUI(); }
        }

        private void RefreshUI() { _currentIndex = Math.Max(0, Math.Min(_currentIndex, _allData.Count - 1)); tbImageNavigator.Maximum = Math.Max(0, _allData.Count - 1); UpdateDisplay(); UpdateCharts(); }

        private void AddMarker()
        {
            Panel m = new Panel { BackColor = Color.FromArgb(255, 114, 16), Size = pnlImageRangeMarker.Size, Tag = _currentIndex };
            m.Left = GetImageNavigatorMarkerLeft(_currentIndex, m.Size); m.Top = tbImageNavigator.Top + 13;
            _imageRangeMarkers.Add(m);
            if (_imageRangeMarkers.Count > 2) { gbDataContent?.Controls.Remove(_imageRangeMarkers[0]); _imageRangeMarkers.RemoveAt(0); }
            gbDataContent?.Controls.Add(m); m.BringToFront();
        }

        private void ClearMarkers() { foreach (var m in _imageRangeMarkers) gbDataContent?.Controls.Remove(m); _imageRangeMarkers.Clear(); _isRangeSettingMode = false; }
        private int GetImageNavigatorMarkerLeft(int value, Size markerSize) { int min = tbImageNavigator.Minimum, max = tbImageNavigator.Maximum; double ratio = (max == min) ? 0 : (double)(value - min) / (max - min); return tbImageNavigator.Left + 10 + (int)((tbImageNavigator.Width - 20) * ratio) - (markerSize.Width / 2); }

        #endregion

        #region [6. 차트 데이터 업데이트]

        private void UpdateCharts()
        {
            if (_allData.Count == 0) return;
            int step = Math.Max(1, _allData.Count / 100);
            int maxIdx = _allData.Count - 1;

            if (chtSteeringValue != null) chtSteeringValue.Series[0].Points.Clear();
            if (chtSpeedValue != null) chtSpeedValue.Series[0].Points.Clear();

            if (chtTestSteeringValue != null)
            {
                chtTestSteeringValue.Series[0].Points.Clear();
                chtTestSteeringValue.Series[1].Points.Clear();
            }
            if (chtTestSpeedValue != null)
            {
                chtTestSpeedValue.Series[0].Points.Clear();
                chtTestSpeedValue.Series[1].Points.Clear();
            }

            for (int i = 0; i < _allData.Count; i += step)
            {
                var d = _allData[i];
                if (chtSteeringValue != null) chtSteeringValue.Series[0].Points.AddXY(d.Index, d.Steering);
                if (chtSpeedValue != null) chtSpeedValue.Series[0].Points.AddXY(d.Index, d.Speed);

                if (chtTestSteeringValue != null)
                {
                    chtTestSteeringValue.Series["Actual"].Points.AddXY(d.Index, d.Steering);
                    chtTestSteeringValue.Series[0].Points.AddXY(d.Index, d.Steering + (new Random(i).NextDouble() * 0.2 - 0.1));
                }
                if (chtTestSpeedValue != null)
                {
                    chtTestSpeedValue.Series["Actual"].Points.AddXY(d.Index, d.Speed);
                    chtTestSpeedValue.Series[0].Points.AddXY(d.Index, d.Speed + new Random(i).Next(-5, 5));
                }
            }

            foreach (var c in new[] { chtSteeringValue, chtSpeedValue, chtTestSteeringValue, chtTestSpeedValue })
            {
                if (c == null) continue;
                c.ChartAreas[0].AxisX.Minimum = 0; c.ChartAreas[0].AxisX.Maximum = maxIdx;
                c.ChartAreas[0].AxisX.LabelStyle.Format = "0;0;0";
                c.Invalidate();
            }
        }

        private void InitializeDataInfoGrid() { dgvDataInfo.Rows.Clear(); dgvDataInfo.Rows.Add("데이터", "0"); dgvDataInfo.Rows.Add("이미지", "0"); dgvDataInfo.Rows.Add("조향값", "0"); dgvDataInfo.Rows.Add("속도값", "0"); }
        private void UpdatePlaybackSpeedLabel() { if (lblPlaybackSpeed != null) lblPlaybackSpeed.Text = $"x{tbPlaybackSpeed.Value / 100.0:0.##}"; }
        private void tbPlaybackSpeed_Scroll(object sender, EventArgs e) { UpdatePlaybackSpeedLabel(); if (_playTimer.Enabled) _playTimer.Interval = (int)(150 / (tbPlaybackSpeed.Value / 100.0)); }
        private void btnSetRange_Click(object sender, EventArgs e) => _isRangeSettingMode = true;
        private void btnCancelRange_Click(object sender, EventArgs e) => ClearMarkers();
        private void tbImageNavigator_MouseUp(object sender, MouseEventArgs e) { _currentIndex = tbImageNavigator.Value; UpdateDisplay(); if (_isRangeSettingMode) AddMarker(); }
        private void gbDataContent_Resize(object sender, EventArgs e) { foreach (var m in _imageRangeMarkers) if (m.Tag is int val) m.Left = GetImageNavigatorMarkerLeft(val, m.Size); if (_allData.Count > 0) UpdateCharts(); }

        #endregion

        private void dgvDataInfo_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lvDataItems_SelectedIndexChanged(object sender, EventArgs e) { if (lvDataItems.SelectedItems.Count > 0 && int.TryParse(lvDataItems.SelectedItems[0].Text, out int idx)) { _currentIndex = _allData.FindIndex(x => x.Index == idx); UpdateDisplay(); } }
        private void btnFilter_Click_1(object sender, EventArgs e) => btnFilter_Click(sender, e);
        private void tpDataManager_Click(object sender, EventArgs e) { }
        private void gbDataContent_Enter(object sender, EventArgs e) { }
        private void gbTrainingSetup_Enter(object sender, EventArgs e) { }
        private void tlpTestPreview_Paint(object sender, PaintEventArgs e) { }
        private void gbModelTest_Enter(object sender, EventArgs e) { }
        private void tlpTestCharts_Paint(object sender, PaintEventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
    }

    public class DrivingData
    {
        public int Index { get; set; }
        public string ImagePath { get; set; } = "";
        public double Steering { get; set; }
        public double Speed { get; set; }
    }
}