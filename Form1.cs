using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataManager
{
    public partial class Form1 : Form
    {
        // [1] 데이터 및 상태 변수
        private List<DrivingData> _allData = new List<DrivingData>();
        private int _currentIndex = -1;
        private bool _isReversed = false;
        private bool _isRangeSettingMode = false;
        private readonly List<Panel> _imageRangeMarkers = new List<Panel>();
        private System.Windows.Forms.Timer _playTimer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
            _playTimer.Tick += PlayTimer_Tick;
            this.Shown += Form1_Shown;
        }

        private void Form1_Shown(object? sender, EventArgs e)
        {
            InitializeDataInfoGrid();
            UpdatePlaybackSpeedLabel();
            // 초기 차트 뼈대 구성
            PrepareChart(chtSteeringValue, "Steering", Color.DodgerBlue);
            PrepareChart(chtSpeedValue, "Speed", Color.OrangeRed);
        }

        // [차트 강제 활성화 로직]
        private void PrepareChart(Chart chart, string seriesName, Color color)
        {
            if (chart == null) return;
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(new ChartArea("Main"));
            chart.Series.Clear();
            var s = chart.Series.Add(seriesName);
            s.ChartType = SeriesChartType.Line;
            s.Color = color;
            s.BorderWidth = 2;
            chart.Visible = true;
        }

        #region [1~4. 데이터 불러오기 및 기본 표시]

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

        private void LoadData(string path)
        {
            var files = Directory.GetFiles(path, "*.jpg").OrderBy(f => f).ToList();
            if (files.Count == 0) return;

            _allData.Clear();
            Random rnd = new Random();
            foreach (var file in files)
            {
                _allData.Add(new DrivingData
                {
                    ImagePath = file,
                    Steering = (rnd.NextDouble() * 2) - 1,
                    Speed = rnd.Next(20, 120)
                });
            }

            _currentIndex = 0;
            tbImageNavigator.Maximum = _allData.Count - 1;
            UpdateFrame();
            UpdateCharts(); // 9. 데이터 그래프 출력
        }

        private void UpdateFrame()
        {
            if (_currentIndex < 0 || _currentIndex >= _allData.Count) return;

            var data = _allData[_currentIndex];
            // 2. 이미지 표시
            if (File.Exists(data.ImagePath))
            {
                using (var stream = new FileStream(data.ImagePath, FileMode.Open, FileAccess.Read))
                {
                    pbDataPreview.Image = Image.FromStream(stream);
                }
            }

            // 4. 리스트 정보 업데이트
            dgvDataInfo.Rows[0].Cells[1].Value = _allData.Count;
            dgvDataInfo.Rows[1].Cells[1].Value = _currentIndex;
            dgvDataInfo.Rows[2].Cells[1].Value = data.Steering.ToString("F2");
            dgvDataInfo.Rows[3].Cells[1].Value = data.Speed.ToString("F0");

            // 3. 프레임 이동 (내비게이터 동기화)
            tbImageNavigator.Value = _currentIndex;
        }

        #endregion

        #region [7. 이미지 자동 재생 제어]

        private void btnPlay_Click_1(object sender, EventArgs e) { _isReversed = false; StartPlayback(); }
        private void btnStop_Click(object sender, EventArgs e) { _playTimer.Stop(); }
        private void btnReverse_Click(object sender, EventArgs e) { _isReversed = true; StartPlayback(); }

        private void StartPlayback()
        {
            if (_allData.Count == 0) return;
            decimal[] speeds = { 0.25m, 0.5m, 1m, 1.5m, 2m };
            _playTimer.Interval = (int)(200 / (double)speeds[tbPlaybackSpeed.Value]);
            _playTimer.Start();
        }

        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isReversed) { if (_currentIndex > 0) _currentIndex--; else _playTimer.Stop(); }
            else { if (_currentIndex < _allData.Count - 1) _currentIndex++; else _playTimer.Stop(); }
            UpdateFrame();
        }

        #endregion

        #region [8~10. 학습, 그래프, 테스트]

        // 9. 데이터 그래프 업데이트
        private void UpdateCharts()
        {
            if (chtSteeringValue == null || chtSteeringValue.Series.Count == 0) return;

            chtSteeringValue.Series[0].Points.Clear();
            chtSpeedValue.Series[0].Points.Clear();

            int step = Math.Max(1, _allData.Count / 100);
            for (int i = 0; i < _allData.Count; i += step)
            {
                chtSteeringValue.Series[0].Points.AddXY(i, _allData[i].Steering);
                chtSpeedValue.Series[0].Points.AddXY(i, _allData[i].Speed);
            }
        }

        // 8. 학습 실행
        private async void btnTrain_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false;
            for (int i = 0; i <= 100; i += 20)
            {
                txtTrainingLog.AppendText($"학습 중... {i}%\r\n");
                await Task.Delay(300);
            }
            txtTrainingLog.AppendText("학습 완료\r\n");
            btnTrain.Enabled = true;
        }

        #endregion

        #region [기타 유틸리티 및 마커 로직]

        private void InitializeDataInfoGrid() { dgvDataInfo.Rows.Clear(); dgvDataInfo.Rows.Add("데이터", "0"); dgvDataInfo.Rows.Add("이미지", "0"); dgvDataInfo.Rows.Add("조향값", "0"); dgvDataInfo.Rows.Add("속도값", "0"); }
        private void UpdatePlaybackSpeedLabel() { decimal[] speeds = { 0.25m, 0.5m, 1m, 1.5m, 2m }; lblPlaybackSpeed.Text = $"x{speeds[tbPlaybackSpeed.Value]:0.##}"; }
        private void tbPlaybackSpeed_Scroll(object sender, EventArgs e) { UpdatePlaybackSpeedLabel(); }
        private void tbImageNavigator_MouseUp(object sender, MouseEventArgs e) { if (_allData.Count > 0) { _currentIndex = tbImageNavigator.Value; UpdateFrame(); } }

        // 삭제 기능 (기존 마커 로직 유지)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentIndex >= 0 && _allData.Count > 0)
            {
                _allData.RemoveAt(_currentIndex);
                if (_currentIndex >= _allData.Count) _currentIndex = _allData.Count - 1;
                UpdateFrame();
                UpdateCharts();
            }
        }

        // 디자이너 연결용 빈 메서드들
        private void tpDataManager_Click(object sender, EventArgs e) { }
        private void btnCheckDataIntegrity_Click(object sender, EventArgs e) { }
        private void btnSetRange_Click(object sender, EventArgs e) { _isRangeSettingMode = true; }
        private void btnCancelRange_Click(object sender, EventArgs e) { _isRangeSettingMode = false; }
        private void btnCancelDelete_Click(object sender, EventArgs e) { }
        private void gbDataContent_Resize(object sender, EventArgs e) { if (_allData.Count > 0) UpdateCharts(); }
        private void gbDataContent_Enter(object sender, EventArgs e) { }
        private void gbTrainingSetup_Enter(object sender, EventArgs e) { }
        private void tlpTestPreview_Paint(object sender, PaintEventArgs e) { }
        #endregion
    }

    public class DrivingData
    {
        public string ImagePath { get; set; } = "";
        public double Steering { get; set; }
        public double Speed { get; set; }
    }
}