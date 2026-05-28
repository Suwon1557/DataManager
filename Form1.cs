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
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices; // Required for native logical string comparison.

namespace DataManager
{
    // Implementation note.
    public partial class Form1 : Form
    {
        // Implementation note.
        private List<DrivingData> _allData = new List<DrivingData>();
        private readonly Stack<DeleteAction> _deleteUndoStack = new Stack<DeleteAction>();
        private int _currentIndex = -1;
        private bool _isReversed = false;
        private bool _isRangeSettingMode = false;
        private readonly Color _folderPathTextColor = Color.FromArgb(238, 243, 249);
        private readonly Color _folderPathWarningColor = Color.FromArgb(248, 113, 113);
        private readonly List<Panel> _imageRangeMarkers = new List<Panel>();
        private System.Windows.Forms.Timer _playTimer = new System.Windows.Forms.Timer();
        private const string UiFontFamily = "留묒? 怨좊뵓";

        private class DeleteAction
        {
            public List<DrivingData> Items { get; set; } = new List<DrivingData>();
            public int RestoreIndex { get; set; }
            public List<FileMoveInfo> MovedFiles { get; set; } = new List<FileMoveInfo>();
            public List<CatalogBackupInfo> CatalogBackups { get; set; } = new List<CatalogBackupInfo>();
        }

        private class FileMoveInfo
        {
            public string OriginalPath { get; set; } = "";
            public string TrashPath { get; set; } = "";
        }

        private class CatalogBackupInfo
        {
            public string OriginalPath { get; set; } = "";
            public string BackupPath { get; set; } = "";
        }

        // Required for native logical string comparison.
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

        public Form1()
        {
            InitializeComponent();

            if (lvDataItems.Columns.Count == 0)
            {
                lvDataItems.Columns.Add("\uBC88\uD638", 70);
                lvDataItems.Columns.Add("\uC774\uBBF8\uC9C0 \uD30C\uC77C\uBA85", 320);
            }
            AdjustDataListColumns();

            // Configure the playback speed range.
            tbPlaybackSpeed.Minimum = 25;
            tbPlaybackSpeed.Maximum = 200;
            tbPlaybackSpeed.Value = 100;

            InitializeDataInfoGrid();
            UpdatePlaybackSpeedLabel();
            ApplyPolishedTheme();
            ConfigureResponsiveLayout();

            _playTimer.Tick += PlayTimer_Tick;
            this.Shown += Form1_Shown;
            this.Resize += Form1_Resize;

            // Wire runtime event handlers.
            lvDataItems.SelectedIndexChanged += lvDataItems_SelectedIndexChanged;

            // Wire runtime event handlers.
            tbImageNavigator.Scroll += tbImageNavigator_Scroll;

            // Implementation note.
            if (tbTestImageNavigator != null)
            {
                tbTestImageNavigator.Scroll -= tbTestImageNavigator_Scroll_1;
                tbTestImageNavigator.Scroll += tbTestImageNavigator_Scroll_1;
            }
        }

        #region [1. Chart layout]

