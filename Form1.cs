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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace DataManager
{
    public partial class Form1 : Form
    {
        private List<DrivingData> _allData = new List<DrivingData>();
        private List<DrivingData> _trainingData = new List<DrivingData>();
        private readonly Stack<DeleteAction> _deleteUndoStack = new Stack<DeleteAction>();
        private int _currentIndex = -1;
        private bool _isReversed = false;
        private bool _isRangeSettingMode = false;
        private int _listViewDragAnchorIndex = -1;
        private string _selectedDataFolderPath = "";
        private string _selectedTrainingSavePath = "";
        private bool _showTestOverlay = false;
        private bool _isTestRunning = false;
        private Process? _testProcess;
        private Button? _activeTestButton;
        private bool _testStopRequested = false;
        private Process? _trainingProcess;
        private bool _isTrainingRunning = false;
        private bool _trainingStopRequested = false;
        private SynchronizationContext? _uiContext;
        private Image? _trainButtonIdleImage;
        private string _selectedModelFile = "";
        private string _selectedPredictionResultsFile = "";
        private readonly Color _folderPathTextColor = Color.FromArgb(238, 243, 249);
        private readonly Color _folderPathWarningColor = Color.FromArgb(248, 113, 113);
        private readonly List<Panel> _imageRangeMarkers = new List<Panel>();
        private System.Windows.Forms.Timer _playTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer _testPlayTimer = new System.Windows.Forms.Timer();
        private int _testCurrentIndex = -1;
        private bool _isTestReversed = false;
        private const int BasePlaybackIntervalMs = 50;
        private const int TestBrightnessMin = 40;
        private const int TestBrightnessMax = 200;
        private const int TestBrightnessDefault = 100;
        private const string UiFontFamily = "Malgun Gothic";
        private const string WslSimulationProjectPath = "~/mysim";
        private const string WslModelDirectory = "models";
        private const string WslModelFilePattern = "models/*.h5";
        private const string WslPredictionResultsFilePattern = "models/*.csv";

        private class DeleteAction
        {
            public List<DrivingData> Items { get; set; } = new List<DrivingData>();
            public int RestoreIndex { get; set; }
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref Point lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int EmGetScrollPos = 0x04DD;
        private const int EmSetScrollPos = 0x04DE;
        private const int WmSetRedraw = 0x000B;

        public Form1()
        {
            InitializeComponent();
            _uiContext = SynchronizationContext.Current;
            _trainButtonIdleImage = btnTrain.BackgroundImage;
            AutoScaleMode = AutoScaleMode.None;

            if (lvDataItems.Columns.Count == 0)
            {
                lvDataItems.Columns.Add("\uBC88\uD638", 70);
                lvDataItems.Columns.Add("\uC774\uBBF8\uC9C0 \uD30C\uC77C\uBA85", 320);
            }
            AdjustDataListColumns();

            tbPlaybackSpeed.AutoSize = false;
            tbImageNavigator.AutoSize = false;
            tbTestImageNavigator.AutoSize = false;
            if (tbTestBrightness != null) tbTestBrightness.AutoSize = false;
            if (tbTestPlaybackSpeed != null) tbTestPlaybackSpeed.AutoSize = false;

            tbPlaybackSpeed.TickStyle = TickStyle.None;
            tbImageNavigator.TickStyle = TickStyle.BottomRight;
            tbTestImageNavigator.TickStyle = TickStyle.BottomRight;
            if (tbTestBrightness != null) tbTestBrightness.TickStyle = TickStyle.None;
            if (tbTestPlaybackSpeed != null) tbTestPlaybackSpeed.TickStyle = TickStyle.None;

            tbPlaybackSpeed.Minimum = 25;
            tbPlaybackSpeed.Maximum = 400;
            tbPlaybackSpeed.Value = 100;
            if (tbTestPlaybackSpeed != null)
            {
                tbTestPlaybackSpeed.Minimum = 25;
                tbTestPlaybackSpeed.Maximum = 400;
                tbTestPlaybackSpeed.Value = 100;
            }
            if (tbTestBrightness != null)
            {
                tbTestBrightness.Minimum = TestBrightnessMin;
                tbTestBrightness.Maximum = TestBrightnessMax;
                tbTestBrightness.SmallChange = 1;
                tbTestBrightness.LargeChange = 10;
                tbTestBrightness.Value = TestBrightnessDefault;
            }

            InitializeDataInfoGrid();
            ResetTrainingSummary();
            UpdatePlaybackSpeedLabel();
            UpdateTestPlaybackSpeedLabel();
            UpdateTestBrightnessLabel();
            UpdateSelectedArtifactTextBoxes();
            ConfigureResponsiveLayout();
            lvDataItems.MultiSelect = true;
            lvDataItems.HideSelection = false;

            _playTimer.Tick += PlayTimer_Tick;
            _testPlayTimer.Tick += TestPlayTimer_Tick;
            this.Shown += Form1_Shown;
            this.Resize += Form1_Resize;
            this.FormClosed += Form1_FormClosed;

            lvDataItems.SelectedIndexChanged += lvDataItems_SelectedIndexChanged;
            lvDataItems.MouseDown += lvDataItems_MouseDown;
            lvDataItems.MouseMove += lvDataItems_MouseMove;
            lvDataItems.MouseUp += lvDataItems_MouseUp;

            tbImageNavigator.Scroll += tbImageNavigator_Scroll;
            if (tbTestBrightness != null) tbTestBrightness.Scroll += tbTestBrightness_Scroll;
            if (tbTestPlaybackSpeed != null) tbTestPlaybackSpeed.Scroll += tbTestPlaybackSpeed_Scroll;

            btnSelectModelFile.Click -= btnSelectModelFile_Click;
            btnSelectModelFile.Click += btnSelectModelFile_Click;
            btnSelectPredictionCsv.Click -= btnSelectPredictionCsv_Click;
            btnSelectPredictionCsv.Click += btnSelectPredictionCsv_Click;

            btnTestPlay.Click -= btnTestPlay_Click;
            btnTestPlay.Click += btnTestPlay_Click;
            btnTestStop.Click -= btnTestStop_Click;
            btnTestStop.Click += btnTestStop_Click;
            btnTestReverse.Click -= btnTestReverse_Click;
            btnTestReverse.Click += btnTestReverse_Click;

            if (tbTestImageNavigator != null)
            {
                tbTestImageNavigator.Scroll -= tbTestImageNavigator_Scroll_1;
                tbTestImageNavigator.Scroll += tbTestImageNavigator_Scroll_1;
            }
        }

        #region [1. Chart layout]

        private void Form1_Shown(object? sender, EventArgs e)
        {
            FitFormToWorkingArea();
            dgvDataInfo.ClearSelection();
            _isRangeSettingMode = false;
            pnlImageRangeMarker.Visible = false;

            this.AutoScroll = false;

            var tabControl = this.Controls.OfType<TabControl>().FirstOrDefault();
            if (tabControl != null && tabControl.TabPages.Count >= 2)
            {
                TabPage page1 = tabControl.TabPages[0];
                TabPage page2 = tabControl.TabPages[1];

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

            ApplyResponsiveLayout();
            EnsureDataChartsLayout();
            EnsureTestChartsLayout();
            SetupSafeChart(chtSteeringValue, "조향값", Color.FromArgb(45, 212, 191), "실제 조향값");
            SetupSafeChart(chtSpeedValue, "속도", Color.FromArgb(245, 176, 65), "실제 속도");
            SetupSafeChart(chtTestSteeringValue, "조향값 예측", Color.FromArgb(248, 113, 113), "Predict", "Actual", Color.FromArgb(45, 212, 191));
            SetupSafeChart(chtTestSpeedValue, "속도 예측", Color.FromArgb(248, 113, 113), "Predict", "Actual", Color.FromArgb(245, 176, 65));

            UpdateCharts();
        }

        private void FitFormToWorkingArea()
        {
            if (WindowState != FormWindowState.Normal)
                return;

            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            int maxWidth = Math.Max(MinimumSize.Width, workingArea.Width);
            int maxHeight = Math.Max(MinimumSize.Height, workingArea.Height - 12);

            if (Width > maxWidth || Height > maxHeight)
            {
                Size = new Size(Math.Min(Width, maxWidth), Math.Min(Height, maxHeight));
                Location = new Point(
                    workingArea.Left + Math.Max(0, (workingArea.Width - Width) / 2),
                    workingArea.Top + Math.Max(0, (workingArea.Height - Height) / 2));
            }
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            ApplyResponsiveLayout();
            EnsureDataChartsLayout();
            EnsureTestChartsLayout();
            AdjustDataListColumns();
        }

        private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _playTimer.Stop();
            _testPlayTimer.Stop();
            _playTimer.Dispose();
            _testPlayTimer.Dispose();
            ShutdownWslForApp();
        }

        private void ShutdownWslForApp()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "wsl.exe",
                    Arguments = "--shutdown",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process.Start(start)?.Dispose();
            }
            catch
            {
            }
        }

        private void ConfigureResponsiveLayout()
        {
            tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbDataLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCheckDataIntegrity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveCatalogState.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            pbDataPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvDataInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lvDataItems.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbImageNavigator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSetRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            gbTrainingSetup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbModelTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtTrainingLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTrainingSavePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbTestImageNavigator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (tcMain == null || gbDataLoad == null || gbDataContent == null || gbTrainingSetup == null || gbModelTest == null)
                return;

            const int margin = 5;
            int tabTop = Math.Max(48, lblTitle.Bottom + 8);
            tcMain.SetBounds(0, tabTop, Math.Max(400, ClientSize.Width), Math.Max(300, ClientSize.Height - tabTop));

            int pageWidth = Math.Max(400, tpDataManager.ClientSize.Width - (margin * 2));
            int pageHeight = Math.Max(260, tpDataManager.ClientSize.Height);

            int dataLoadHeight = Math.Min(96, Math.Max(76, pageHeight / 6));
            gbDataLoad.SetBounds(margin, 3, pageWidth, dataLoadHeight);
            gbDataContent.SetBounds(margin, gbDataLoad.Bottom + 8, pageWidth, Math.Max(220, pageHeight - gbDataLoad.Bottom - 16));
        }

        private void LayoutModelTestControls()
        {
            if (gbModelTest == null) return;

            const int margin = 18;
            int width = gbModelTest.ClientSize.Width;
            int height = gbModelTest.ClientSize.Height;
            int previewWidth = Math.Min(606, Math.Max(260, width / 3));
            int previewHeight = Math.Min(360, Math.Max(110, height - 180));

            pbTestPreview.SetBounds(margin, 41, previewWidth, previewHeight);
            btnStartTest.SetBounds(margin, pbTestPreview.Bottom + 12, previewWidth, 46);
            btnShowCurrentPrediction.SetBounds(margin, btnStartTest.Bottom + 8, previewWidth, 40);
            int controlLeft = pbTestPreview.Right + 23;
            int controlWidth = Math.Min(460, Math.Max(280, width - controlLeft - 12));
            int leftButtonWidth = Math.Min(220, Math.Max(132, (controlWidth - 14) / 2));
            int rightButtonLeft = controlLeft + leftButtonWidth + 14;
            int rightButtonWidth = Math.Max(140, controlLeft + controlWidth - rightButtonLeft);
            lblTestCurrentIndex.SetBounds(controlLeft, 41, leftButtonWidth, 74);
            btnStartTest.SetBounds(rightButtonLeft, 41, rightButtonWidth, 74);
            btnShowCurrentPrediction.SetBounds(controlLeft, 123, leftButtonWidth, 46);
            btnPredictCurrentFrame.SetBounds(rightButtonLeft, 123, rightButtonWidth, 46);
            btnSelectModelFile.SetBounds(controlLeft, 179, 132, 32);
            txtSelectedModelFile.SetBounds(btnSelectModelFile.Right + 8, 179, Math.Max(160, controlWidth - 140), 32);
            btnSelectPredictionCsv.SetBounds(controlLeft, 219, 132, 32);
            txtSelectedPredictionCsv.SetBounds(btnSelectPredictionCsv.Right + 8, 219, Math.Max(160, controlWidth - 140), 32);
            tbTestBrightness.SetBounds(controlLeft, 272, Math.Max(120, controlWidth - 70), 45);
            lblTestBrightness.SetBounds(tbTestBrightness.Right + 8, tbTestBrightness.Top + 2, 70, 32);
            tbTestPlaybackSpeed.SetBounds(controlLeft, 325, Math.Max(120, controlWidth - 70), 45);
            lblTestPlaybackSpeed.SetBounds(tbTestPlaybackSpeed.Right + 8, tbTestPlaybackSpeed.Top + 2, 70, 32);
            btnTestPlay.SetBounds(controlLeft, 378, 145, 48);
            btnTestStop.SetBounds(btnTestPlay.Right + 11, 378, 148, 48);
            btnTestReverse.SetBounds(btnTestStop.Right + 11, 378, 146, 48);
            tbTestImageNavigator.SetBounds(margin, Math.Max(40, height - 58), Math.Max(120, width - (margin * 2)), 30);
        }

        private void EnsureDataChartsLayout()
        {
            TableLayoutPanel layout = GetOrCreateChartLayout(gbDataContent, "tlpDataCharts", 2, 1);
            int chartTop = Math.Max(tbImageNavigator.Bottom + 10, btnSetRange.Bottom + 10);
            int chartHeight = Math.Max(80, gbDataContent.ClientSize.Height - chartTop - 12);
            layout.Bounds = new Rectangle(12, chartTop, Math.Max(100, gbDataContent.ClientSize.Width - 24), chartHeight);
            layout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            if (chtSteeringValue == null) chtSteeringValue = new Chart();
            if (chtSpeedValue == null) chtSpeedValue = new Chart();

            AddChartToLayout(layout, chtSteeringValue, 0, 0);
            AddChartToLayout(layout, chtSpeedValue, 1, 0);
        }

        private void EnsureTestChartsLayout()
        {
            TableLayoutPanel layout = GetOrCreateChartLayout(gbModelTest, "tlpTestCharts", 1, 2);
            int chartLeft = pbTestPreview.Right + 20;
            int chartTop = pbTestPreview.Top;
            int chartBottom = tbTestImageNavigator.Top - 12;
            int availableWidth = Math.Max(100, gbModelTest.ClientSize.Width - chartLeft - 12);
            int labelRight = Math.Max(lblTestBrightness?.Right ?? 0, lblTestPlaybackSpeed?.Right ?? 0);
            int safeChartLeft = Math.Max(chartLeft, labelRight + 24);
            int maxSafeChartWidth = Math.Max(100, gbModelTest.ClientSize.Width - safeChartLeft - 12);
            int desiredChartWidth = Math.Max(100, (int)Math.Round((availableWidth / 2.0) * 0.9));
            int chartWidth = Math.Min(desiredChartWidth, maxSafeChartWidth);
            int rightAlignedLeft = Math.Max(safeChartLeft, gbModelTest.ClientSize.Width - chartWidth - 12);
            layout.Bounds = new Rectangle(rightAlignedLeft, chartTop, chartWidth, Math.Max(80, chartBottom - chartTop));
            layout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            if (chtTestSteeringValue == null) chtTestSteeringValue = new Chart();
            if (chtTestSpeedValue == null) chtTestSpeedValue = new Chart();

            AddChartToLayout(layout, chtTestSteeringValue, 0, 0);
            AddChartToLayout(layout, chtTestSpeedValue, 0, 1);
        }

        private void LayoutTestImageNavigatorAtBottom()
        {
            if (gbModelTest == null || tbTestImageNavigator == null) return;

            const int margin = 12;
            tbTestImageNavigator.SetBounds(
                margin,
                Math.Max(40, gbModelTest.ClientSize.Height - 58),
                Math.Max(100, gbModelTest.ClientSize.Width - (margin * 2)),
                30);
        }

        private void UpdateTestIndexLabel(int? displayIndex = null)
        {
            if (lblTestCurrentIndex == null) return;

            if (_trainingData.Count == 0)
            {
                lblTestCurrentIndex.Text = "\uD604\uC7AC \uC778\uB371\uC2A4\r\n- / -";
                return;
            }

            int index = displayIndex ?? Math.Max(0, Math.Min(_testCurrentIndex, _trainingData.Count - 1));
            lblTestCurrentIndex.Text = $"\uD604\uC7AC \uC778\uB371\uC2A4\r\n{index} / {_trainingData.Count - 1}";
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
            ca.AxisX.LabelStyle.Format = "0;0;0";
            ca.BackColor = Color.FromArgb(22, 30, 46);
            ca.BorderColor = Color.FromArgb(103, 119, 148);
            ca.AxisX.LineColor = Color.FromArgb(103, 119, 148);
            ca.AxisY.LineColor = Color.FromArgb(103, 119, 148);
            ca.AxisX.MajorGrid.LineColor = Color.FromArgb(49, 62, 88);
            ca.AxisY.MajorGrid.LineColor = Color.FromArgb(49, 62, 88);
            ca.AxisX.LabelStyle.ForeColor = Color.FromArgb(238, 243, 249);
            ca.AxisY.LabelStyle.ForeColor = Color.FromArgb(238, 243, 249);
            ca.AxisX.MajorTickMark.LineColor = Color.FromArgb(103, 119, 148);
            ca.AxisY.MajorTickMark.LineColor = Color.FromArgb(103, 119, 148);

            chart.FormatNumber -= Chart_FormatNumber;
            chart.FormatNumber += Chart_FormatNumber;
            chart.BackColor = Color.FromArgb(22, 30, 46);
            chart.BorderlineColor = Color.FromArgb(103, 119, 148);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BorderlineWidth = 1;

            chart.Titles.Clear();
            var title = chart.Titles.Add(titleName);
            title.Font = new Font("Malgun Gothic", 12, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(245, 176, 65);

            chart.Series.Clear();
            var s1 = chart.Series.Add(s1Name);
            s1.ChartType = SeriesChartType.Line;
            s1.Color = c1;
            s1.BorderWidth = 3;
            s1.ShadowColor = Color.Transparent;
            s1.LegendText = GetKoreanChartSeriesName(s1Name);

            if (s2Name != null)
            {
                var s2 = chart.Series.Add(s2Name);
                s2.ChartType = SeriesChartType.Line;
                s2.Color = c2 ?? Color.FromArgb(45, 212, 191);
                s2.BorderWidth = 3;
                s2.ShadowColor = Color.Transparent;
                s2.LegendText = GetKoreanChartSeriesName(s2Name);
            }

            chart.Legends.Clear();
            if (s2Name != null)
            {
                var legend = chart.Legends.Add("Legend");
                legend.BackColor = Color.Transparent;
                legend.ForeColor = Color.FromArgb(238, 243, 249);
                legend.Font = new Font("Malgun Gothic", 8F, FontStyle.Regular);
                legend.Docking = Docking.Bottom;
            }

            chart.Visible = true;
            chart.BringToFront();
        }

        private void Chart_FormatNumber(object? sender, FormatNumberEventArgs e)
        {
            if (Math.Abs(e.Value) < 0.0000001 || e.LocalizedValue == "-0")
            {
                e.LocalizedValue = "0";
            }
        }

        private static string GetKoreanChartSeriesName(string seriesName)
        {
            return seriesName switch
            {
                "Predict" => "예측값",
                "Actual" => "실제값",
                _ => seriesName
            };
        }

        #endregion

        #region [2. Data loading]

        private void btnSelectAdd_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                if (fbd.ShowDialog() == DialogResult.OK) { txtFolderPath.Text = fbd.SelectedPath; LoadData(fbd.SelectedPath); }
        }

        private void btnLoadSavedFolder_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "저장본 폴더 안의 저장 시각 폴더를 선택하세요."
            };

            if (fbd.ShowDialog() != DialogResult.OK) return;
            txtFolderPath.Text = fbd.SelectedPath;
            LoadData(fbd.SelectedPath);
        }

        private void btnSelectTrainingSave_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "학습에 사용할 저장본 폴더 안의 저장 시각 폴더를 선택하세요."
            };

            if (fbd.ShowDialog() != DialogResult.OK) return;

            _selectedTrainingSavePath = fbd.SelectedPath;
            LoadTrainingData(fbd.SelectedPath);
        }

        private void LoadData(string path)
        {
            _selectedDataFolderPath = path;
            _allData.Clear();
            _deleteUndoStack.Clear();
            ClearMarkers();
            lvDataItems.Items.Clear();
            ReleaseDataPreviewImage();
            txtFolderPath.ForeColor = _folderPathTextColor;

            var catalogFiles = Directory.GetFiles(path, "*.catalog");
            string imageBasePath = ResolveImageBasePath(path);

            if (catalogFiles.Length > 0)
            {
                ParseCatalogData(_allData, imageBasePath, catalogFiles);
            }

            RefreshDataListView(false);

            if (_allData.Count == 0)
            {
                ShowNoDataMessage();
                return;
            }

            ReindexDataItems();

            _currentIndex = 0;
            tbImageNavigator.Maximum = Math.Max(0, _allData.Count - 1);

            UpdateDisplay();
            UpdateCharts();
        }

        private void LoadTrainingData(string path)
        {
            _selectedTrainingSavePath = path;
            _trainingData.Clear();
            ReleaseTestPreviewImage();
            _testPlayTimer.Stop();
            _testCurrentIndex = -1;
            _showTestOverlay = false;
            UpdateTrainingSavePathTextBox();

            var catalogFiles = Directory.GetFiles(path, "*.catalog");
            string imageBasePath = ResolveImageBasePath(path);

            if (catalogFiles.Length > 0)
            {
                ParseCatalogData(_trainingData, imageBasePath, catalogFiles);
            }

            if (_trainingData.Count == 0)
            {
                ShowNoTrainingDataMessage();
                return;
            }

            ReindexTrainingDataItems();

            _testCurrentIndex = 0;
            if (tbTestImageNavigator != null) tbTestImageNavigator.Maximum = Math.Max(0, _trainingData.Count - 1);

            UpdateCharts();
            ShowFrame(0);
        }

        private void ParseCatalogData(List<DrivingData> targetData, string basePath, string[] catalogFiles)
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

                            targetData.Add(new DrivingData
                            {
                                Index = globalIdx++,
                                ImagePath = Path.Combine(basePath, "images", imgName),
                                Steering = angle,
                                Speed = throttle * 100,
                                CatalogRawLine = line,
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

            var missingImages = _allData
                .Where(x => !File.Exists(x.ImagePath))
                .OrderBy(x => x.CatalogFilePath)
                .ThenBy(x => x.CatalogLineNumber)
                .ToList();

            if (missingImages.Count == 0)
            {
                MessageBox.Show(
                    "검사 완료\n누락된 이미지가 없습니다.",
                    "데이터 무결성 검사",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            const int maxDisplayCount = 40;
            var lines = missingImages
                .Take(maxDisplayCount)
                .Select(x =>
                    $"{Path.GetFileName(x.CatalogFilePath)} / 카탈로그 인덱스 {x.CatalogLineNumber}: {x.CatalogImageName}")
                .ToList();

            if (missingImages.Count > maxDisplayCount)
            {
                lines.Add($"...외 {missingImages.Count - maxDisplayCount}개");
            }

            DialogResult result = MessageBox.Show(
                $"누락된 이미지가 {missingImages.Count}개 있습니다.\n\n" +
                "확인을 누르면 아래 카탈로그 항목이 삭제됩니다.\n" +
                "취소를 누르면 아무것도 변경하지 않습니다.\n\n" +
                string.Join("\n", lines),
                "데이터 무결성 검사",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                int restoreIndex = missingImages
                    .Select(item => _allData.IndexOf(item))
                    .Where(index => index >= 0)
                    .DefaultIfEmpty(0)
                    .Min();

                DeleteDataItems(missingImages, restoreIndex);
                MessageBox.Show(
                    "문제가 있는 카탈로그 항목을 삭제했습니다.",
                    "데이터 무결성 검사",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        #endregion

        #region [3. Navigation and playback]

        private void UpdateDisplay()
        {
            if (_currentIndex < 0 || _currentIndex >= _allData.Count) return;
            var data = _allData[_currentIndex];
            ReleaseDataPreviewImage();
            if (File.Exists(data.ImagePath))
            {
                pbDataPreview.Image = LoadImageWithoutLock(data.ImagePath);
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

        private bool EnsureTrainingDataLoaded()
        {
            if (_trainingData.Count > 0) return true;
            ShowNoTrainingDataMessage();
            return false;
        }

        private void ShowNoDataMessage()
        {
            _playTimer.Stop();
            txtFolderPath.ForeColor = _folderPathWarningColor;
            txtFolderPath.Text = "\uB370\uC774\uD130\uAC00 \uC5C6\uC2B5\uB2C8\uB2E4. \uB370\uC774\uD130\uB97C \uAC00\uC838\uC624\uC138\uC694";
        }

        private void ShowNoTrainingDataMessage()
        {
            _testPlayTimer.Stop();
            ReleaseTestPreviewImage();
            UpdateTestIndexLabel();
        }

        private void StartPlayback() { if (!EnsureDataLoaded()) return; _playTimer.Interval = GetPlaybackInterval(); _playTimer.Start(); }
        private void StartTestPlayback()
        {
            if (!EnsureTrainingDataLoaded()) return;

            if (tbTestImageNavigator != null)
            {
                _testCurrentIndex = Math.Max(0, Math.Min(tbTestImageNavigator.Value, _trainingData.Count - 1));
            }

            ShowFrame(_testCurrentIndex);
            _testPlayTimer.Interval = GetTestPlaybackInterval();
            _testPlayTimer.Start();
        }

        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isReversed) { if (_currentIndex > 0) _currentIndex--; else _playTimer.Stop(); }
            else { if (_currentIndex < _allData.Count - 1) _currentIndex++; else _playTimer.Stop(); }
            UpdateDisplay();
        }

        private void TestPlayTimer_Tick(object? sender, EventArgs e)
        {
            if (_isTestReversed) { if (_testCurrentIndex > 0) _testCurrentIndex--; else _testPlayTimer.Stop(); }
            else { if (_testCurrentIndex < _trainingData.Count - 1) _testCurrentIndex++; else _testPlayTimer.Stop(); }
            ShowFrame(_testCurrentIndex);
        }

        private void btnPlay_Click_1(object sender, EventArgs e) { _isReversed = false; StartPlayback(); }
        private void btnReverse_Click(object sender, EventArgs e) { _isReversed = true; StartPlayback(); }
        private void btnStop_Click(object sender, EventArgs e) { if (!EnsureDataLoaded()) return; _playTimer.Stop(); }

        private void btnTestPlay_Click(object? sender, EventArgs e) { _isTestReversed = false; StartTestPlayback(); }
        private void btnTestReverse_Click(object? sender, EventArgs e) { _isTestReversed = true; StartTestPlayback(); }
        private void btnTestStop_Click(object? sender, EventArgs e) { if (!EnsureTrainingDataLoaded()) return; _testPlayTimer.Stop(); }

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
            message = CleanTrainingLogMessage(message);
            if (string.IsNullOrWhiteSpace(message)) return;

            if (_uiContext != null && SynchronizationContext.Current != _uiContext)
            {
                _uiContext.Post(_ => AppendTrainingLog(message), null);
                return;
            }

            if (IsDisposed || !IsHandleCreated) return;
            if (txtTrainingLog == null || txtTrainingLog.IsDisposed) return;
            UpdateTrainingSummaryFromLog(message);
            AppendTrainingLogLine($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }

        private void AppendTrainingLogLine(string logLine)
        {
            bool shouldAutoScroll = IsTrainingLogScrolledToBottom();
            int selectionStart = txtTrainingLog.SelectionStart;
            int selectionLength = txtTrainingLog.SelectionLength;
            Point scrollPosition = Point.Empty;

            if (!shouldAutoScroll)
            {
                SendMessage(txtTrainingLog.Handle, EmGetScrollPos, IntPtr.Zero, ref scrollPosition);
                SendMessage(txtTrainingLog.Handle, WmSetRedraw, IntPtr.Zero, IntPtr.Zero);
            }

            txtTrainingLog.AppendText(logLine);

            if (shouldAutoScroll)
            {
                txtTrainingLog.SelectionStart = txtTrainingLog.TextLength;
                txtTrainingLog.SelectionLength = 0;
                txtTrainingLog.ScrollToCaret();
                return;
            }

            txtTrainingLog.SelectionStart = Math.Min(selectionStart, txtTrainingLog.TextLength);
            txtTrainingLog.SelectionLength = Math.Min(selectionLength, txtTrainingLog.TextLength - txtTrainingLog.SelectionStart);
            SendMessage(txtTrainingLog.Handle, EmSetScrollPos, IntPtr.Zero, ref scrollPosition);
            SendMessage(txtTrainingLog.Handle, WmSetRedraw, new IntPtr(1), IntPtr.Zero);
            txtTrainingLog.Invalidate();
        }

        private bool IsTrainingLogScrolledToBottom()
        {
            if (txtTrainingLog.TextLength == 0) return true;

            int bottomCharIndex = txtTrainingLog.GetCharIndexFromPosition(new Point(1, txtTrainingLog.ClientSize.Height - 1));
            int bottomVisibleLine = txtTrainingLog.GetLineFromCharIndex(bottomCharIndex);
            int lastLine = txtTrainingLog.GetLineFromCharIndex(txtTrainingLog.TextLength);
            return bottomVisibleLine >= lastLine - 1;
        }

        private void ResetTrainingSummary()
        {
            SetTrainingSummaryText("- / -", "-", "-", "-");
        }

        private void SetTrainingSummaryText(string epoch, string loss, string valLoss, string status)
        {
            if (lblTrainingEpochValue == null) return;

            lblTrainingEpochValue.Text = epoch;
            lblTrainingLossValue.Text = loss;
            lblTrainingValLossValue.Text = valLoss;
            lblTrainingStatusValue.Text = status;
            lblTrainingStatusValue.ForeColor = Color.FromArgb(45, 212, 191);
        }

        private void UpdateTrainingSummaryFromLog(string message)
        {
            Match epochMatch = Regex.Match(message, @"Epoch\s+(\d+)\s*/\s*(\d+)", RegexOptions.IgnoreCase);
            if (epochMatch.Success)
            {
                lblTrainingEpochValue.Text = $"{epochMatch.Groups[1].Value} / {epochMatch.Groups[2].Value}";
                if (lblTrainingStatusValue.Text == "-")
                {
                    lblTrainingStatusValue.Text = "학습 중";
                }
            }

            Match lossMatch = Regex.Match(message, @"(?:^|\s)-\s+loss:\s*([0-9.eE+-]+)");
            if (lossMatch.Success)
            {
                lblTrainingLossValue.Text = lossMatch.Groups[1].Value;
            }

            Match valLossMatch = Regex.Match(message, @"(?:^|\s)-\s+val_loss:\s*([0-9.eE+-]+)");
            if (valLossMatch.Success)
            {
                lblTrainingValLossValue.Text = valLossMatch.Groups[1].Value;
            }

            Match improvedMatch = Regex.Match(message, @"Epoch\s+\d+\s*:\s*val_loss\s+improved\s+from\s+([0-9.eE+-]+|inf)\s+to\s+([0-9.eE+-]+)", RegexOptions.IgnoreCase);
            if (!improvedMatch.Success)
            {
                improvedMatch = Regex.Match(message, @"val_loss\s+improved\s+from\s+([0-9.eE+-]+|inf)\s+to\s+([0-9.eE+-]+)", RegexOptions.IgnoreCase);
            }
            if (improvedMatch.Success)
            {
                lblTrainingValLossValue.Text = improvedMatch.Groups[2].Value;
                if (improvedMatch.Groups[1].Value.Equals("inf", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                lblTrainingStatusValue.Text = $"개선됨 {improvedMatch.Groups[1].Value} -> {improvedMatch.Groups[2].Value}";
                lblTrainingStatusValue.ForeColor = Color.FromArgb(45, 212, 191);
                return;
            }

            Match notImprovedMatch = Regex.Match(message, @"Epoch\s+\d+\s*:\s*val_loss\s+did\s+not\s+improve\s+from\s+([0-9.eE+-]+|inf)", RegexOptions.IgnoreCase);
            if (!notImprovedMatch.Success)
            {
                notImprovedMatch = Regex.Match(message, @"val_loss\s+did\s+not\s+improve\s+from\s+([0-9.eE+-]+|inf)", RegexOptions.IgnoreCase);
            }
            if (notImprovedMatch.Success)
            {
                lblTrainingStatusValue.Text = $"개선 없음 최적값 {notImprovedMatch.Groups[1].Value}";
                lblTrainingStatusValue.ForeColor = Color.FromArgb(245, 176, 65);
            }
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

        private string CreateTrainingDataZip()
        {
            string catalogPath = GetTrainingCatalogDirectory();
            if (string.IsNullOrWhiteSpace(catalogPath) || !Directory.Exists(catalogPath))
                throw new InvalidOperationException("The selected data folder could not be found.");

            string[] allCatalogFiles = Directory.GetFiles(catalogPath, "*.catalog");
            if (!allCatalogFiles.Any())
                throw new InvalidOperationException("The selected data folder does not contain any catalog files.");

            string dataRootPath = ResolveDataRootPath(catalogPath);
            string imagesPath = Path.Combine(dataRootPath, "images");
            if (!Directory.Exists(imagesPath))
                throw new InvalidOperationException("The selected data folder does not contain an images folder.");

            string[] trainingCatalogFiles = allCatalogFiles
                .Where(path => new FileInfo(path).Length > 0)
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToArray();
            if (!trainingCatalogFiles.Any())
                throw new InvalidOperationException("The selected data folder does not contain any non-empty catalog files.");

            string zipPath = Path.Combine(Path.GetTempPath(), $"datamanager_training_data_{DateTime.Now:yyyyMMddHHmmss}.zip");
            if (File.Exists(zipPath)) File.Delete(zipPath);

            using ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
            AddTrainingMetadataFiles(catalogPath, dataRootPath, trainingCatalogFiles, archive);

            foreach (string catalogFile in trainingCatalogFiles)
            {
                archive.CreateEntryFromFile(catalogFile, Path.GetFileName(catalogFile), CompressionLevel.Fastest);

                string manifestPath = catalogFile + "_manifest";
                if (File.Exists(manifestPath))
                {
                    archive.CreateEntryFromFile(manifestPath, Path.GetFileName(manifestPath), CompressionLevel.Fastest);
                }
            }

            foreach (string imageFile in Directory.GetFiles(imagesPath, "*", SearchOption.AllDirectories))
            {
                string relativeImagePath = Path.GetRelativePath(imagesPath, imageFile).Replace("\\", "/");
                archive.CreateEntryFromFile(imageFile, $"images/{relativeImagePath}", CompressionLevel.Fastest);
            }

            return zipPath;
        }

        private string ConvertWindowsPathToWslPath(string windowsPath)
        {
            string fullPath = Path.GetFullPath(windowsPath);
            string root = Path.GetPathRoot(fullPath) ?? "";
            if (root.Length < 2 || root[1] != ':')
                throw new InvalidOperationException("Only drive-letter Windows paths can be converted to WSL paths.");

            string drive = char.ToLowerInvariant(root[0]).ToString();
            string subPath = fullPath.Substring(root.Length).Replace("\\", "/");
            return $"/mnt/{drive}/{subPath}";
        }

        private string ConvertImagePathToWslPath(string imagePath)
        {
            string fullPath = Path.GetFullPath(imagePath);
            string normalized = fullPath.Replace("\\", "/");

            if (normalized.StartsWith("//wsl.localhost/", StringComparison.OrdinalIgnoreCase) ||
                normalized.StartsWith("//wsl$/", StringComparison.OrdinalIgnoreCase))
            {
                string[] parts = normalized.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 2)
                    return "/" + string.Join("/", parts.Skip(2));
            }

            string root = Path.GetPathRoot(fullPath) ?? "";
            if (root.Length < 2 || root[1] != ':')
                throw new InvalidOperationException($"Unsupported image path: {imagePath}");

            string subPath = fullPath.Substring(root.Length).Replace("\\", "/").TrimStart('/');
            if (subPath.StartsWith("home/", StringComparison.OrdinalIgnoreCase))
                return "/" + subPath;

            string drive = char.ToLowerInvariant(root[0]).ToString();
            return $"/mnt/{drive}/{subPath}";
        }

        private string BashQuote(string value)
        {
            return "'" + value.Replace("'", "'\"'\"'") + "'";
        }

        private string CreateTimestampedWslArtifactPath(string prefix, string extension)
        {
            return $"{WslModelDirectory}/{prefix}_{DateTime.Now:yyyyMMdd_HHmmss}{extension}";
        }

        private string NormalizeSelectedArtifactPath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return "";

            string fileName = Path.GetFileName(filePath);
            return string.IsNullOrWhiteSpace(fileName)
                ? filePath.Replace("\\", "/")
                : $"{WslModelDirectory}/{fileName}";
        }

        private void UpdateSelectedArtifactTextBoxes()
        {
            if (txtSelectedModelFile != null)
                txtSelectedModelFile.Text = GetDisplayArtifactName(_selectedModelFile);
            if (txtSelectedPredictionCsv != null)
                txtSelectedPredictionCsv.Text = GetDisplayArtifactName(_selectedPredictionResultsFile);
        }

        private string GetDisplayArtifactName(string artifactPath)
        {
            return string.IsNullOrWhiteSpace(artifactPath) ? "최신 파일 자동 선택" : Path.GetFileName(artifactPath);
        }

        private async Task<string> ResolveModelFileForPredictionAsync()
        {
            if (!string.IsNullOrWhiteSpace(_selectedModelFile)) return _selectedModelFile;
            return await GetLatestWslArtifactAsync(WslModelFilePattern, "학습 모델(.h5)");
        }

        private async Task<string> ResolvePredictionResultsFileAsync()
        {
            if (!string.IsNullOrWhiteSpace(_selectedPredictionResultsFile)) return _selectedPredictionResultsFile;
            return await GetLatestWslArtifactAsync(WslPredictionResultsFilePattern, "예측 결과(.csv)");
        }

        private async Task<string> GetLatestWslArtifactAsync(string pattern, string displayName)
        {
            string command = $"cd {WslSimulationProjectPath} || exit 2; ls -t {pattern} 2>/dev/null | head -n 1";
            string latest = (await RunWslBashCommandAsync(command)).Trim();
            if (string.IsNullOrWhiteSpace(latest))
                throw new InvalidOperationException($"{displayName} 파일을 찾을 수 없습니다.");

            return latest;
        }

        private async Task<string> GetWslModelsWindowsPathAsync()
        {
            string command = $"cd {WslSimulationProjectPath} && mkdir -p {BashQuote(WslModelDirectory)} && wslpath -w {BashQuote(WslModelDirectory)}";
            return (await RunWslBashCommandAsync(command)).Trim();
        }

        private async Task<string> RunWslBashCommandAsync(string bashCommand)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "wsl.exe";
            start.Arguments = GetWslDistroArgument() + "bash -lc \"" + bashCommand.Replace("\"", "\\\"") + "\"";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;

            using Process process = Process.Start(start) ?? throw new InvalidOperationException("WSL command could not be started.");
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new InvalidOperationException(string.IsNullOrWhiteSpace(error) ? $"WSL command failed. ExitCode={process.ExitCode}" : error.Trim());

            return output;
        }

        private async void btnSelectModelFile_Click(object? sender, EventArgs e)
        {
            await SelectArtifactFileAsync("학습 모델 선택", "H5 model (*.h5)|*.h5|All files (*.*)|*.*", path =>
            {
                _selectedModelFile = NormalizeSelectedArtifactPath(path);
            });
        }

        private async void btnSelectPredictionCsv_Click(object? sender, EventArgs e)
        {
            await SelectArtifactFileAsync("예측 결과 선택", "CSV file (*.csv)|*.csv|All files (*.*)|*.*", path =>
            {
                _selectedPredictionResultsFile = NormalizeSelectedArtifactPath(path);
            });
        }

        private async Task SelectArtifactFileAsync(string title, string filter, Action<string> setSelectedPath)
        {
            try
            {
                using OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = title;
                dialog.Filter = filter;
                dialog.InitialDirectory = await GetWslModelsWindowsPathAsync();
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;

                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                setSelectedPath(dialog.FileName);
                UpdateSelectedArtifactTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일 선택 중 오류가 발생했습니다: " + ex.Message);
            }
        }

        private void SetTrainingButtonRunningState(bool isRunning)
        {
            if (btnTrain == null) return;

            if (isRunning)
            {
                btnTrain.Enabled = true;
                btnTrain.BackgroundImage = null;
                btnTrain.Text = "멈춤";
                StyleButton(btnTrain, Color.FromArgb(248, 113, 113), Color.White, Color.FromArgb(248, 113, 113));
                return;
            }

            btnTrain.Enabled = true;
            btnTrain.Text = string.Empty;
            btnTrain.BackgroundImage = _trainButtonIdleImage;
            StyleButton(btnTrain, Color.FromArgb(45, 212, 191), Color.FromArgb(6, 42, 43), Color.FromArgb(45, 212, 191));
        }

        private void StopTrainingProcess()
        {
            _trainingStopRequested = true;
            AppendTrainingLog("Training stop requested.");

            try
            {
                if (_trainingProcess != null && !_trainingProcess.HasExited)
                {
                    _trainingProcess.Kill(true);
                }
            }
            catch (Exception ex)
            {
                AppendTrainingLog("Error while stopping training: " + ex.Message);
            }
        }

        private void SetTestButtonRunningState(bool isRunning)
        {
            if (btnStartTest == null) return;

            if (!isRunning)
            {
                RestoreTestActionButtons();
                return;
            }

            Button activeButton = _activeTestButton ?? btnStartTest;
            Button? inactiveButton = activeButton == btnStartTest ? btnPredictCurrentFrame : btnStartTest;

            activeButton.Enabled = true;
            activeButton.Text = "Stop";
            StyleButton(activeButton, Color.FromArgb(248, 113, 113), Color.White, Color.FromArgb(248, 113, 113));

            if (inactiveButton != null)
                inactiveButton.Enabled = false;
        }

        private void RestoreTestActionButtons()
        {
            if (btnStartTest != null)
            {
                btnStartTest.Enabled = true;
                btnStartTest.Text = "테스트 시작";
                StyleButton(btnStartTest, Color.FromArgb(45, 212, 191), Color.FromArgb(6, 42, 43), Color.FromArgb(45, 212, 191));
            }

            if (btnPredictCurrentFrame != null)
            {
                btnPredictCurrentFrame.Enabled = true;
                btnPredictCurrentFrame.Text = "밝기 예측 적용";
                StyleButton(btnPredictCurrentFrame, Color.FromArgb(16, 185, 129), Color.FromArgb(6, 42, 43), Color.FromArgb(16, 185, 129));
            }
        }

        private void StopTestProcess()
        {
            _testStopRequested = true;
            AppendTrainingLog("Test stop requested.");

            try
            {
                if (_testProcess != null && !_testProcess.HasExited)
                {
                    _testProcess.Kill(true);
                }
            }
            catch (Exception ex)
            {
                AppendTrainingLog("Error while stopping test: " + ex.Message);
            }
        }

        private async void btnTrain_Click(object sender, EventArgs e)
        {
            if (_isTrainingRunning)
            {
                StopTrainingProcess();
                return;
            }

            if (!EnsureTrainingDataLoaded()) return;

            try
            {
                _isTrainingRunning = true;
                _trainingStopRequested = false;
                ResetTrainingSummary();
                SetTrainingButtonRunningState(true);
                AppendTrainingLog("Compressing selected data folder for training.");
                string trainingZipPath = CreateTrainingDataZip();
                string wslTrainingZipPath = ConvertWindowsPathToWslPath(trainingZipPath);

                if (_trainingStopRequested)
                {
                    AppendTrainingLog("Training was stopped before it started.");
                    return;
                }

                string envName = "e2e_env";
                string projectPath = WslSimulationProjectPath;
                string outputModelFile = CreateTimestampedWslArtifactPath("mypilot", ".h5");

                string installCmd = "pip install numpy==1.24.3 pandas==2.0.3 tensorflow==2.13.0 albumentations imgaug";
                string importCmd = $"rm -rf ./data && mkdir -p ./data && cp {BashQuote(wslTrainingZipPath)} ./training_data.zip && python -m zipfile -e ./training_data.zip ./data && test -d ./data/images && ls ./data/*.catalog >/dev/null";
                string trainCmd = $"python train.py --tubs ./data --model {BashQuote(outputModelFile)}";

                string bashCmd = BuildWslCondaCommand(envName, $"cd {projectPath} && {importCmd} && {installCmd} && {trainCmd}");

                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "wsl.exe";

                string wslDistroArgument = GetWslDistroArgument();
                start.Arguments = wslDistroArgument + "bash -lc \"" + bashCmd + "\"";

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
                AppendTrainingLog("Starting WSL training process after replacing the data folder.");
                process.Start();
                _trainingProcess = process;
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                await process.WaitForExitAsync();
                if (_trainingStopRequested)
                {
                    AppendTrainingLog($"Training stopped. ExitCode={process.ExitCode}");
                }
                else
                {
                    AppendTrainingLog($"Training process finished. ExitCode={process.ExitCode}");
                    if (process.ExitCode == 0)
                    {
                        _selectedModelFile = outputModelFile;
                        UpdateSelectedArtifactTextBoxes();
                        AppendTrainingLog($"Model saved: {outputModelFile}");
                    }
                }
            }
            catch (Exception ex) when (!_trainingStopRequested)
            {
                MessageBox.Show("Error while running process: " + ex.Message);
            }
            catch
            {
                AppendTrainingLog("Training stopped.");
            }
            finally
            {
                _trainingProcess = null;
                _isTrainingRunning = false;
                _trainingStopRequested = false;
                SetTrainingButtonRunningState(false);
            }
        }

        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            if (_isTestRunning)
            {
                StopTestProcess();
                return;
            }

            if (!EnsureTrainingDataLoaded()) return;

            try
            {
                _isTestRunning = true;
                _testStopRequested = false;
                _activeTestButton ??= btnStartTest;
                SetTestButtonRunningState(true);
                AppendTrainingLog("Test session started. Syncing paths.");
                string envName = "e2e_env";
                string projectPath = WslSimulationProjectPath;
                string modelFile = await ResolveModelFileForPredictionAsync();
                float predictionBrightnessFactor = GetTestBrightnessFactor();
                string resultsPrefix = IsDefaultTestBrightness(predictionBrightnessFactor)
                    ? "results"
                    : $"results_brightness_{GetBrightnessToken(predictionBrightnessFactor)}";
                string resultsFile = CreateTimestampedWslArtifactPath(resultsPrefix, ".csv");
                _selectedModelFile = modelFile;
                _selectedPredictionResultsFile = resultsFile;
                UpdateSelectedArtifactTextBoxes();

                string appDir = Application.StartupPath;
                string winPathsFile = Path.Combine(appDir, "win_paths.txt");

                var linuxPaths = _trainingData
                    .Select(d => ConvertImagePathToWslPath(d.ImagePath))
                    .ToList();
                File.WriteAllLines(winPathsFile, linuxPaths);
                AppendTrainingLog($"Prepared {linuxPaths.Count} test images.");
                AppendTrainingLog($"Prediction input brightness: x{predictionBrightnessFactor:0.##}.");

                if (_testStopRequested)
                {
                    AppendTrainingLog("Test was stopped before prediction started.");
                    return;
                }

                Process wslPathProc = new Process();
                wslPathProc.StartInfo.FileName = "wsl.exe";
                string wslDistroArgument = GetWslDistroArgument();
                wslPathProc.StartInfo.Arguments = $"{wslDistroArgument}wslpath '{appDir.Replace("\\", "/")}'";
                wslPathProc.StartInfo.UseShellExecute = false;
                wslPathProc.StartInfo.RedirectStandardOutput = true;
                wslPathProc.StartInfo.RedirectStandardError = true;
                wslPathProc.StartInfo.CreateNoWindow = true;
                if (!wslPathProc.Start())
                    throw new InvalidOperationException("wslpath process could not be started.");

                string wslAppDir = (await wslPathProc.StandardOutput.ReadToEndAsync()).Trim();
                string wslPathError = await wslPathProc.StandardError.ReadToEndAsync();
                await wslPathProc.WaitForExitAsync();
                if (wslPathProc.ExitCode != 0)
                    throw new InvalidOperationException($"wslpath failed: {wslPathError}");
                AppendTrainingLog("WSL path conversion complete. Creating prediction script.");

                if (_testStopRequested)
                {
                    AppendTrainingLog("Test was stopped before prediction started.");
                    return;
                }

                string wslPathsFile = $"{wslAppDir}/win_paths.txt";
                string winPredictScriptFile = Path.Combine(appDir, "predict_frames.py");
                string wslPredictScriptFile = $"{wslAppDir}/predict_frames.py";

                string pythonPredictScript = string.Join("\n", new[]
                {
                    "import os",
                    "import tensorflow as tf",
                    "import numpy as np",
                    "from PIL import Image",
                    $"brightness_factor = {predictionBrightnessFactor.ToString(CultureInfo.InvariantCulture)}",
                    "def preprocess_image(image_path):",
                    "    image = Image.open(image_path).convert('RGB').resize((160, 120))",
                    "    image_array = np.asarray(image, dtype=np.float64) / 255.0",
                    "    if brightness_factor != 1.0:",
                    "        image_array = np.clip(image_array * brightness_factor, 0.0, 1.0)",
                    "    return image_array",
                    "def extract_predictions(pred, count):",
                    "    if isinstance(pred, (list, tuple)):",
                    "        angles = np.asarray(pred[0]).reshape(-1)",
                    "        throttles = np.asarray(pred[1]).reshape(-1) if len(pred) > 1 else np.zeros(count)",
                    "    else:",
                    "        arr = np.asarray(pred)",
                    "        arr = arr.reshape((count, -1))",
                    "        angles = arr[:, 0]",
                    "        throttles = arr[:, 1] if arr.shape[1] > 1 else np.zeros(count)",
                    "    return [(float(a), float(t)) for a, t in zip(angles[:count], throttles[:count])]",
                    "def predict_image_batch(model, image_paths):",
                    "    batch = np.stack([preprocess_image(path) for path in image_paths], axis=0)",
                    "    pred = model.predict(batch, verbose=0)",
                    "    return extract_predictions(pred, len(image_paths))",
                    "print('Loading model...', flush=True)",
                    $"model = tf.keras.models.load_model({JsonSerializer.Serialize(modelFile)})",
                    "print('Model loaded.', flush=True)",
                    $"with open({JsonSerializer.Serialize(wslPathsFile)}, 'r') as f:",
                    "    paths = f.read().splitlines()",
                    "total = len(paths)",
                    "print(f'Predicting {total} frames...', flush=True)",
                    "batch_size = 1024",
                    "results = ['0,0'] * total",
                    "predicted_count = 0",
                    "missing_examples = []",
                    "for start in range(0, total, batch_size):",
                    "    end = min(start + batch_size, total)",
                    "    indexed_paths = []",
                    "    for idx in range(start, end):",
                    "        if os.path.exists(paths[idx]):",
                    "            indexed_paths.append((idx, paths[idx]))",
                    "        elif len(missing_examples) < 3:",
                    "            missing_examples.append(paths[idx])",
                    "    missing_count = (end - start) - len(indexed_paths)",
                    "    if indexed_paths:",
                    "        batch_indexes = [item[0] for item in indexed_paths]",
                    "        batch_paths = [item[1] for item in indexed_paths]",
                    "        predictions = predict_image_batch(model, batch_paths)",
                    "        for idx, (angle, throttle) in zip(batch_indexes, predictions):",
                    "            results[idx] = f'{angle},{throttle}'",
                    "        predicted_count += len(predictions)",
                    "    print(f'[{end}/{total}] predicted batch; missing={missing_count}', flush=True)",
                    "if total > 0 and predicted_count == 0:",
                    "    raise RuntimeError('No images were predicted. First missing paths: ' + ' | '.join(missing_examples))",
                    "print(f'Predicted frames: {predicted_count}/{total}', flush=True)",
                    $"with open({JsonSerializer.Serialize(resultsFile)}, 'w') as f:",
                    "    f.write('\\n'.join(results))",
                    $"print('Prediction results saved to {resultsFile}.', flush=True)"
                });
                File.WriteAllText(winPredictScriptFile, pythonPredictScript);
                AppendTrainingLog("Starting prediction process. TensorFlow model loading may take a while.");

                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "wsl.exe";
                string bashCmd = BuildWslCondaCommand(envName, $"cd {projectPath} && python {BashQuote(wslPredictScriptFile)}");
                start.Arguments = $"{wslDistroArgument}bash -lc \"{bashCmd}\"";
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;
                start.CreateNoWindow = true;

                object predictionLogLock = new object();
                List<string> predictionLogTail = new List<string>();
                void CapturePredictionLogLine(string line)
                {
                    lock (predictionLogLock)
                    {
                        predictionLogTail.Add(line);
                        if (predictionLogTail.Count > 25)
                            predictionLogTail.RemoveAt(0);
                    }
                }

                using Process process = Process.Start(start) ?? throw new InvalidOperationException("Prediction process could not be started.");
                _testProcess = process;
                process.OutputDataReceived += (_, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    CapturePredictionLogLine(args.Data);
                    AppendTrainingLog(args.Data);
                };
                process.ErrorDataReceived += (_, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    CapturePredictionLogLine(args.Data);
                    AppendTrainingLog(args.Data);
                };
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                await process.WaitForExitAsync();
                if (_testStopRequested)
                {
                    AppendTrainingLog($"Test stopped. ExitCode={process.ExitCode}");
                    return;
                }

                if (process.ExitCode != 0)
                {
                    string detail;
                    lock (predictionLogLock)
                    {
                        detail = string.Join("\r\n", predictionLogTail);
                    }

                    throw new InvalidOperationException(
                        string.IsNullOrWhiteSpace(detail)
                            ? $"prediction process failed. ExitCode={process.ExitCode}"
                            : $"prediction process failed. ExitCode={process.ExitCode}\r\n\r\n{detail}");
                }

                string[] resultLines = await ReadWslPredictionResultsAsync(projectPath, resultsFile);
                if (LoadPredictionResults(resultLines))
                {
                    _selectedPredictionResultsFile = resultsFile;
                    UpdateSelectedArtifactTextBoxes();
                    AppendTrainingLog("Prediction finished. Check the charts and overlay.");
                    AppendTrainingLog($"Prediction results saved: {resultsFile}");
                }
            }
            catch (Exception ex) when (!_testStopRequested)
            {
                MessageBox.Show("테스트 실패: " + ex.Message);
            }
            catch
            {
                AppendTrainingLog("Test stopped.");
            }
            finally
            {
                _testProcess = null;
                _isTestRunning = false;
                _testStopRequested = false;
                SetTestButtonRunningState(false);
                _activeTestButton = null;
            }
        }

        private async void btnShowCurrentPrediction_Click(object sender, EventArgs e)
        {
            if (!EnsureTrainingDataLoaded()) return;

            try
            {
                string resultsFile = await ResolvePredictionResultsFileAsync();
                _selectedPredictionResultsFile = resultsFile;
                UpdateSelectedArtifactTextBoxes();
                string[] resultLines = await ReadWslPredictionResultsAsync(WslSimulationProjectPath, resultsFile);
                if (LoadPredictionResults(resultLines))
                {
                    AppendTrainingLog($"Loaded existing prediction results from {WslSimulationProjectPath}/{resultsFile}.");
                }
            }
            catch (Exception ex)
            {
                AppendTrainingLog("Error while loading prediction results: " + ex.Message);
            }
        }

        private void btnPredictCurrentFrame_Click(object? sender, EventArgs e)
        {
            if (!EnsureTrainingDataLoaded()) return;
            if (_isTestRunning)
            {
                StopTestProcess();
                return;
            }

            AppendTrainingLog($"Starting full prediction with brightness x{GetTestBrightnessFactor():0.##}.");
            _activeTestButton = btnPredictCurrentFrame;
            btnStartTest_Click(sender ?? btnPredictCurrentFrame, e);
        }

        private async Task<string[]> ReadWslPredictionResultsAsync(string projectPath, string resultsFile)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "wsl.exe";
            start.Arguments = $"{GetWslDistroArgument()}bash -lc \"cd {projectPath} && test -f {resultsFile} && cat {resultsFile}\"";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;

            using Process process = Process.Start(start) ?? throw new InvalidOperationException("WSL results reader could not be started.");
            Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
            Task<string> errorTask = process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            string output = await outputTask;
            string error = await errorTask;
            if (process.ExitCode != 0)
                throw new InvalidOperationException($"{projectPath}/{resultsFile} was not found or could not be read. {error}".Trim());

            return output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool LoadPredictionResults(string[] lines)
        {
            if (lines.Length != _trainingData.Count)
            {
                AppendTrainingLog($"Prediction result count differs from data count. results={lines.Length}, data={_trainingData.Count}");
            }

            foreach (var data in _trainingData)
            {
                data.PredictedSteering = 0;
                data.PredictedSpeed = 0;
                data.HasPrediction = false;
            }

            int count = Math.Min(lines.Length, _trainingData.Count);
            int parsedCount = 0;
            int zeroPredictionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string[] vals = lines[i].Split(',');
                if (vals.Length != 2) continue;

                if (double.TryParse(vals[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double angle) &&
                    double.TryParse(vals[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double throttle))
                {
                    _trainingData[i].PredictedSteering = angle;
                    _trainingData[i].PredictedSpeed = throttle * 100;
                    _trainingData[i].HasPrediction = true;
                    parsedCount++;
                    if (Math.Abs(angle) < 0.0000001 && Math.Abs(throttle) < 0.0000001)
                        zeroPredictionCount++;
                }
            }

            if (parsedCount > 0 && zeroPredictionCount == parsedCount)
            {
                AppendTrainingLog("Warning: all loaded prediction values are 0,0. Check the prediction log for missing image paths or model output issues.");
            }

            _showTestOverlay = true;
            UpdateCharts();
            ShowFrame(Math.Max(0, Math.Min(tbTestImageNavigator?.Value ?? 0, _trainingData.Count - 1)));
            return true;
        }

        private void tbTestImageNavigator_Scroll_1(object sender, EventArgs e)
        {
            if (!EnsureTrainingDataLoaded() || tbTestImageNavigator == null || pbTestPreview == null) return;
            _testCurrentIndex = Math.Max(0, Math.Min(tbTestImageNavigator.Value, _trainingData.Count - 1));
            ShowFrame(_testCurrentIndex);
        }

        #endregion

        #region [5. Filtering, deletion, and markers]

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
            else if (TryGetSelectedListViewDataItems(out var selectedItems, out int restoreIndex))
            {
                DeleteDataItems(selectedItems, restoreIndex);
            }
            else
            {
                DeleteDataItems(new List<DrivingData> { _allData[_currentIndex] }, _currentIndex);
            }
        }

        private void btnCancelDelete_Click(object sender, EventArgs e)
        {
            if (_deleteUndoStack.Count == 0) return;

            DeleteAction action = _deleteUndoStack.Pop();
            int insertIndex = Math.Max(0, Math.Min(action.RestoreIndex, _allData.Count));
            _allData.InsertRange(insertIndex, action.Items);
            _currentIndex = insertIndex;
            ReindexDataItems();
            RefreshUI();
        }

        private void btnSaveCatalogState_Click(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;

            try
            {
                string sourceCatalogDir = GetSelectedCatalogDirectory();
                string saveDir = CreateCatalogSaveDirectory(sourceCatalogDir);
                WriteSavedCatalogFiles(saveDir);

                MessageBox.Show(
                    "현재 데이터 저장됨",
                    "저장 완료",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "카탈로그 저장 중 오류가 발생했어: " + ex.Message,
                    "저장 실패",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DeleteDataItems(List<DrivingData> items, int restoreIndex)
        {
            if (items.Count == 0) return;

            ReleaseDataPreviewImage();

            DeleteAction action = new DeleteAction
            {
                Items = new List<DrivingData>(items),
                RestoreIndex = restoreIndex
            };

            foreach (var item in items)
                _allData.Remove(item);

            _deleteUndoStack.Push(action);
            _currentIndex = Math.Max(0, Math.Min(restoreIndex, _allData.Count - 1));
            ReindexDataItems();
            RefreshUI();
        }

        private string GetSelectedCatalogDirectory()
        {
            if (string.IsNullOrWhiteSpace(_selectedDataFolderPath) || !Directory.Exists(_selectedDataFolderPath))
                throw new InvalidOperationException("선택된 카탈로그 폴더를 찾을 수 없어.");

            return _selectedDataFolderPath;
        }

        private string CreateCatalogSaveDirectory(string sourceCatalogDir)
        {
            string saveRoot = Path.Combine(ResolveDataRootPath(sourceCatalogDir), "Save");
            Directory.CreateDirectory(saveRoot);

            string saveDir = Path.Combine(saveRoot, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"));
            Directory.CreateDirectory(saveDir);
            return saveDir;
        }

        private string ResolveImageBasePath(string selectedCatalogPath)
        {
            string directImagesPath = Path.Combine(selectedCatalogPath, "images");
            if (Directory.Exists(directImagesPath)) return selectedCatalogPath;

            return ResolveDataRootPath(selectedCatalogPath);
        }

        private string ResolveDataRootPath(string selectedCatalogPath)
        {
            DirectoryInfo? currentDir = new DirectoryInfo(selectedCatalogPath);
            if (currentDir.Parent != null &&
                currentDir.Parent.Parent != null &&
                currentDir.Parent.Name.Equals("Save", StringComparison.OrdinalIgnoreCase))
            {
                string originalRootPath = currentDir.Parent.Parent.FullName;
                if (Directory.Exists(originalRootPath))
                    return originalRootPath;
            }

            return selectedCatalogPath;
        }

        private bool IsSaveSnapshotPath(string selectedCatalogPath)
        {
            DirectoryInfo? currentDir = new DirectoryInfo(selectedCatalogPath);
            return currentDir.Parent != null &&
                currentDir.Parent.Parent != null &&
                currentDir.Parent.Name.Equals("Save", StringComparison.OrdinalIgnoreCase);
        }

        private string GetTrainingCatalogDirectory()
        {
            if (!string.IsNullOrWhiteSpace(_selectedTrainingSavePath) && Directory.Exists(_selectedTrainingSavePath))
                return _selectedTrainingSavePath;

            if (!string.IsNullOrWhiteSpace(_selectedDataFolderPath) && Directory.Exists(_selectedDataFolderPath))
                return _selectedDataFolderPath;

            throw new InvalidOperationException("학습에 사용할 데이터 폴더가 선택되지 않았어.");
        }

        private void UpdateTrainingSavePathTextBox()
        {
            if (txtTrainingSavePath == null) return;

            txtTrainingSavePath.Text = string.IsNullOrWhiteSpace(_selectedTrainingSavePath)
                ? "(학습용 저장본 경로)"
                : _selectedTrainingSavePath;
        }

        private void ReindexDataItems()
        {
            for (int i = 0; i < _allData.Count; i++)
            {
                _allData[i].Index = i;
            }
        }

        private void ReindexTrainingDataItems()
        {
            for (int i = 0; i < _trainingData.Count; i++)
            {
                _trainingData[i].Index = i;
            }
        }

        private void WriteSavedCatalogFiles(string saveDir)
        {
            string sourceCatalogDir = GetSelectedCatalogDirectory();
            CopyTubMetadataFiles(sourceCatalogDir, saveDir);

            var remainingItemsByCatalog = _allData
                .Where(x => !string.IsNullOrWhiteSpace(x.CatalogFilePath))
                .GroupBy(x => x.CatalogFilePath)
                .ToDictionary(g => g.Key, g => g.OrderBy(x => x.CatalogLineNumber).ToList(), StringComparer.OrdinalIgnoreCase);

            string[] sourceCatalogPaths = Directory
                .GetFiles(sourceCatalogDir, "*.catalog")
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            foreach (string sourceCatalogPath in sourceCatalogPaths)
            {
                string targetCatalogPath = Path.Combine(saveDir, Path.GetFileName(sourceCatalogPath));
                string[] catalogLines = remainingItemsByCatalog.TryGetValue(sourceCatalogPath, out var items)
                    ? items.Select(x => x.CatalogRawLine).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray()
                    : Array.Empty<string>();

                File.WriteAllLines(targetCatalogPath, catalogLines);
                WriteCatalogManifestFile(sourceCatalogPath, targetCatalogPath, catalogLines);
            }
        }

        private void WriteCatalogManifestFile(string sourceCatalogPath, string targetCatalogPath, string[] catalogLines)
        {
            string sourceManifestPath = sourceCatalogPath + "_manifest";
            string targetManifestPath = targetCatalogPath + "_manifest";

            try
            {
                double createdAt = 0;
                string path = Path.GetFileName(targetManifestPath);
                int startIndex = 0;

                if (File.Exists(sourceManifestPath))
                {
                    using JsonDocument doc = JsonDocument.Parse(File.ReadAllText(sourceManifestPath));
                    var root = doc.RootElement;
                    createdAt = root.TryGetProperty("created_at", out var createdAtElement)
                        ? createdAtElement.GetDouble()
                        : 0;
                    path = root.TryGetProperty("path", out var pathElement)
                        ? pathElement.GetString() ?? path
                        : path;
                    startIndex = root.TryGetProperty("start_index", out var startIndexElement)
                        ? startIndexElement.GetInt32()
                        : 0;
                }

                var manifest = new
                {
                    created_at = createdAt,
                    line_lengths = catalogLines.Select(line => line.Length).ToArray(),
                    path,
                    start_index = startIndex
                };

                File.WriteAllText(targetManifestPath, JsonSerializer.Serialize(manifest));
            }
            catch
            {
                File.WriteAllText(targetManifestPath, JsonSerializer.Serialize(new
                {
                    line_lengths = catalogLines.Select(line => line.Length).ToArray(),
                    path = Path.GetFileName(targetManifestPath),
                    start_index = 0
                }));
            }
        }

        private void CopyTubMetadataFiles(string sourceCatalogDir, string targetDir)
        {
            string dataRootPath = ResolveDataRootPath(sourceCatalogDir);

            foreach (string fileName in new[] { "manifest.json", "metadata.json" })
            {
                string sourcePath = Path.Combine(dataRootPath, fileName);
                if (!File.Exists(sourcePath)) continue;

                string targetPath = Path.Combine(targetDir, fileName);
                File.Copy(sourcePath, targetPath, true);
            }
        }

        private void AddTrainingMetadataFiles(string catalogPath, string dataRootPath, string[] trainingCatalogFiles, ZipArchive archive)
        {
            string metadataPath = Path.Combine(catalogPath, "metadata.json");
            if (!File.Exists(metadataPath))
                metadataPath = Path.Combine(dataRootPath, "metadata.json");
            if (File.Exists(metadataPath))
                archive.CreateEntryFromFile(metadataPath, "metadata.json", CompressionLevel.Fastest);

            string manifestPath = Path.Combine(catalogPath, "manifest.json");
            if (!File.Exists(manifestPath))
                manifestPath = Path.Combine(dataRootPath, "manifest.json");
            if (File.Exists(manifestPath))
            {
                string manifestText = BuildTrainingManifestText(manifestPath, trainingCatalogFiles);
                ZipArchiveEntry entry = archive.CreateEntry("manifest.json", CompressionLevel.Fastest);
                using StreamWriter writer = new StreamWriter(entry.Open());
                writer.Write(manifestText);
            }
        }

        private string BuildTrainingManifestText(string sourceManifestPath, string[] trainingCatalogFiles)
        {
            string[] lines = File.ReadAllLines(sourceManifestPath);
            if (lines.Length < 5) return string.Join(Environment.NewLine, lines);

            using JsonDocument pathDoc = JsonDocument.Parse(lines[4]);
            JsonElement root = pathDoc.RootElement;
            int maxLen = root.TryGetProperty("max_len", out var maxLenElement)
                ? maxLenElement.GetInt32()
                : 1000;
            int currentIndex = CalculateTrainingCurrentIndex(trainingCatalogFiles);

            var manifestTail = new
            {
                paths = trainingCatalogFiles.Select(Path.GetFileName).ToArray(),
                current_index = currentIndex,
                max_len = maxLen,
                deleted_indexes = Array.Empty<int>()
            };

            lines[4] = JsonSerializer.Serialize(manifestTail);
            return string.Join(Environment.NewLine, lines);
        }

        private int CalculateTrainingCurrentIndex(string[] trainingCatalogFiles)
        {
            int total = 0;

            foreach (string catalogFile in trainingCatalogFiles)
            {
                string manifestPath = catalogFile + "_manifest";
                if (!File.Exists(manifestPath))
                {
                    total += File.ReadLines(catalogFile).Count();
                    continue;
                }

                try
                {
                    using JsonDocument doc = JsonDocument.Parse(File.ReadAllText(manifestPath));
                    if (doc.RootElement.TryGetProperty("line_lengths", out var lineLengths))
                    {
                        total += lineLengths.GetArrayLength();
                        continue;
                    }
                }
                catch
                {
                }

                total += File.ReadLines(catalogFile).Count();
            }

            return total;
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

        private void ReleasePreviewImages()
        {
            ReleaseDataPreviewImage();
            ReleaseTestPreviewImage();
        }

        private void ReleaseDataPreviewImage()
        {
            Image? dataImage = pbDataPreview.Image;
            pbDataPreview.Image = null;
            dataImage?.Dispose();
        }

        private void ReleaseTestPreviewImage()
        {
            if (pbTestPreview != null)
            {
                Image? testImage = pbTestPreview.Image;
                pbTestPreview.Image = null;
                testImage?.Dispose();
            }
        }

        private void ShowFrame(int index)
        {
            if (pbTestPreview == null || _trainingData.Count == 0) return;

            int frameIndex = Math.Max(0, Math.Min(index, _trainingData.Count - 1));
            _testCurrentIndex = frameIndex;
            DrivingData data = _trainingData[frameIndex];
            UpdateTestIndexLabel(frameIndex);

            if (tbTestImageNavigator != null && tbTestImageNavigator.Value != frameIndex)
            {
                tbTestImageNavigator.Value = frameIndex;
            }

            ReleaseTestPreviewImage();
            if (!File.Exists(data.ImagePath)) return;

            using Image originalImage = LoadImageWithoutLock(data.ImagePath);
            Bitmap frameBitmap = CreateBrightnessAdjustedBitmap(originalImage, GetTestBrightnessFactor());

            if (_showTestOverlay)
            {
                DrawControlBars(frameBitmap, data);
            }

            pbTestPreview.Image = frameBitmap;
        }

        private Bitmap CreateBrightnessAdjustedBitmap(Image sourceImage, float brightnessFactor)
        {
            Bitmap bitmap = new Bitmap(sourceImage.Width, sourceImage.Height);
            using Graphics graphics = Graphics.FromImage(bitmap);

            ColorMatrix colorMatrix = new ColorMatrix(new[]
            {
                new[] { brightnessFactor, 0f, 0f, 0f, 0f },
                new[] { 0f, brightnessFactor, 0f, 0f, 0f },
                new[] { 0f, 0f, brightnessFactor, 0f, 0f },
                new[] { 0f, 0f, 0f, 1f, 0f },
                new[] { 0f, 0f, 0f, 0f, 1f }
            });

            using ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            graphics.DrawImage(
                sourceImage,
                new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                0,
                0,
                sourceImage.Width,
                sourceImage.Height,
                GraphicsUnit.Pixel,
                attributes);

            return bitmap;
        }

        private void DrawControlBars(Bitmap frameBitmap, DrivingData data)
        {
            using Graphics graphics = Graphics.FromImage(frameBitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            float centerX = frameBitmap.Width / 2f;
            float bottomY = frameBitmap.Height - Math.Max(18f, frameBitmap.Height * 0.08f);
            PointF origin = new PointF(centerX, bottomY);
            float maxLength = Math.Max(18f, Math.Min(frameBitmap.Width, frameBitmap.Height) * 0.76f);

            DrawControlBar(
                graphics,
                origin,
                data.Steering,
                NormalizeThrottle(data.Speed),
                Color.FromArgb(45, 212, 120),
                maxLength);

            if (data.HasPrediction)
            {
                DrawControlBar(
                    graphics,
                    origin,
                    data.PredictedSteering,
                    NormalizeThrottle(data.PredictedSpeed),
                    Color.FromArgb(59, 130, 246),
                    maxLength);
            }
        }

        private void DrawControlBar(Graphics graphics, PointF start, double angle, double throttle, Color color, float maxLength)
        {
            double clampedAngle = Math.Max(-1.0, Math.Min(1.0, angle));
            double clampedThrottle = Math.Max(0.0, Math.Min(1.0, throttle));

            double radians = clampedAngle * (Math.PI / 2.0);

            float minLength = 18f;
            float length = (minLength + (float)clampedThrottle * (maxLength - minLength)) * 2f;
            PointF end = new PointF(
                start.X + (float)Math.Sin(radians) * length,
                start.Y - (float)Math.Cos(radians) * length);

            using Pen shadowPen = new Pen(Color.FromArgb(150, 0, 0, 0), 5f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
            using Pen barPen = new Pen(color, 2.5f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
            using SolidBrush baseBrush = new SolidBrush(color);

            graphics.DrawLine(shadowPen, start, end);
            graphics.DrawLine(barPen, start, end);
            graphics.FillEllipse(baseBrush, start.X - 3f, start.Y - 3f, 6f, 6f);
        }

        private double NormalizeThrottle(double value)
        {
            double throttle = value > 1.0 ? value / 100.0 : value;
            return Math.Max(0.0, Math.Min(1.0, throttle));
        }

        private Image LoadImageWithoutLock(string imagePath)
        {
            using var stream = new MemoryStream(File.ReadAllBytes(imagePath));
            using var image = Image.FromStream(stream);
            return new Bitmap(image);
        }

        private bool TryGetSelectedListViewDataItems(out List<DrivingData> selectedItems, out int restoreIndex)
        {
            var selectedIndexes = lvDataItems.SelectedItems
                .Cast<ListViewItem>()
                .Select(item => int.TryParse(item.Text, out int idx) ? idx : (int?)null)
                .Where(idx => idx.HasValue)
                .Select(idx => idx!.Value)
                .ToHashSet();

            selectedItems = _allData
                .Where(data => selectedIndexes.Contains(data.Index))
                .ToList();

            restoreIndex = selectedItems.Count == 0
                ? -1
                : selectedItems.Min(item => _allData.IndexOf(item));

            return selectedItems.Count > 0;
        }

        private void RefreshDataListView(bool preserveScroll = true)
        {
            int topItemIndex = preserveScroll && lvDataItems.TopItem != null
                ? lvDataItems.TopItem.Index
                : -1;

            lvDataItems.BeginUpdate();
            lvDataItems.Items.Clear();
            foreach (var d in _allData.OrderBy(x => x.Index))
            {
                var item = new ListViewItem(d.Index.ToString());
                item.SubItems.Add(Path.GetFileName(d.ImagePath));
                lvDataItems.Items.Add(item);
            }
            lvDataItems.EndUpdate();

            if (topItemIndex >= 0 && lvDataItems.Items.Count > 0)
            {
                int restoredTopIndex = Math.Min(topItemIndex, lvDataItems.Items.Count - 1);
                try
                {
                    lvDataItems.TopItem = lvDataItems.Items[restoredTopIndex];
                }
                catch
                {
                    lvDataItems.Items[restoredTopIndex].EnsureVisible();
                }
            }
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
            if (chtSteeringValue != null) chtSteeringValue.Series[0].Points.Clear();
            if (chtSpeedValue != null) chtSpeedValue.Series[0].Points.Clear();
            if (chtTestSteeringValue != null)
            {
                chtTestSteeringValue.Series["Actual"].Points.Clear();
                chtTestSteeringValue.Series["Predict"].Points.Clear();
            }
            if (chtTestSpeedValue != null)
            {
                chtTestSpeedValue.Series["Actual"].Points.Clear();
                chtTestSpeedValue.Series["Predict"].Points.Clear();
            }

            if (_allData.Count > 0)
            {
                int dataStep = Math.Max(1, _allData.Count / 100);
                int dataMaxIdx = _allData.Count - 1;

                for (int i = 0; i < _allData.Count; i += dataStep)
                {
                    var d = _allData[i];
                    if (chtSteeringValue != null) chtSteeringValue.Series[0].Points.AddXY(d.Index, d.Steering);
                    if (chtSpeedValue != null) chtSpeedValue.Series[0].Points.AddXY(d.Index, d.Speed);
                }

                foreach (var c in new[] { chtSteeringValue, chtSpeedValue })
                {
                    if (c == null) continue;
                    c.ChartAreas[0].AxisX.Minimum = 0; c.ChartAreas[0].AxisX.Maximum = dataMaxIdx;
                    c.ChartAreas[0].AxisX.LabelStyle.Format = "0;0;0";
                    c.ChartAreas[0].RecalculateAxesScale();
                    c.Invalidate();
                }
            }

            if (_trainingData.Count == 0) return;

            int testStep = Math.Max(1, _trainingData.Count / (_showTestOverlay ? 500 : 100));
            int testMaxIdx = _trainingData.Count - 1;

            for (int i = 0; i < _trainingData.Count; i += testStep)
            {
                var d = _trainingData[i];

                if (chtTestSteeringValue != null)
                {
                    chtTestSteeringValue.Series["Actual"].Points.AddXY(d.Index, d.Steering);
                    if (_showTestOverlay && d.HasPrediction)
                        chtTestSteeringValue.Series["Predict"].Points.AddXY(d.Index, d.PredictedSteering);
                }
                if (chtTestSpeedValue != null)
                {
                    chtTestSpeedValue.Series["Actual"].Points.AddXY(d.Index, d.Speed);
                    if (_showTestOverlay && d.HasPrediction)
                        chtTestSpeedValue.Series["Predict"].Points.AddXY(d.Index, d.PredictedSpeed);
                }
            }

            foreach (var c in new[] { chtTestSteeringValue, chtTestSpeedValue })
            {
                if (c == null) continue;
                c.ChartAreas[0].AxisX.Minimum = 0; c.ChartAreas[0].AxisX.Maximum = testMaxIdx;
                c.ChartAreas[0].AxisX.LabelStyle.Format = "0;0;0";
                c.ChartAreas[0].RecalculateAxesScale();
                c.Invalidate();
            }
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

        private void InitializeDataInfoGrid() { dgvDataInfo.Rows.Clear(); dgvDataInfo.Rows.Add("데이터 수", "0"); dgvDataInfo.Rows.Add("이미지 인덱스", "0"); dgvDataInfo.Rows.Add("조향값", "0"); dgvDataInfo.Rows.Add("속도", "0"); }
        private void UpdatePlaybackSpeedLabel()
        {
            string speedText = $"x{tbPlaybackSpeed.Value / 100.0:0.##}";
            if (lblPlaybackSpeed != null) lblPlaybackSpeed.Text = speedText;
        }
        private int GetPlaybackInterval() { return Math.Max(1, (int)(BasePlaybackIntervalMs / (tbPlaybackSpeed.Value / 100.0))); }

        private void UpdateTestPlaybackSpeedLabel()
        {
            if (lblTestPlaybackSpeed != null) lblTestPlaybackSpeed.Text = $"배속 x{tbTestPlaybackSpeed.Value / 100.0:0.##}";
        }

        private int GetTestPlaybackInterval()
        {
            return Math.Max(1, (int)(BasePlaybackIntervalMs / (tbTestPlaybackSpeed.Value / 100.0)));
        }

        private float GetTestBrightnessFactor()
        {
            if (tbTestBrightness == null) return 1f;

            return tbTestBrightness.Value / 100f;
        }

        private bool IsDefaultTestBrightness(float brightnessFactor)
        {
            return Math.Abs(brightnessFactor - 1f) < 0.001f;
        }

        private string GetBrightnessToken(float brightnessFactor)
        {
            return Math.Round(brightnessFactor * 100).ToString("000", CultureInfo.InvariantCulture);
        }

        private void UpdateTestBrightnessLabel()
        {
            if (lblTestBrightness != null) lblTestBrightness.Text = $"밝기 x{GetTestBrightnessFactor():0.##}";
        }

        private void tbPlaybackSpeed_Scroll(object sender, EventArgs e)
        {
            if (!EnsureDataLoaded()) return;

            UpdatePlaybackSpeedLabel();
            if (_playTimer.Enabled) _playTimer.Interval = GetPlaybackInterval();
        }

        private void tbTestPlaybackSpeed_Scroll(object? sender, EventArgs e)
        {
            if (!EnsureTrainingDataLoaded()) return;

            UpdateTestPlaybackSpeedLabel();
            if (_testPlayTimer.Enabled) _testPlayTimer.Interval = GetTestPlaybackInterval();
        }

        private void tbTestBrightness_Scroll(object sender, EventArgs e)
        {
            if (!EnsureTrainingDataLoaded()) return;

            UpdateTestBrightnessLabel();
            ShowFrame(_testCurrentIndex);
        }

        private void btnSetRange_Click(object sender, EventArgs e) { if (!EnsureDataLoaded()) return; _isRangeSettingMode = true; }
        private void btnCancelRange_Click(object sender, EventArgs e) { if (!EnsureDataLoaded()) return; ClearMarkers(); }
        private void gbDataContent_Resize(object sender, EventArgs e) { foreach (var m in _imageRangeMarkers) if (m.Tag is int val) m.Left = GetImageNavigatorMarkerLeft(val, m.Size); if (_allData.Count > 0) UpdateCharts(); }

        #endregion

        private void dgvDataInfo_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lvDataItems_SelectedIndexChanged(object sender, EventArgs e) { if (lvDataItems.SelectedItems.Count > 0 && int.TryParse(lvDataItems.SelectedItems[0].Text, out int idx)) { _currentIndex = _allData.FindIndex(x => x.Index == idx); UpdateDisplay(); } }
        private void lvDataItems_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            ListViewItem? item = lvDataItems.GetItemAt(e.X, e.Y);
            _listViewDragAnchorIndex = item?.Index ?? -1;
        }

        private void lvDataItems_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _listViewDragAnchorIndex < 0) return;

            ListViewItem? item = lvDataItems.GetItemAt(e.X, e.Y);
            if (item == null || item.Index == _listViewDragAnchorIndex) return;

            SelectListViewRange(_listViewDragAnchorIndex, item.Index);
        }

        private void lvDataItems_MouseUp(object? sender, MouseEventArgs e)
        {
            _listViewDragAnchorIndex = -1;
        }

        private void SelectListViewRange(int startIndex, int endIndex)
        {
            int min = Math.Max(0, Math.Min(startIndex, endIndex));
            int max = Math.Min(lvDataItems.Items.Count - 1, Math.Max(startIndex, endIndex));

            lvDataItems.BeginUpdate();
            for (int i = 0; i < lvDataItems.Items.Count; i++)
                lvDataItems.Items[i].Selected = i >= min && i <= max;
            lvDataItems.EndUpdate();
        }

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

        private void lblTestPlaybackSpeed_Click(object sender, EventArgs e)
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
        public bool HasPrediction { get; set; }
        public string CatalogRawLine { get; set; } = "";
        public string CatalogFilePath { get; set; } = "";
        public string CatalogImageName { get; set; } = "";
        public int CatalogLineNumber { get; set; } = -1;
    }
}
