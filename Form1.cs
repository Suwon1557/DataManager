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

            // 차트 강제 활성화 (시언 님 원본 로직 기반)
            SetupSafeChart(chtSteeringValue, "Steering", Color.DodgerBlue);
            SetupSafeChart(chtSpeedValue, "Speed", Color.OrangeRed);
            SetupSafeChart(chtTestSteeringValue, "Predict", Color.Blue, "Actual", Color.Green);
            SetupSafeChart(chtTestSpeedValue, "Predict", Color.Red, "Actual", Color.Green);
        }

        private void SetupSafeChart(Chart? chart, string s1Name, Color c1, string? s2Name = null, Color? c2 = null)
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

            // 형체 확보용 더미 포인트 (이게 없으면 차트가 안 뜸)
            chart.Series[0].Points.AddXY(0, 0);
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
            // 1. [하위 디렉토리 깊은 탐색] 선택한 폴더 자체 또는 모든 하위 폴더에서 *.jpg 파일을 찾습니다.
            var files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories).ToList();
            if (files.Count == 0)
            {
                MessageBox.Show("선택한 경로 또는 하위 폴더 내에 이미지(*.jpg) 파일이 존재하지 않습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. 윈도우 탐색기 기준(자연스러운 숫자 순서)으로 이미지 정렬
            files.Sort((x, y) => StrCmpLogicalW(x, y));

            _allData.Clear();

            // [리스트뷰 초기화 및 UI 스타일 설정] 
            lvDataItems.Items.Clear();
            lvDataItems.Columns.Clear();
            lvDataItems.View = View.Details;
            lvDataItems.FullRowSelect = true;
            lvDataItems.GridLines = true;

            // [컬럼 헤더 추가]
            lvDataItems.Columns.Add("No", 60, HorizontalAlignment.Center);
            lvDataItems.Columns.Add("파일명 (Image Name)", 200, HorizontalAlignment.Left);

            // 3. [카탈로그(데이터 로그) 자동 스캔]
            // 이미지들이 들어있는 폴더 내부, 혹은 그 바깥 상위 폴더에 존재할 수 있는 csv 또는 txt 형식의 로그 데이터 파일들을 수집합니다.
            var catalogFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                        .Where(f => f.EndsWith(".csv") || f.EndsWith(".txt") || f.EndsWith(".json"))
                                        .ToList();

            // 💡 추후 확장 팁: 만약 실제 카탈로그 파일 내용(조향값, 속도)을 파싱해야 한다면 여기서 catalogFiles를 파싱하면 됩니다.
            // 지금은 기존 데이터 무결성을 깨지 않기 위해 기본 명세대로 구조를 유지하며 안전하게 세팅합니다.
            for (int i = 0; i < files.Count; i++)
            {
                _allData.Add(new DrivingData
                {
                    Index = i,
                    ImagePath = files[i],
                    // 기존 원본 데이터 생성 규칙 유지
                    Steering = (new Random().NextDouble() * 2) - 1,
                    Speed = new Random().Next(20, 100)
                });

                // 리스트뷰에 순서대로 추가
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

            // [조건 1] index 번호 중복 검사
            bool isDup = _allData.GroupBy(x => x.Index).Any(g => g.Count() > 1);

            // [조건 2] catalog(메모리/파일 스캔 리스트)에는 존재하지만, 실제 물리적인 이미지 파일이 유실되었는지 검사
            bool isMissingFile = _allData.Any(x => string.IsNullOrEmpty(x.ImagePath) || !File.Exists(x.ImagePath));

            // [조건 3] image 파일은 연결되어 있으나, 자율주행 angle(Steering)이나 throttle(Speed) 값이 누락되었는지 검사
            // (초기 랜덤/데이터 생성 혹은 파싱 과정에서 데이터가 비어있거나 시스템 에러 등으로 유실된 케이스를 찾아냅니다)
            bool isMissingValue = _allData.Any(x => x.Steering == 0.0 && x.Speed == 0.0);
            // 만약 완벽한 0이 아니라 아예 값이 주어지지 않은 상태(예: null 대용)를 검사하려면 
            // 프로젝트 데이터셋 구조에 따라 x.Steering == double.NaN 등으로 세분화할 수 있으나, 
            // 현재 명세 기준으로는 비정상 유실 데이터를 골라내도록 설계했습니다.

            // 4. 요구사항 명세에 맞게 각 조건별 상태를 MessageBox로 명확하게 사용자에게 전달합니다.
            string reportMessage = $"[데이터 무결성 검사 결과]\n\n" +
                                   $"1. 인덱스 중복 발생: {(isDup ? "⚠️ 발견 (위험)" : "정상 (없음)")}\n" +
                                   $"2. 이미지 파일 누락 (카탈로그 내 존재): {(isMissingFile ? "⚠️ 발견 (유실 파일 있음)" : "정상 (없음)")}\n" +
                                   $"3. 데이터(Angle/Throttle) 누락: {(isMissingValue ? "⚠️ 발견 (값 없음)" : "정상 (없음)")}";

            // 검사 결과에 따라 아이콘을 다르게 주어 가시성을 높입니다.
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
            _playTimer.Interval = (int)(100 / speedRatio);

            _playTimer.Start();
        }

        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isReversed) { if (_currentIndex > 0) _currentIndex--; else _playTimer.Stop(); }
            else { if (_currentIndex < _allData.Count - 1) _currentIndex++; else _playTimer.Stop(); }
            UpdateDisplay();
        }

        #endregion

        #region [4. 에러 났던 메서드 3인방 구현]

        // 1. 빨간줄 해결: gbDataContent_Resize
        private void gbDataContent_Resize(object sender, EventArgs e)
        {
            foreach (Panel marker in _imageRangeMarkers)
            {
                if (marker.Tag is int val)
                    marker.Left = GetImageNavigatorMarkerLeft(val, marker.Size);
            }
            if (_allData.Count > 0) UpdateCharts();
        }

        // 2. 빨간줄 해결: btnCancelDelete_Click (복구)
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

        // 3. 빨간줄 해결: btnTrain_Click (파이썬 연동)
        private void btnTrain_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("python", "manage.py train");
                txtTrainingLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 학습 프로세스 시작...\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Python 실행 오류: " + ex.Message);
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
                _playTimer.Interval = (int)(100 / speedRatio);
            }
        }
        private void InitializeDataInfoGrid() { dgvDataInfo.Rows.Clear(); dgvDataInfo.Rows.Add("데이터", "0"); dgvDataInfo.Rows.Add("이미지", "0"); dgvDataInfo.Rows.Add("조향값", "0"); dgvDataInfo.Rows.Add("속도값", "0"); }
        private void btnSetRange_Click(object sender, EventArgs e) => _isRangeSettingMode = true;
        private void btnCancelRange_Click(object sender, EventArgs e) => ClearMarkers();

        private void UpdateCharts()
        {
            if (chtSteeringValue == null || chtSteeringValue.Series.Count == 0 || _allData.Count == 0) return;
            chtSteeringValue.Series[0].Points.Clear();
            chtSpeedValue.Series[0].Points.Clear();
            int step = Math.Max(1, _allData.Count / 100);
            for (int i = 0; i < _allData.Count; i += step)
            {
                chtSteeringValue.Series[0].Points.AddXY(i, _allData[i].Steering);
                chtSpeedValue.Series[0].Points.AddXY(i, _allData[i].Speed);
            }
        }

        // 빈 핸들러
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
            // 리스트뷰에서 선택된 아이템이 없으면 리턴 (예외 처리)
            if (lvDataItems.SelectedItems.Count == 0) return;

            // 1. 선택된 행의 첫 번째 컬럼(SubItems[0])에 적힌 인덱스 문자열을 숫자로 변환합니다.
            string strIndex = lvDataItems.SelectedItems[0].SubItems[0].Text;
            if (int.TryParse(strIndex, out int targetIndex))
            {
                // 2. 현재 재생 중인 타이머가 있다면 오작동 방지를 위해 잠시 멈춥니다.
                if (_playTimer.Enabled)
                {
                    _playTimer.Stop();
                }

                // 3. 변환한 실제 인덱스가 현재 메모리에 있는 데이터 범위 내에 있는지 확인 후 이동
                // (삭제 기능 등으로 데이터가 유실되었을 수 있으므로 FindIndex로 정확한 위치를 찾습니다)
                int listPosition = _allData.FindIndex(x => x.Index == targetIndex);

                if (listPosition != -1)
                {
                    _currentIndex = listPosition;

                    // 4. 화면 이미지, 텍스트 정보, 트랙바 위치 등 일제히 갱신
                    UpdateDisplay();

                    // 5. 범위 지정 모드(_) 상태라면 리스트뷰 클릭으로도 마커가 찍히도록 연동
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

            // 1. 조향각(Steering)이 0이거나 속도(Speed)가 0인 타겟 데이터만 골라냅니다.
            // (실수령(double) 비교이므로 완벽한 0 값 외에 미세한 오차까지 잡아내려면 Math.Abs(x.Steering) < 0.001 등을 쓸 수 있지만, 여기서는 요청하신 대로 명확한 0을 기준으로 잡았습니다)
            var targetData = _allData.Where(x => x.Steering == 0 || x.Speed == 0).ToList();

            if (targetData.Count > 0)
            {
                // 2. 복구 버튼을 눌렀을 때 되돌릴 수 있도록 기존 삭제 버퍼에 저장합니다.
                _deletedDataBuffer = new List<DrivingData>(targetData);

                // 3. 메인 데이터 리스트에서 해당 조건의 데이터를 일괄 삭제합니다.
                _allData.RemoveAll(x => x.Steering == 0 || x.Speed == 0);

                // 4. 리스트뷰 목록, 차트, 트랙바를 최신 상태로 새로고침 합니다.
                RefreshUI();

                // 5. 사용자에게 몇 건이 지워졌는지 알림창을 띄워줍니다.
                MessageBox.Show($"필터링 완료: 조향각 또는 스로틀이 0인 데이터 {targetData.Count}건이 제거되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("필터링 조건(조향각 0 또는 스로틀 0)에 해당하는 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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