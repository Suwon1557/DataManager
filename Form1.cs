using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.Json;
using System.Runtime.InteropServices; // 🛠️ FIX: DllImport 사용을 위해 반드시 필요함!

namespace DataManager
{
    // ⭐ 중요: Form1 클래스가 파일 최상단에 위치해야 디자이너 에러가 방지됩니다.
    public partial class Form1 : Form
    {
        // [필드 변수 선언 - 단 하나도 빠짐없이 100% 유지]
        private List<DrivingData> _allData = new List<DrivingData>();
        private List<DrivingData> _deletedDataBuffer = new List<DrivingData>();
        private int _currentIndex = -1;
        private bool _isReversed = false;
        private bool _isRangeSettingMode = false;
        private readonly List<Panel> _imageRangeMarkers = new List<Panel>();
        private System.Windows.Forms.Timer _playTimer = new System.Windows.Forms.Timer();

        // 🛠️ FIX: shlwapi.dll 임포트 위치 (클래스 바로 아래)
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

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

            // 🛠️ 데이터 관리 탭 슬라이더: 드래그 즉시 실시간 업데이트를 위해 Scroll 이벤트 연결
            tbImageNavigator.Scroll += tbImageNavigator_Scroll;

            // 🛠️ 학습 탭 슬라이더 안전 연결
            if (tbTestImageNavigator != null)
            {
                tbTestImageNavigator.Scroll -= tbTestImageNavigator_Scroll_1;
                tbTestImageNavigator.Scroll += tbTestImageNavigator_Scroll_1;
            }
        }