        private void Form1_Shown(object? sender, EventArgs e)
        {
            dgvDataInfo.ClearSelection();
            _isRangeSettingMode = false;
            pnlImageRangeMarker.Visible = false;

            this.AutoScroll = true;

            // Find the main tab control.
            var tabControl = this.Controls.OfType<TabControl>().FirstOrDefault();
            if (tabControl != null && tabControl.TabPages.Count >= 2)
            {
                TabPage page1 = tabControl.TabPages[0]; // Find the main tab control.
                TabPage page2 = tabControl.TabPages[1]; // Create fallback charts for the data tab.

                // Create fallback charts for the data tab.
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

                // Create fallback charts for the training tab.
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

            // Apply chart layout and styling.
            EnsureDataChartsLayout();
            EnsureTestChartsLayout();
            SetupSafeChart(chtSteeringValue, "Steering Data", Color.DodgerBlue, "실제 조향값");
            SetupSafeChart(chtSpeedValue, "Speed Data", Color.OrangeRed, "실제 속도값");
            SetupSafeChart(chtTestSteeringValue, "실제/예측 조향값 비교 Chart", Color.Blue, "예측값", "Actual", Color.Green);
            SetupSafeChart(chtTestSpeedValue, "실제/예측 속도값 비교 Chart", Color.Red, "예측값", "Actual", Color.Green);

            UpdateCharts();
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            EnsureDataChartsLayout();
            EnsureTestChartsLayout();
            AdjustDataListColumns();
        }

        private void ConfigureResponsiveLayout()
        {
            tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbDataLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCheckDataIntegrity.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            pbDataPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvDataInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lvDataItems.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbImageNavigator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSetRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            gbTrainingSetup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbModelTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtTrainingLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbTestImageNavigator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LayoutTestImageNavigatorAtBottom();
        }

        private void EnsureDataChartsLayout()
        {
            TableLayoutPanel layout = GetOrCreateChartLayout(gbDataContent, "tlpDataCharts", 2, 1);
            int chartTop = Math.Max(tbImageNavigator.Bottom + 16, pbDataPreview.Bottom + 16);
            layout.Bounds = new Rectangle(12, chartTop, Math.Max(100, gbDataContent.ClientSize.Width - 24), Math.Max(160, gbDataContent.ClientSize.Height - chartTop - 18));
            layout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            if (chtSteeringValue == null) chtSteeringValue = new Chart();
            if (chtSpeedValue == null) chtSpeedValue = new Chart();

            AddChartToLayout(layout, chtSteeringValue, 0, 0);
            AddChartToLayout(layout, chtSpeedValue, 1, 0);
        }

        private void EnsureTestChartsLayout()
        {
            LayoutTestImageNavigatorAtBottom();

            TableLayoutPanel layout = GetOrCreateChartLayout(gbModelTest, "tlpTestCharts", 1, 2);
            int chartLeft = pbTestPreview.Right + 20;
            int chartTop = pbTestPreview.Top;
            int chartBottom = tbTestImageNavigator.Top - 12;
            layout.Bounds = new Rectangle(chartLeft, chartTop, Math.Max(100, gbModelTest.ClientSize.Width - chartLeft - 12), Math.Max(160, chartBottom - chartTop));
            layout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            if (chtTestSteeringValue == null) chtTestSteeringValue = new Chart();
            if (chtTestSpeedValue == null) chtTestSpeedValue = new Chart();

            AddChartToLayout(layout, chtTestSteeringValue, 0, 0);
            AddChartToLayout(layout, chtTestSpeedValue, 0, 1);
        }

        private void LayoutTestImageNavigatorAtBottom()
        {
            if (gbModelTest == null || tbTestImageNavigator == null) return;

            const int margin = 12;
            tbTestImageNavigator.Left = margin;
            tbTestImageNavigator.Width = Math.Max(100, gbModelTest.ClientSize.Width - (margin * 2));
            tbTestImageNavigator.Top = Math.Max(pbTestPreview.Bottom + 24, gbModelTest.ClientSize.Height - tbTestImageNavigator.Height - margin);
        }

        private TableLayoutPanel GetOrCreateChartLayout(Control parent, string name, int columnCount, int rowCount)
        {
            TableLayoutPanel? layout = parent.Controls.Find(name, false).OfType<TableLayoutPanel>().FirstOrDefault();
            if (layout == null)
            {
                layout = new TableLayoutPanel
                {
                    Name = name,
                    ColumnCount = columnCount,
                    RowCount = rowCount,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty
                };
                parent.Controls.Add(layout);
            }

            layout.ColumnStyles.Clear();
            layout.RowStyles.Clear();
            for (int i = 0; i < columnCount; i++)
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnCount));
            for (int i = 0; i < rowCount; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));