        #region [1. 탭별 차트 레이아웃 - 중앙 배치 및 슬라이더 보호]

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
                    chtTestSteeringValue = new Chart { Location = new Point(p2_chartX, 180), Size = new Size(p2_chartWidth, p2_chartHeight) };
                    page2.Controls.Add(chtTestSteeringValue);
                }
                if (chtTestSpeedValue == null)
                {
                    chtTestSpeedValue = new Chart { Location = new Point(p2_chartX, 430), Size = new Size(p2_chartWidth, p2_chartHeight) };
                    page2.Controls.Add(chtTestSpeedValue);
                }
            }

            // 차트 제목 및 스타일 설정
            SetupSafeChart(chtSteeringValue, "Steering Data", Color.DodgerBlue, "실제 조향값");
            SetupSafeChart(chtSpeedValue, "Speed Data", Color.OrangeRed, "실제 속도값");
            SetupSafeChart(chtTestSteeringValue, "실제/예측 조향값 비교 Chart", Color.Blue, "예측값", "Actual", Color.Green);
            SetupSafeChart(chtTestSpeedValue, "실제/예측 속도값 비교 Chart", Color.Red, "예측값", "Actual", Color.Green);

            UpdateCharts();
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

        #region [2. 카탈로그 및 메니페스트 파싱 데이터 로드]

        private void btnSelectAdd_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                if (fbd.ShowDialog() == DialogResult.OK) { txtFolderPath.Text = fbd.SelectedPath; LoadData(fbd.SelectedPath); }
        }

        private void LoadData(string path)
        {
            _allData.Clear();
            lvDataItems.Items.Clear();

            var catalogFiles = Directory.GetFiles(path, "*.catalog");

            if (catalogFiles.Length > 0)
            {
                ParseCatalogData(path, catalogFiles);
            }
            else
            {
                var files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories).ToList();
                files.Sort((x, y) => StrCmpLogicalW(x, y));
                for (int i = 0; i < files.Count; i++)
                    _allData.Add(new DrivingData { Index = i, ImagePath = files[i], Steering = 0, Speed = 0 });
            }

            lvDataItems.BeginUpdate();
            foreach (var d in _allData)
            {
                var item = new ListViewItem(d.Index.ToString());
                item.SubItems.Add(Path.GetFileName(d.ImagePath));
                lvDataItems.Items.Add(item);
            }
            lvDataItems.EndUpdate();

            _currentIndex = 0;
            tbImageNavigator.Maximum = Math.Max(0, _allData.Count - 1);
            if (tbTestImageNavigator != null) tbTestImageNavigator.Maximum = Math.Max(0, _allData.Count - 1);

            UpdateDisplay();
            UpdateCharts();

            // 데이터 로드 즉시 학습 탭 미리보기 이미지 출력
            if (pbTestPreview != null && _allData.Count > 0)
                if (File.Exists(_allData[0].ImagePath)) pbTestPreview.Image = Image.FromFile(_allData[0].ImagePath);
        }

        private void ParseCatalogData(string basePath, string[] catalogFiles)
        {
            int globalIdx = 0;
            Array.Sort(catalogFiles, StrCmpLogicalW);
            foreach (var catFile in catalogFiles)
            {
                string[] lines = File.ReadAllLines(catFile);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    try
                    {
                        using (JsonDocument doc = JsonDocument.Parse(line))
                        {
                            var root = doc.RootElement;
                            string imgName = root.GetProperty("cam/image_array").GetString();
                            double angle = root.GetProperty("user/angle").GetDouble();
                            double throttle = root.GetProperty("user/throttle").GetDouble();

                            _allData.Add(new DrivingData
                            {
                                Index = globalIdx++,
                                ImagePath = Path.Combine(basePath, "images", imgName),
                                Steering = angle,
                                Speed = throttle * 100
                            });
                        }
                    }
                    catch { }
                }
            }
        }

        private void btnCheckDataIntegrity_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0) return;
            bool isDup = _allData.GroupBy(x => x.Index).Any(g => g.Count() > 1);
            bool isMissingFile = _allData.Any(x => !File.Exists(x.ImagePath));
            MessageBox.Show($"중복: {(isDup ? "⚠️ 발견" : "정상")}\n파일 유실: {(isMissingFile ? "⚠️ 발견" : "정상")}", "데이터 무결성 검사");
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

        #region [4. 탭 2: 학습 및 테스트]

        private void btnTrain_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 리눅스 환경 설정
                string envName = "e2e_env";
                string projectPath = "~/mysim";

                // 2. 실행할 전체 명령어 (라이브러리 점검 및 학습 시작)
                string installCmd = "pip install numpy==1.24.3 pandas==2.0.3 tensorflow==2.13.0 albumentations imgaug";
                string trainCmd = "python train.py --tub ./data --model ./models/mypilot.h5";

                // bash -i 모드로 실행하여 .bashrc 설정을 자동으로 로드합니다.
                string bashCmd = $"conda activate {envName} && cd {projectPath} && {installCmd} && {trainCmd}";

                // 3. 프로세스 실행 설정 (외부 CMD 창 모드)
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "cmd.exe";

                // /k 옵션으로 명령 실행 후 창을 유지하고, wsl 명령을 안전하게 전달합니다.
                start.Arguments = "/k wsl bash -i -c \"" + bashCmd + "\"";

                // 외부 창을 띄우기 위한 핵심 설정
                start.UseShellExecute = true;
                start.CreateNoWindow = false;

                Process.Start(start);

                txtTrainingLog?.AppendText($"[{DateTime.Now:HH:mm:ss}] 외부 터미널에서 WSL 학습 프로세스가 시작되었습니다.\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("프로세스 실행 중 오류: " + ex.Message);
            }
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (_allData.Count == 0) return;

            try
            {
                txtTrainingLog?.AppendText($"[{DateTime.Now:HH:mm:ss}] 테스트 세션 시작 (경로 동기화 중...)\r\n");

                string envName = "e2e_env";
                string projectPath = "/home/gorhanhee/mysim";
                string modelFile = "models/mypilot.h5";

                // 1. 윈도우 경로 설정
                string appDir = Application.StartupPath;
                string winPathsFile = Path.Combine(appDir, "win_paths.txt");
                string winResultsFile = Path.Combine(appDir, "results.csv");

                // 2. 리눅스용 이미지 경로 목록 작성 (Windows 파일 생성)
                var linuxPaths = _allData.Select(d => {
                    string drive = Path.GetPathRoot(d.ImagePath).Substring(0, 1).ToLower();
                    string subPath = d.ImagePath.Substring(Path.GetPathRoot(d.ImagePath).Length).Replace("\\", "/");
                    return $"/mnt/{drive}/{subPath}";
                }).ToList();
                File.WriteAllLines(winPathsFile, linuxPaths);

                // 3. [핵심] WSL이 이해하는 현재 폴더의 진짜 경로를 'wslpath'로 가져오기
                Process wslPathProc = new Process();
                wslPathProc.StartInfo.FileName = "wsl.exe";
                wslPathProc.StartInfo.Arguments = $"wslpath '{appDir.Replace("\\", "/")}'";
                wslPathProc.StartInfo.UseShellExecute = false;
                wslPathProc.StartInfo.RedirectStandardOutput = true;
                wslPathProc.Start();
                string wslAppDir = wslPathProc.StandardOutput.ReadToEnd().Trim();
                wslPathProc.WaitForExit();

                string wslPathsFile = $"{wslAppDir}/win_paths.txt";
                string wslResultsFile = $"{wslAppDir}/results.csv";

                // 4. 파이썬 예측 스크립트 (직접 파일 읽고 쓰기)
                string pythonPredictScript =
                    "import tensorflow as tf; import numpy as np; from PIL import Image; import os; " +
                    $"model = tf.keras.models.load_model('{modelFile}'); " +
                    $"with open('{wslPathsFile}', 'r') as f: paths = f.read().splitlines(); " +
                    "results = []; " +
                    "for p in paths: " +
                    "    if not os.path.exists(p): results.append('0,0'); continue; " +
                    "    img = np.array(Image.open(p).convert('RGB').resize((160, 120))) / 255.0; " +
                    "    pred = model.predict(img[None, :], verbose=0); " +
                    "    results.append(f'{pred[0][0][0]},{pred[1][0][0]}'); " +
                    $"with open('{wslResultsFile}', 'w') as f: f.write('\\n'.join(results))";

                // 5. WSL 명령어 실행
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "wsl.exe";
                start.Arguments = $"bash -i -c \"conda activate {envName} && cd {projectPath} && python -c \\\"{pythonPredictScript}\\\"\"";
                start.UseShellExecute = false;
                start.RedirectStandardError = true; // 에러 확인용
                start.CreateNoWindow = true;

                Process process = Process.Start(start);
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    txtTrainingLog?.AppendText($"[파이썬 에러] {error}\r\n");
                }

                // 6. 결과 파일(results.csv) 읽기
                if (File.Exists(winResultsFile))
                {
                    string[] lines = File.ReadAllLines(winResultsFile);
                    for (int i = 0; i < Math.Min(lines.Length, _allData.Count); i++)
                    {
                        string[] vals = lines[i].Split(',');
                        if (vals.Length == 2)
                        {
                            _allData[i].PredictedSteering = double.Parse(vals[0]);
                            _allData[i].PredictedSpeed = double.Parse(vals[1]) * 100;
                        }
                    }
                    UpdateCharts();
                    txtTrainingLog?.AppendText($"[{DateTime.Now:HH:mm:ss}] 예측 완료! 차트를 확인하세요.\r\n");
                }
                else
                {
                    txtTrainingLog?.AppendText($"[{DateTime.Now:HH:mm:ss}] 오류: results.csv가 생성되지 않았습니다.\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("테스트 실패: " + ex.Message);
            }
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

        #region [5. 필터링, 삭제, 마커 및 실시간 슬라이더 로직]

        // 🛠️ 추가: 데이터 관리 탭 슬라이더 실시간 업데이트 메서드
        private void tbImageNavigator_Scroll(object sender, EventArgs e)
        {
            _currentIndex = tbImageNavigator.Value;
            UpdateDisplay();
        }

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

        // 마커 배치를 위한 MouseUp 로직은 유지
        private void tbImageNavigator_MouseUp(object sender, MouseEventArgs e)
        {
            _currentIndex = tbImageNavigator.Value;
            UpdateDisplay();
            if (_isRangeSettingMode) AddMarker();
        }

        #endregion

        #region [6. 차트 업데이트 및 기타]

        private void UpdateCharts()
        {
            if (_allData.Count == 0) return;
            int step = Math.Max(1, _allData.Count / 100);
            int maxIdx = _allData.Count - 1;

            if (chtSteeringValue != null) chtSteeringValue.Series[0].Points.Clear();
            if (chtSpeedValue != null) chtSpeedValue.Series[0].Points.Clear();

            if (chtTestSteeringValue != null) { chtTestSteeringValue.Series[0].Points.Clear(); chtTestSteeringValue.Series[1].Points.Clear(); }
            if (chtTestSpeedValue != null) { chtTestSpeedValue.Series[0].Points.Clear(); chtTestSpeedValue.Series[1].Points.Clear(); }

            for (int i = 0; i < _allData.Count; i += step)
            {
                var d = _allData[i];
                if (chtSteeringValue != null) chtSteeringValue.Series[0].Points.AddXY(d.Index, d.Steering);
                if (chtSpeedValue != null) chtSpeedValue.Series[0].Points.AddXY(d.Index, d.Speed);

                if (chtTestSteeringValue != null)
                {
                    chtTestSteeringValue.Series["Actual"].Points.AddXY(d.Index, d.Steering);
                    chtTestSteeringValue.Series[0].Points.AddXY(d.Index, d.PredictedSteering);
                }
                if (chtTestSpeedValue != null)
                {
                    chtTestSpeedValue.Series["Actual"].Points.AddXY(d.Index, d.Speed);
                    chtTestSpeedValue.Series[0].Points.AddXY(d.Index, d.PredictedSpeed);
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
        public double PredictedSteering { get; set; }
        public double PredictedSpeed { get; set; }
    }
}