            return layout;
        }

        private void AddChartToLayout(TableLayoutPanel layout, Chart chart, int column, int row)
        {
            chart.Dock = DockStyle.Fill;
            chart.Margin = new Padding(4);
            layout.Controls.Add(chart, column, row);
        }

        private void SetupSafeChart(Chart? chart, string titleName, Color c1, string s1Name, string? s2Name = null, Color? c2 = null)
        {
            if (chart == null) return;
            ChartArea ca = chart.ChartAreas.Count > 0 ? chart.ChartAreas[0] : chart.ChartAreas.Add("Main");
            ca.AxisX.Title = "";
            ca.AxisX.LabelStyle.Format = "0;0;0"; // Avoid displaying negative zero.

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

        #region [2. Data loading]

        private void btnSelectAdd_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                if (fbd.ShowDialog() == DialogResult.OK) { txtFolderPath.Text = fbd.SelectedPath; LoadData(fbd.SelectedPath); }
        }

        private void LoadData(string path)
        {
            _allData.Clear();
            lvDataItems.Items.Clear();
            txtFolderPath.ForeColor = _folderPathTextColor;

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

            RefreshDataListView();

            if (_allData.Count == 0)
            {
                ShowNoDataMessage();
                return;
            }

            _currentIndex = 0;
            tbImageNavigator.Maximum = Math.Max(0, _allData.Count - 1);
            if (tbTestImageNavigator != null) tbTestImageNavigator.Maximum = Math.Max(0, _allData.Count - 1);

            UpdateDisplay();
            UpdateCharts();

            // Show the first loaded image in the test preview.
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
                for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
                {
                    var line = lines[lineIndex];
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
                                Speed = throttle * 100,
                                CatalogFilePath = catFile,
                                CatalogImageName = imgName,
                                CatalogLineNumber = lineIndex
                            });
                        }
                    }
                    catch { }
                }
            }
        }

        private void btnCheckDataIntegrity_Click(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;
            bool isDup = _allData.GroupBy(x => x.Index).Any(g => g.Count() > 1);
            bool isMissingFile = _allData.Any(x => !File.Exists(x.ImagePath));
            MessageBox.Show($"중복 인덱스: {(isDup ? "문제 있음" : "정상")}\n이미지 파일 누락: {(isMissingFile ? "문제 있음" : "정상")}", "무결성 검사");
        }

        #endregion

        #region [3. Navigation and playback]

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

        private bool EnsureDataLoaded()
        {
            if (_allData.Count > 0) return true;
            ShowNoDataMessage();
            return false;
        }

        private void ShowNoDataMessage()
        {
            _playTimer.Stop();
            txtFolderPath.ForeColor = _folderPathWarningColor;
            txtFolderPath.Text = "\uB370\uC774\uD130\uAC00 \uC5C6\uC2B5\uB2C8\uB2E4. \uB370\uC774\uD130\uB97C \uAC00\uC838\uC624\uC138\uC694";
        }

        private void StartPlayback() { if (!EnsureDataLoaded()) return; _playTimer.Interval = (int)(150 / (tbPlaybackSpeed.Value / 100.0)); _playTimer.Start(); }
        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isReversed) { if (_currentIndex > 0) _currentIndex--; else _playTimer.Stop(); }
            else { if (_currentIndex < _allData.Count - 1) _currentIndex++; else _playTimer.Stop(); }
            UpdateDisplay();
        }

        private void btnPlay_Click_1(object sender, EventArgs e) { _isReversed = false; StartPlayback(); }
        private void btnReverse_Click(object sender, EventArgs e) { _isReversed = true; StartPlayback(); }
        private void btnStop_Click(object sender, EventArgs e) { if (!EnsureDataLoaded()) return; _playTimer.Stop(); }

        #endregion

        #region [4. Training and testing]

        private string BuildWslCondaCommand(string envName, string command)
        {
            string condaSetup =
                "if [ -f ~/miniconda3/etc/profile.d/conda.sh ]; then source ~/miniconda3/etc/profile.d/conda.sh; " +
                "elif [ -f ~/anaconda3/etc/profile.d/conda.sh ]; then source ~/anaconda3/etc/profile.d/conda.sh; " +
                "else echo 'conda not found under ~/miniconda3 or ~/anaconda3' >&2; exit 1; fi";

            return $"{condaSetup} && conda activate {envName} && {command}";
        }

        private string GetWslDistroArgument()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "wsl.exe";
                start.Arguments = "-l -q";
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = true;

                using Process process = Process.Start(start);
                string output = process.StandardOutput.ReadToEnd().Replace("\0", "");
                process.WaitForExit();

                string[] distros = output
                    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(d => d.Trim())
                    .ToArray();

                if (distros.Any(d => d.Equals("Ubuntu-24.04", StringComparison.OrdinalIgnoreCase)))
                {
                    return "-d Ubuntu-24.04 -- ";
                }

                if (distros.Any(d => d.Equals("Ubuntu", StringComparison.OrdinalIgnoreCase)))
                {
                    return "-d Ubuntu -- ";
                }
            }
            catch
            {
            }

            return "";
        }

        private void AppendTrainingLog(string message)
        {
            if (txtTrainingLog == null) return;

            message = CleanTrainingLogMessage(message);
            if (string.IsNullOrWhiteSpace(message)) return;

            if (txtTrainingLog.InvokeRequired)
            {
                txtTrainingLog.BeginInvoke(new Action<string>(AppendTrainingLog), message);
                return;
            }

            txtTrainingLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }

        private string CleanTrainingLogMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return string.Empty;

            var cleaned = new System.Text.StringBuilder(message.Length);
            bool inEscapeSequence = false;

            foreach (char ch in message)
            {
                if (inEscapeSequence)
                {
                    if (ch >= '@' && ch <= '~')
                    {
                        inEscapeSequence = false;
                    }
                    continue;
                }

                if (ch == '\u001b')
                {
                    inEscapeSequence = true;
                    continue;
                }

                if (ch == '\b')
                {
                    if (cleaned.Length > 0) cleaned.Length--;
                    continue;
                }

                if (ch == '\r') continue;
                if (char.IsControl(ch) && ch != '\t') continue;

                cleaned.Append(ch);
            }

            return cleaned.ToString().Trim();
        }

        private async void btnTrain_Click(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;

            try
            {
                btnTrain.Enabled = false;

                // Configure the WSL training environment.
                string envName = "e2e_env";
                string projectPath = "~/mysim";

                // Build the training commands.
                string installCmd = "pip install numpy==1.24.3 pandas==2.0.3 tensorflow==2.13.0 albumentations imgaug";
                string trainCmd = "python train.py --tub ./data --model ./models/mypilot.h5";

                // Configure the WSL training environment.
                string bashCmd = BuildWslCondaCommand(envName, $"cd {projectPath} && {installCmd} && {trainCmd}");

                // Configure the WSL process.
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "wsl.exe";

                // Implementation note.
                string wslDistroArgument = GetWslDistroArgument();
                start.Arguments = wslDistroArgument + "bash -lc \"" + bashCmd + "\"";

                // Capture process output in the app log.
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;
                start.CreateNoWindow = true;

                using Process process = new Process();
                process.StartInfo = start;
                process.OutputDataReceived += (_, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data)) AppendTrainingLog(args.Data);
                };
                process.ErrorDataReceived += (_, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data)) AppendTrainingLog(args.Data);
                };
                AppendTrainingLog("WSL \uD559\uC2B5 \uD504\uB85C\uC138\uC2A4\uB97C \uC2DC\uC791\uD588\uC2B5\uB2C8\uB2E4.");
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                await process.WaitForExitAsync();
                AppendTrainingLog($"\uD559\uC2B5 \uD504\uB85C\uC138\uC2A4\uAC00 \uC885\uB8CC\uB418\uC5C8\uC2B5\uB2C8\uB2E4. ExitCode={process.ExitCode}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("?꾨줈?몄뒪 ?ㅽ뻾 以??ㅻ쪟: " + ex.Message);
            }
            finally
            {
                btnTrain.Enabled = true;
            }
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;

            try
            {
                AppendTrainingLog("테스트 세션 시작 (경로 동기화 중...)");
                string envName = "e2e_env";
                string projectPath = "~/mysim";
                string modelFile = "models/mypilot.h5";

                // Prepare temporary files for test predictions.
                string appDir = Application.StartupPath;
                string winPathsFile = Path.Combine(appDir, "win_paths.txt");
                string winResultsFile = Path.Combine(appDir, "results.csv");

                // Convert selected image paths for WSL.
                var linuxPaths = _allData.Select(d =>
                {
                    string drive = Path.GetPathRoot(d.ImagePath).Substring(0, 1).ToLower();
                    string subPath = d.ImagePath.Substring(Path.GetPathRoot(d.ImagePath).Length).Replace("\\", "/");
                    return $"/mnt/{drive}/{subPath}";
                }).ToList();
                File.WriteAllLines(winPathsFile, linuxPaths);

                // Resolve the app output directory inside WSL.
                Process wslPathProc = new Process();
                wslPathProc.StartInfo.FileName = "wsl.exe";
                string wslDistroArgument = GetWslDistroArgument();
                wslPathProc.StartInfo.Arguments = $"{wslDistroArgument}wslpath '{appDir.Replace("\\", "/")}'";
                wslPathProc.StartInfo.UseShellExecute = false;
                wslPathProc.StartInfo.RedirectStandardOutput = true;
                wslPathProc.Start();
                string wslAppDir = wslPathProc.StandardOutput.ReadToEnd().Trim();
                wslPathProc.WaitForExit();

                string wslPathsFile = $"{wslAppDir}/win_paths.txt";
                string wslResultsFile = $"{wslAppDir}/results.csv";

                // Build the Python prediction script.
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

                // Configure the WSL process.
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "wsl.exe";
                string bashCmd = BuildWslCondaCommand(envName, $"cd {projectPath} && python -c \\\"{pythonPredictScript}\\\"");
                start.Arguments = $"{wslDistroArgument}bash -lc \"{bashCmd}\"";
                start.UseShellExecute = false;
                start.RedirectStandardError = true; // Implementation note.
                start.CreateNoWindow = true;

                Process process = Process.Start(start);
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    txtTrainingLog?.AppendText($"[?뚯씠???먮윭] {error}\r\n");
                }

                // Prepare temporary files for test predictions.
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
                    AppendTrainingLog("예측 완료! 차트를 확인하세요.");
                }
                else
                {
                    AppendTrainingLog("\uC624\uB958: results.csv\uAC00 \uC0DD\uC131\uB418\uC9C0 \uC54A\uC558\uC2B5\uB2C8\uB2E4.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("?뚯뒪???ㅽ뙣: " + ex.Message);
            }
        }

        private void tbTestImageNavigator_Scroll_1(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded() || tbTestImageNavigator == null || pbTestPreview == null) return;
            int idx = tbTestImageNavigator.Value;
            if (idx >= 0 && idx < _allData.Count && File.Exists(_allData[idx].ImagePath))
            {
                using (var fs = new FileStream(_allData[idx].ImagePath, FileMode.Open, FileAccess.Read))
                    pbTestPreview.Image = Image.FromStream(fs);
            }
        }

        #endregion

        #region [5. Filtering, deletion, and markers]

        // Wire runtime event handlers.
        private void tbImageNavigator_Scroll(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;
            _currentIndex = tbImageNavigator.Value;
            UpdateDisplay();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;
            var targets = _allData.Where(x => x.Steering == 0 || x.Speed == 0).ToList();
            if (targets.Count > 0)
            {
                DeleteDataItems(targets, _allData.IndexOf(targets[0]));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded() || _currentIndex < 0) return;
            if (_imageRangeMarkers.Count == 2)
            {
                int s = (int)_imageRangeMarkers[0].Tag, f = (int)_imageRangeMarkers[1].Tag;
                int min = Math.Min(s, f), max = Math.Max(s, f);
                DeleteDataItems(_allData.GetRange(min, max - min + 1), min);
                ClearMarkers();
            }
            else
            {
                DeleteDataItems(new List<DrivingData> { _allData[_currentIndex] }, _currentIndex);
            }
        }

        private void btnCancelDelete_Click(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;
            if (_deleteUndoStack.Count == 0) return;

            DeleteAction action = _deleteUndoStack.Pop();
            try
            {
                foreach (var file in action.MovedFiles)
                {
                    if (!File.Exists(file.TrashPath)) continue;

                    string? dir = Path.GetDirectoryName(file.OriginalPath);
                    if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

                    string originalPath = file.OriginalPath;
                    if (File.Exists(file.OriginalPath))
                    {
                        string duplicatePath = GetUniquePath(file.OriginalPath);
                        File.Move(file.TrashPath, duplicatePath);
                        file.OriginalPath = duplicatePath;
                        foreach (var item in action.Items.Where(x => x.ImagePath == originalPath))
                            item.ImagePath = duplicatePath;
                    }
                    else
                    {
                        File.Move(file.TrashPath, file.OriginalPath);
                    }
                }

                RestoreCatalogFiles(action);

                int insertIndex = Math.Max(0, Math.Min(action.RestoreIndex, _allData.Count));
                _allData.InsertRange(insertIndex, action.Items);
                _currentIndex = insertIndex;
                RefreshUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("蹂듦뎄 以??ㅻ쪟: " + ex.Message);
            }
        }

        private void DeleteDataItems(List<DrivingData> items, int restoreIndex)
        {
            if (items.Count == 0) return;

            ReleasePreviewImages();

            DeleteAction action = new DeleteAction
            {
                Items = new List<DrivingData>(items),
                RestoreIndex = restoreIndex
            };

            try
            {
                string trashDir = CreateTrashDirectory();
                foreach (var item in items)
                {
                    if (!File.Exists(item.ImagePath)) continue;

                    string trashPath = GetUniquePath(Path.Combine(trashDir, Path.GetFileName(item.ImagePath)));
                    File.Move(item.ImagePath, trashPath);
                    action.MovedFiles.Add(new FileMoveInfo
                    {
                        OriginalPath = item.ImagePath,
                        TrashPath = trashPath
                    });
                }

                RemoveCatalogLines(action, items, trashDir);

                foreach (var item in items)
                    _allData.Remove(item);

                _deleteUndoStack.Push(action);
                _currentIndex = Math.Max(0, Math.Min(restoreIndex, _allData.Count - 1));
                RefreshUI();
            }
            catch (Exception ex)
            {
                RestoreCatalogFiles(action);
                RollbackMovedFiles(action);
                MessageBox.Show("??젣 以??ㅻ쪟: " + ex.Message);
            }
        }

        private void RemoveCatalogLines(DeleteAction action, List<DrivingData> items, string trashDir)
        {
            var catalogItems = items
                .Where(x => !string.IsNullOrWhiteSpace(x.CatalogFilePath) && x.CatalogLineNumber >= 0)
                .GroupBy(x => x.CatalogFilePath)
                .ToList();

            foreach (var group in catalogItems)
            {
                string catalogPath = group.Key;
                if (!File.Exists(catalogPath)) continue;

                BackupCatalogFile(action, catalogPath, trashDir);

                var imageNamesToRemove = group
                    .Select(x => x.CatalogImageName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
                var lineNumbersToRemove = group
                    .Select(x => x.CatalogLineNumber)
                    .ToHashSet();

                string[] lines = File.ReadAllLines(catalogPath);
                var keptLines = lines
                    .Where((line, index) => !ShouldRemoveCatalogLine(line, index, imageNamesToRemove, lineNumbersToRemove))
                    .ToArray();

                File.WriteAllLines(catalogPath, keptLines);
                UpdateCatalogManifest(action, catalogPath, keptLines, trashDir);
            }
        }

        private void BackupCatalogFile(DeleteAction action, string originalPath, string trashDir)
        {
            if (!File.Exists(originalPath)) return;
            if (action.CatalogBackups.Any(x => x.OriginalPath.Equals(originalPath, StringComparison.OrdinalIgnoreCase))) return;

            string backupPath = GetUniquePath(Path.Combine(trashDir, Path.GetFileName(originalPath) + ".bak"));
            File.Copy(originalPath, backupPath);
            action.CatalogBackups.Add(new CatalogBackupInfo
            {
                OriginalPath = originalPath,
                BackupPath = backupPath
            });
        }

        private void UpdateCatalogManifest(DeleteAction action, string catalogPath, string[] catalogLines, string trashDir)
        {
            string catalogManifestPath = catalogPath + "_manifest";
            if (!File.Exists(catalogManifestPath)) return;

            BackupCatalogFile(action, catalogManifestPath, trashDir);

            try
            {
                using JsonDocument doc = JsonDocument.Parse(File.ReadAllText(catalogManifestPath));
                var root = doc.RootElement;
                double createdAt = root.TryGetProperty("created_at", out var createdAtElement)
                    ? createdAtElement.GetDouble()
                    : 0;
                string path = root.TryGetProperty("path", out var pathElement)
                    ? pathElement.GetString() ?? Path.GetFileName(catalogManifestPath)
                    : Path.GetFileName(catalogManifestPath);
                int startIndex = root.TryGetProperty("start_index", out var startIndexElement)
                    ? startIndexElement.GetInt32()
                    : 0;

                var manifest = new
                {
                    created_at = createdAt,
                    line_lengths = catalogLines.Select(line => line.Length).ToArray(),
                    path,
                    start_index = startIndex
                };

                File.WriteAllText(catalogManifestPath, JsonSerializer.Serialize(manifest));
            }
            catch
            {
                File.WriteAllText(catalogManifestPath, JsonSerializer.Serialize(new
                {
                    line_lengths = catalogLines.Select(line => line.Length).ToArray(),
                    path = Path.GetFileName(catalogManifestPath),
                    start_index = 0
                }));
            }
        }

        private bool ShouldRemoveCatalogLine(string line, int index, HashSet<string> imageNamesToRemove, HashSet<int> lineNumbersToRemove)
        {
            if (imageNamesToRemove.Count == 0) return lineNumbersToRemove.Contains(index);

            try
            {
                using JsonDocument doc = JsonDocument.Parse(line);
                string? imgName = doc.RootElement.GetProperty("cam/image_array").GetString();
                return imgName != null && imageNamesToRemove.Contains(imgName);
            }
            catch
            {
                return false;
            }
        }

        private void RestoreCatalogFiles(DeleteAction action)
        {
            foreach (var backup in action.CatalogBackups)
            {
                try
                {
                    if (!File.Exists(backup.BackupPath)) continue;
                    string? dir = Path.GetDirectoryName(backup.OriginalPath);
                    if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
                    File.Copy(backup.BackupPath, backup.OriginalPath, true);
                }
                catch { }
            }
        }

        private string CreateTrashDirectory()
        {
            string basePath = Directory.Exists(txtFolderPath.Text) ? txtFolderPath.Text : Application.StartupPath;
            string trashRoot = Path.Combine(basePath, ".datamanager_trash");
            string trashDir = Path.Combine(trashRoot, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"));
            Directory.CreateDirectory(trashDir);

            try
            {
                File.SetAttributes(trashRoot, File.GetAttributes(trashRoot) | FileAttributes.Hidden);
            }
            catch { }

            return trashDir;
        }

        private string GetUniquePath(string path)
        {
            if (!File.Exists(path) && !Directory.Exists(path)) return path;

            string dir = Path.GetDirectoryName(path) ?? "";
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            int n = 1;
            string candidate;
            do
            {
                candidate = Path.Combine(dir, $"{name}_{n}{ext}");
                n++;
            } while (File.Exists(candidate) || Directory.Exists(candidate));

            return candidate;
        }

        private void RollbackMovedFiles(DeleteAction action)
        {
            foreach (var file in action.MovedFiles)
            {
                try
                {
                    if (!File.Exists(file.TrashPath) || File.Exists(file.OriginalPath)) continue;
                    string? dir = Path.GetDirectoryName(file.OriginalPath);
                    if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
                    File.Move(file.TrashPath, file.OriginalPath);
                }
                catch { }
            }
        }

        private void ReleasePreviewImages()
        {
            Image? dataImage = pbDataPreview.Image;
            pbDataPreview.Image = null;
            dataImage?.Dispose();

            if (pbTestPreview != null)
            {
                Image? testImage = pbTestPreview.Image;
                pbTestPreview.Image = null;
                testImage?.Dispose();
            }
        }

        private void RefreshDataListView()
        {
            lvDataItems.BeginUpdate();
            lvDataItems.Items.Clear();
            foreach (var d in _allData.OrderBy(x => x.Index))
            {
                var item = new ListViewItem(d.Index.ToString());
                item.SubItems.Add(Path.GetFileName(d.ImagePath));
                lvDataItems.Items.Add(item);
            }
            lvDataItems.EndUpdate();
        }

        private void RefreshUI() { _currentIndex = Math.Max(0, Math.Min(_currentIndex, _allData.Count - 1)); tbImageNavigator.Maximum = Math.Max(0, _allData.Count - 1); RefreshDataListView(); UpdateDisplay(); UpdateCharts(); }

        private void AddMarker()
        {
            Panel m = new Panel { BackColor = Color.FromArgb(245, 176, 65), Size = pnlImageRangeMarker.Size, Tag = _currentIndex };
            m.Left = GetImageNavigatorMarkerLeft(_currentIndex, m.Size); m.Top = tbImageNavigator.Top + 13;
            _imageRangeMarkers.Add(m);
            if (_imageRangeMarkers.Count > 2) { gbDataContent?.Controls.Remove(_imageRangeMarkers[0]); _imageRangeMarkers.RemoveAt(0); }
            gbDataContent?.Controls.Add(m); m.BringToFront();
        }

        private void ClearMarkers() { foreach (var m in _imageRangeMarkers) gbDataContent?.Controls.Remove(m); _imageRangeMarkers.Clear(); _isRangeSettingMode = false; }
        private int GetImageNavigatorMarkerLeft(int value, Size markerSize) { int min = tbImageNavigator.Minimum, max = tbImageNavigator.Maximum; double ratio = (max == min) ? 0 : (double)(value - min) / (max - min); return tbImageNavigator.Left + 10 + (int)((tbImageNavigator.Width - 20) * ratio) - (markerSize.Width / 2); }

        // Handle marker placement after mouse release.
        private void tbImageNavigator_MouseUp(object sender, MouseEventArgs e)
        {
            _currentIndex = tbImageNavigator.Value;
            UpdateDisplay();
            if (_isRangeSettingMode) AddMarker();
        }

        #endregion

        #region [6. Chart updates and helpers]

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

        private void ApplyPolishedTheme()
        {
            Color ink = Color.FromArgb(18, 24, 38);
            Color surface = Color.FromArgb(28, 36, 54);
            Color panel = Color.FromArgb(39, 50, 72);
            Color panelSoft = Color.FromArgb(49, 62, 88);
            Color field = Color.FromArgb(246, 248, 252);
            Color text = Color.FromArgb(238, 243, 249);
            Color darkText = Color.FromArgb(31, 41, 55);
            Color gold = Color.FromArgb(245, 176, 65);
            Color teal = Color.FromArgb(45, 212, 191);
            Color coral = Color.FromArgb(248, 113, 113);
            Color line = Color.FromArgb(103, 119, 148);

            BackColor = ink;
            lblTitle.ForeColor = gold;

            tcMain.BackColor = ink;
            tcMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            tcMain.DrawItem -= tcMain_DrawItem;
            tcMain.DrawItem += tcMain_DrawItem;

            tpDataManager.BackColor = surface;
            tpDataManager.UseVisualStyleBackColor = false;
            tpTrainingTest.BackColor = surface;
            tpTrainingTest.UseVisualStyleBackColor = false;

            StyleGroupBox(gbDataLoad, panel, gold);
            StyleGroupBox(gbDataContent, panel, gold);
            StyleGroupBox(gbTrainingSetup, panel, gold);
            StyleGroupBox(gbModelTest, panel, gold);

            StyleButton(btnSelectFolder, teal, Color.FromArgb(6, 42, 43), teal);
            StyleButton(btnTrain, teal, Color.FromArgb(6, 42, 43), teal);
            StyleButton(btnStartTest, teal, Color.FromArgb(6, 42, 43), teal);
            StyleButton(btnFilter, panelSoft, teal, Color.FromArgb(255, 114, 16));

            StyleButton(btnCheckDataIntegrity, panelSoft, text, teal);
            StyleButton(btnPlay, panelSoft, text, teal);
            StyleButton(btnStop, panelSoft, text, teal);
            StyleButton(btnReverse, panelSoft, text, teal);
            StyleButton(btnSetRange, panelSoft, text, teal);
            StyleButton(btnCancelRange, panelSoft, gold, gold);
            StyleButton(btnDelete, panelSoft, coral, coral);
            StyleButton(btnCancelDelete, Color.FromArgb(22, 30, 46), teal, teal);
            txtFolderPath.BackColor = field;
            txtFolderPath.ForeColor = darkText;
            txtFolderPath.BorderStyle = BorderStyle.FixedSingle;
            txtFolderPath.ReadOnly = false;
            txtFolderPath.BackColor = Color.FromArgb(22, 30, 46);
            txtFolderPath.ForeColor = text;
            txtFolderPath.ReadOnly = true;
            txtTrainingLog.BackColor = Color.FromArgb(12, 18, 30);
            txtTrainingLog.ForeColor = text;
            txtTrainingLog.BorderStyle = BorderStyle.FixedSingle;

            StylePreview(pbDataPreview, line);
            StylePreview(pbTestPreview, line);
            StyleDataGrid(dgvDataInfo, Color.FromArgb(22, 30, 46), panelSoft, gold, line, text);
            StyleListView(lvDataItems, Color.FromArgb(22, 30, 46), panelSoft, gold, text, line);

            lblPlaybackSpeed.ForeColor = teal;
            tbPlaybackSpeed.BackColor = panel;
            tbImageNavigator.BackColor = panel;
            tbTestImageNavigator.BackColor = panel;
            pnlImageRangeMarker.BackColor = gold;

            ApplyTextPolish();
        }

        private void ApplyTextPolish()
        {
            Text = "Data Manager";
            btnFilter.Text = string.Empty;
            btnCheckDataIntegrity.Text = "무결성 검사";
            btnTrain.Text = string.Empty;
            btnStartTest.Text = "테스트 시작";
            btnSetRange.Text = "범위 설정";
            btnCancelRange.Text = "X";
            btnPlay.Text = ">>";
            btnStop.Text = "||";
            btnReverse.Text = "<<";

            dgvDataInfo.ColumnHeadersDefaultCellStyle.Font = new Font(
                dgvDataInfo.Font.FontFamily,
                dgvDataInfo.Font.Size,
                FontStyle.Bold);
            dgvDataInfo.DefaultCellStyle.Font = dgvDataInfo.Font;
            dgvDataInfo.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvDataInfo.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvDataInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDataInfo.ColumnHeadersHeight = Math.Max(dgvDataInfo.ColumnHeadersHeight, 48);
            dgvDataInfo.RowTemplate.Height = Math.Max(dgvDataInfo.RowTemplate.Height, 44);
            foreach (DataGridViewRow row in dgvDataInfo.Rows)
            {
                row.Height = Math.Max(row.Height, 44);
            }
            colDataName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDataName.Width = Math.Max(colDataName.Width, 180);
            colDataValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDataValue.MinimumWidth = Math.Max(colDataValue.MinimumWidth, 80);
        }

        private IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control child in root.Controls)
            {
                yield return child;
                foreach (Control grandChild in GetAllControls(child))
                    yield return grandChild;
            }
        }

        private void StyleGroupBox(GroupBox box, Color backColor, Color titleColor)
        {
            box.BackColor = backColor;
            box.ForeColor = titleColor;
        }

        private void StyleButton(Button button, Color backColor, Color foreColor, Color borderColor)
        {
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = borderColor;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.18f);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.08f);
            button.UseVisualStyleBackColor = false;
        }

        private void StylePreview(PictureBox preview, Color borderColor)
        {
            preview.BackColor = Color.FromArgb(12, 18, 30);
            preview.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StyleDataGrid(DataGridView grid, Color field, Color header, Color accent, Color line, Color cellText)
        {
            grid.BackgroundColor = field;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = line;
            grid.DefaultCellStyle.BackColor = field;
            grid.DefaultCellStyle.ForeColor = cellText;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 62, 88);
            grid.DefaultCellStyle.SelectionForeColor = accent;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 36, 54);
            grid.ColumnHeadersDefaultCellStyle.BackColor = header;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = accent;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = header;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            grid.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
        }

        private void StyleListView(ListView listView, Color field, Color header, Color accent, Color cellText, Color line)
        {
            listView.BackColor = field;
            listView.ForeColor = cellText;
            listView.BorderStyle = BorderStyle.FixedSingle;
            listView.GridLines = true;
            listView.OwnerDraw = true;
            listView.DrawColumnHeader -= lvDataItems_DrawColumnHeader;
            listView.DrawItem -= lvDataItems_DrawItem;
            listView.DrawSubItem -= lvDataItems_DrawSubItem;
            listView.DrawColumnHeader += lvDataItems_DrawColumnHeader;
            listView.DrawItem += lvDataItems_DrawItem;
            listView.DrawSubItem += lvDataItems_DrawSubItem;
            listView.Resize -= lvDataItems_Resize;
            listView.Resize += lvDataItems_Resize;
            AdjustDataListColumns();
        }

        private void AdjustDataListColumns()
        {
            if (lvDataItems.Columns.Count < 2) return;

            using Font headerFont = new Font(lvDataItems.Font.FontFamily, lvDataItems.Font.Size, FontStyle.Bold);
            int firstWidth = Math.Max(90, TextRenderer.MeasureText(lvDataItems.Columns[0].Text, headerFont).Width + 28);
            lvDataItems.Columns[0].Width = firstWidth;
            int fillWidth = Math.Max(120, lvDataItems.ClientSize.Width - firstWidth - 1);
            lvDataItems.Columns[1].Width = fillWidth;
        }

        private void lvDataItems_Resize(object? sender, EventArgs e)
        {
            AdjustDataListColumns();
        }

        private void lvDataItems_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush bg = new SolidBrush(Color.FromArgb(49, 62, 88)))
                e.Graphics.FillRectangle(bg, e.Bounds);
            using (Pen pen = new Pen(Color.FromArgb(103, 119, 148)))
                e.Graphics.DrawRectangle(pen, e.Bounds);

            TextRenderer.DrawText(
                e.Graphics,
                e.Header?.Text ?? string.Empty,
                new Font(lvDataItems.Font.FontFamily, lvDataItems.Font.Size, FontStyle.Bold),
                e.Bounds,
                Color.FromArgb(245, 176, 65),
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private void lvDataItems_DrawItem(object? sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false;
        }

        private void lvDataItems_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            Color back = e.Item.Selected ? Color.FromArgb(49, 62, 88) : Color.FromArgb(22, 30, 46);
            Color fore = e.Item.Selected ? Color.FromArgb(45, 212, 191) : Color.FromArgb(238, 243, 249);

            using (SolidBrush bg = new SolidBrush(back))
                e.Graphics.FillRectangle(bg, e.Bounds);
            using (Pen pen = new Pen(Color.FromArgb(103, 119, 148)))
                e.Graphics.DrawRectangle(pen, e.Bounds);

            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                lvDataItems.Font,
                e.Bounds,
                fore,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private void tcMain_DrawItem(object? sender, DrawItemEventArgs e)
        {
            TabPage page = tcMain.TabPages[e.Index];
            bool selected = e.Index == tcMain.SelectedIndex;
            Color back = selected ? Color.FromArgb(245, 176, 65) : Color.FromArgb(39, 50, 72);
            Color fore = selected ? Color.FromArgb(31, 41, 55) : Color.FromArgb(238, 243, 249);

            using (SolidBrush bg = new SolidBrush(back))
                e.Graphics.FillRectangle(bg, e.Bounds);

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tcMain.Font,
                e.Bounds,
                fore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        private void InitializeDataInfoGrid() { dgvDataInfo.Rows.Clear(); dgvDataInfo.Rows.Add("데이터 수", "0"); dgvDataInfo.Rows.Add("이미지", "0"); dgvDataInfo.Rows.Add("조향값", "0"); dgvDataInfo.Rows.Add("속도값", "0"); }
        private void UpdatePlaybackSpeedLabel() { if (lblPlaybackSpeed != null) lblPlaybackSpeed.Text = $"x{tbPlaybackSpeed.Value / 100.0:0.##}"; }
        private void tbPlaybackSpeed_Scroll(object sender, EventArgs e) { if (!EnsureDataLoaded()) return; UpdatePlaybackSpeedLabel(); if (_playTimer.Enabled) _playTimer.Interval = (int)(150 / (tbPlaybackSpeed.Value / 100.0)); }
        private void btnSetRange_Click(object sender, EventArgs e) { if (!EnsureDataLoaded()) return; _isRangeSettingMode = true; }
        private void btnCancelRange_Click(object sender, EventArgs e) { if (!EnsureDataLoaded()) return; ClearMarkers(); }
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

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class DrivingData
    {
        public int Index { get; set; }
        public string ImagePath { get; set; } = "";
        public double Steering { get; set; }
        public double Speed { get; set; }
        public double PredictedSteering { get; set; }
        public double PredictedSpeed { get; set; }
        public string CatalogFilePath { get; set; } = "";
        public string CatalogImageName { get; set; } = "";
        public int CatalogLineNumber { get; set; } = -1;
    }
}
