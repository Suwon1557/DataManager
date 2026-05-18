namespace DataManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TabControl tcMain;
        private TabPage tpDataManager;
        private TabPage tpTrainingTest;
        private GroupBox gbTrainingSetup;
        private GroupBox gbModelTest;
        private TableLayoutPanel tlpModelTest;
        private TableLayoutPanel tlpTestPreview;
        private TableLayoutPanel tlpTestCharts;
        private TrackBar tbTestImageNavigator;
        private PictureBox pbTestPreview;
        private Button btnStartTest;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTestSteeringValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTestSpeedValue;
        private Button btnTrain;
        private TextBox txtTrainingLog;
        private GroupBox gbDataLoad;
        private GroupBox gbDataContent;
        private PictureBox pbDataPreview;
        private ListView lvDataItems;
        private DataGridView dgvDataInfo;
        private DataGridViewTextBoxColumn colDataName;
        private DataGridViewTextBoxColumn colDataValue;
        private Button btnSelectFolder;
        private TextBox txtFolderPath;
        private Button btnCheckDataIntegrity;
        private Label lblTitle;
        private Button btnReverse;
        private Button btnStop;
        private Button btnPlay;
        private TrackBar tbPlaybackSpeed;
        private Label lblPlaybackSpeed;
        private TrackBar tbImageNavigator;
        private Button btnSetRange;
        private Button btnCancelRange;
        private Button btnCancelDelete;
        private Button btnDelete;
        private Button btnFilter;
        private Panel pnlImageRangeMarker;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtSteeringValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtSpeedValue;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tcMain = new TabControl();
            tpDataManager = new TabPage();
            gbDataContent = new GroupBox();
            btnSetRange = new Button();
            btnCancelRange = new Button();
            pnlImageRangeMarker = new Panel();
            tbImageNavigator = new TrackBar();
            btnCancelDelete = new Button();
            btnDelete = new Button();
            btnFilter = new Button();
            lblPlaybackSpeed = new Label();
            tbPlaybackSpeed = new TrackBar();
            btnReverse = new Button();
            btnStop = new Button();
            btnPlay = new Button();
            dgvDataInfo = new DataGridView();
            colDataName = new DataGridViewTextBoxColumn();
            colDataValue = new DataGridViewTextBoxColumn();
            lvDataItems = new ListView();
            pbDataPreview = new PictureBox();
            gbDataLoad = new GroupBox();
            btnCheckDataIntegrity = new Button();
            txtFolderPath = new TextBox();
            btnSelectFolder = new Button();
            tpTrainingTest = new TabPage();
            gbModelTest = new GroupBox();
            tlpModelTest = new TableLayoutPanel();
            tlpTestPreview = new TableLayoutPanel();
            pbTestPreview = new PictureBox();
            btnStartTest = new Button();
            tlpTestCharts = new TableLayoutPanel();
            tbTestImageNavigator = new TrackBar();
            chtTestSteeringValue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chtTestSpeedValue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            gbTrainingSetup = new GroupBox();
            txtTrainingLog = new TextBox();
            btnTrain = new Button();
            lblTitle = new Label();
            chtSteeringValue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chtSpeedValue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tcMain.SuspendLayout();
            tpDataManager.SuspendLayout();
            gbDataContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbImageNavigator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbPlaybackSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDataInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbDataPreview).BeginInit();
            gbDataLoad.SuspendLayout();
            tpTrainingTest.SuspendLayout();
            gbModelTest.SuspendLayout();
            tlpModelTest.SuspendLayout();
            tlpTestPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbTestPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbTestImageNavigator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chtTestSteeringValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chtTestSpeedValue).BeginInit();
            gbTrainingSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chtSteeringValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chtSpeedValue).BeginInit();
            SuspendLayout();
            // 
            // tcMain
            // 
            tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tcMain.Controls.Add(tpDataManager);
            tcMain.Controls.Add(tpTrainingTest);
            tcMain.Location = new Point(0, 40);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new Size(800, 810);
            tcMain.TabIndex = 0;
            // 
            // tpDataManager
            // 
            tpDataManager.Controls.Add(gbDataContent);
            tpDataManager.Controls.Add(gbDataLoad);
            tpDataManager.Location = new Point(4, 24);
            tpDataManager.Name = "tpDataManager";
            tpDataManager.Padding = new Padding(3);
            tpDataManager.Size = new Size(792, 782);
            tpDataManager.TabIndex = 0;
            tpDataManager.Text = "데이터 관리";
            tpDataManager.UseVisualStyleBackColor = true;
            tpDataManager.Click += tpDataManager_Click;
            // 
            // gbDataContent
            // 
            gbDataContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbDataContent.Controls.Add(btnSetRange);
            gbDataContent.Controls.Add(btnCancelRange);
            gbDataContent.Controls.Add(pnlImageRangeMarker);
            gbDataContent.Controls.Add(tbImageNavigator);
            gbDataContent.Controls.Add(btnCancelDelete);
            gbDataContent.Controls.Add(btnDelete);
            gbDataContent.Controls.Add(btnFilter);
            gbDataContent.Controls.Add(lblPlaybackSpeed);
            gbDataContent.Controls.Add(tbPlaybackSpeed);
            gbDataContent.Controls.Add(btnReverse);
            gbDataContent.Controls.Add(btnStop);
            gbDataContent.Controls.Add(btnPlay);
            gbDataContent.Controls.Add(dgvDataInfo);
            gbDataContent.Controls.Add(lvDataItems);
            gbDataContent.Controls.Add(pbDataPreview);
            gbDataContent.Controls.Add(chtSpeedValue);
            gbDataContent.Controls.Add(chtSteeringValue);
            gbDataContent.Font = new Font("함초롬바탕 확장", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataContent.ForeColor = Color.FromArgb(14, 61, 156);
            gbDataContent.Location = new Point(3, 109);
            gbDataContent.Name = "gbDataContent";
            gbDataContent.Size = new Size(786, 670);
            gbDataContent.TabIndex = 1;
            gbDataContent.TabStop = false;
            gbDataContent.Text = "데이터 탐색";
            gbDataContent.Enter += gbDataContent_Enter;
            gbDataContent.Resize += gbDataContent_Resize;
            // 
            // btnSetRange
            // 
            btnSetRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSetRange.ForeColor = Color.Black;
            btnSetRange.Location = new Point(540, 318);
            btnSetRange.Name = "btnSetRange";
            btnSetRange.Size = new Size(111, 34);
            btnSetRange.TabIndex = 14;
            btnSetRange.Text = "범위 설정";
            btnSetRange.UseVisualStyleBackColor = true;
            btnSetRange.Click += btnSetRange_Click;
            // 
            // btnCancelRange
            // 
            btnCancelRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelRange.ForeColor = Color.Black;
            btnCancelRange.Location = new Point(663, 318);
            btnCancelRange.Name = "btnCancelRange";
            btnCancelRange.Size = new Size(111, 34);
            btnCancelRange.TabIndex = 12;
            btnCancelRange.Text = "X";
            btnCancelRange.UseVisualStyleBackColor = true;
            btnCancelRange.Click += btnCancelRange_Click;
            // 
            // pnlImageRangeMarker
            // 
            pnlImageRangeMarker.BackColor = Color.FromArgb(255, 114, 16);
            pnlImageRangeMarker.Location = new Point(12, 367);
            pnlImageRangeMarker.Name = "pnlImageRangeMarker";
            pnlImageRangeMarker.Size = new Size(12, 12);
            pnlImageRangeMarker.TabIndex = 13;
            pnlImageRangeMarker.Visible = false;
            // 
            // tbImageNavigator
            // 
            tbImageNavigator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbImageNavigator.Location = new Point(12, 354);
            tbImageNavigator.Maximum = 100;
            tbImageNavigator.Name = "tbImageNavigator";
            tbImageNavigator.Size = new Size(762, 45);
            tbImageNavigator.TabIndex = 11;
            tbImageNavigator.MouseUp += tbImageNavigator_MouseUp;
            // 
            // btnCancelDelete
            // 
            btnCancelDelete.BackgroundImage = (Image)resources.GetObject("btnCancelDelete.BackgroundImage");
            btnCancelDelete.BackgroundImageLayout = ImageLayout.Zoom;
            btnCancelDelete.ForeColor = Color.Black;
            btnCancelDelete.Location = new Point(319, 234);
            btnCancelDelete.Name = "btnCancelDelete";
            btnCancelDelete.Size = new Size(208, 34);
            btnCancelDelete.TabIndex = 10;
            btnCancelDelete.UseVisualStyleBackColor = true;
            btnCancelDelete.Click += btnCancelDelete_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackgroundImage = (Image)resources.GetObject("btnDelete.BackgroundImage");
            btnDelete.BackgroundImageLayout = ImageLayout.Zoom;
            btnDelete.ForeColor = Color.Black;
            btnDelete.Location = new Point(431, 194);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(96, 34);
            btnDelete.TabIndex = 9;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnFilter
            // 
            btnFilter.ForeColor = Color.Black;
            btnFilter.Location = new Point(319, 194);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(96, 34);
            btnFilter.TabIndex = 8;
            btnFilter.Text = "필터링";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // lblPlaybackSpeed
            // 
            lblPlaybackSpeed.AutoSize = true;
            lblPlaybackSpeed.Font = new Font("한컴 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblPlaybackSpeed.ForeColor = Color.FromArgb(14, 61, 156);
            lblPlaybackSpeed.Location = new Point(319, 307);
            lblPlaybackSpeed.Name = "lblPlaybackSpeed";
            lblPlaybackSpeed.Size = new Size(45, 35);
            lblPlaybackSpeed.TabIndex = 7;
            lblPlaybackSpeed.Text = "x1";
            // 
            // tbPlaybackSpeed
            // 
            tbPlaybackSpeed.LargeChange = 25;
            tbPlaybackSpeed.Location = new Point(12, 307);
            tbPlaybackSpeed.Maximum = 200;
            tbPlaybackSpeed.Minimum = 25;
            tbPlaybackSpeed.Name = "tbPlaybackSpeed";
            tbPlaybackSpeed.Size = new Size(300, 45);
            tbPlaybackSpeed.SmallChange = 25;
            tbPlaybackSpeed.TabIndex = 6;
            tbPlaybackSpeed.TickFrequency = 25;
            tbPlaybackSpeed.Value = 100;
            tbPlaybackSpeed.Scroll += tbPlaybackSpeed_Scroll;
            // 
            // btnReverse
            // 
            btnReverse.ForeColor = Color.Black;
            btnReverse.Location = new Point(176, 269);
            btnReverse.Name = "btnReverse";
            btnReverse.Size = new Size(76, 32);
            btnReverse.TabIndex = 5;
            btnReverse.Text = "<<";
            btnReverse.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.ForeColor = Color.Black;
            btnStop.Location = new Point(94, 269);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(76, 32);
            btnStop.TabIndex = 4;
            btnStop.Text = "| |";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            btnPlay.ForeColor = Color.Black;
            btnPlay.Location = new Point(12, 269);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(76, 32);
            btnPlay.TabIndex = 3;
            btnPlay.Text = ">>";
            btnPlay.UseVisualStyleBackColor = true;
            // 
            // dgvDataInfo
            // 
            dgvDataInfo.AllowUserToAddRows = false;
            dgvDataInfo.AllowUserToDeleteRows = false;
            dgvDataInfo.AllowUserToResizeColumns = false;
            dgvDataInfo.AllowUserToResizeRows = false;
            dgvDataInfo.BackgroundColor = SystemColors.Window;
            dgvDataInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDataInfo.Columns.AddRange(new DataGridViewColumn[] { colDataName, colDataValue });
            dgvDataInfo.Font = new Font("한컴 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dgvDataInfo.Location = new Point(318, 36);
            dgvDataInfo.MultiSelect = false;
            dgvDataInfo.Name = "dgvDataInfo";
            dgvDataInfo.ReadOnly = true;
            dgvDataInfo.RowHeadersVisible = false;
            dgvDataInfo.ScrollBars = ScrollBars.None;
            dgvDataInfo.Size = new Size(216, 138);
            dgvDataInfo.TabIndex = 2;
            dgvDataInfo.Text = "(폴더경로)";
            // 
            // colDataName
            // 
            colDataName.HeaderText = "데이터";
            colDataName.Name = "colDataName";
            colDataName.ReadOnly = true;
            colDataName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colDataName.Width = 128;
            // 
            // colDataValue
            // 
            colDataValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDataValue.HeaderText = "값";
            colDataValue.Name = "colDataValue";
            colDataValue.ReadOnly = true;
            colDataValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // lvDataItems
            // 
            lvDataItems.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lvDataItems.Font = new Font("맑은 고딕", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lvDataItems.ForeColor = Color.Black;
            lvDataItems.FullRowSelect = true;
            lvDataItems.GridLines = true;
            lvDataItems.Location = new Point(540, 36);
            lvDataItems.Name = "lvDataItems";
            lvDataItems.Size = new Size(234, 227);
            lvDataItems.TabIndex = 1;
            lvDataItems.UseCompatibleStateImageBehavior = false;
            lvDataItems.View = View.Details;
            // 
            // pbDataPreview
            // 
            pbDataPreview.BackColor = Color.White;
            pbDataPreview.BorderStyle = BorderStyle.FixedSingle;
            pbDataPreview.Location = new Point(12, 36);
            pbDataPreview.Name = "pbDataPreview";
            pbDataPreview.Size = new Size(300, 227);
            pbDataPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pbDataPreview.TabIndex = 0;
            pbDataPreview.TabStop = false;
            // 
            // chtSteeringValue
            // 
            chtSteeringValue.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            chartArea1.AxisX.Maximum = 1000D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            chtSteeringValue.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            chtSteeringValue.Legends.Add(legend1);
            chtSteeringValue.Location = new Point(12, 430);
            chtSteeringValue.Name = "chtSteeringValue";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = Color.FromArgb(14, 61, 156);
            series1.Legend = "Legend1";
            series1.Name = "조향값";
            series1.Points.AddXY(0D, 0D);
            series1.Points.AddXY(1000D, 0D);
            chtSteeringValue.Series.Add(series1);
            chtSteeringValue.Size = new Size(762, 105);
            chtSteeringValue.TabIndex = 15;
            // 
            // chtSpeedValue
            // 
            chtSpeedValue.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            chartArea2.AxisX.Maximum = 1000D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.Name = "ChartArea1";
            chtSpeedValue.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            chtSpeedValue.Legends.Add(legend2);
            chtSpeedValue.Location = new Point(12, 547);
            chtSpeedValue.Name = "chtSpeedValue";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = Color.FromArgb(255, 114, 16);
            series2.Legend = "Legend1";
            series2.Name = "속도값";
            series2.Points.AddXY(0D, 0D);
            series2.Points.AddXY(1000D, 0D);
            chtSpeedValue.Series.Add(series2);
            chtSpeedValue.Size = new Size(762, 105);
            chtSpeedValue.TabIndex = 16;
            // 
            // gbDataLoad
            // 
            gbDataLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataLoad.Controls.Add(btnCheckDataIntegrity);
            gbDataLoad.Controls.Add(txtFolderPath);
            gbDataLoad.Controls.Add(btnSelectFolder);
            gbDataLoad.Font = new Font("함초롬바탕 확장", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataLoad.ForeColor = Color.FromArgb(14, 61, 156);
            gbDataLoad.Location = new Point(3, 3);
            gbDataLoad.Name = "gbDataLoad";
            gbDataLoad.Size = new Size(786, 100);
            gbDataLoad.TabIndex = 0;
            gbDataLoad.TabStop = false;
            gbDataLoad.Text = "데이터 불러오기";
            // 
            // btnCheckDataIntegrity
            // 
            btnCheckDataIntegrity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCheckDataIntegrity.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCheckDataIntegrity.ForeColor = Color.Black;
            btnCheckDataIntegrity.Location = new Point(582, 34);
            btnCheckDataIntegrity.Name = "btnCheckDataIntegrity";
            btnCheckDataIntegrity.Size = new Size(192, 38);
            btnCheckDataIntegrity.TabIndex = 2;
            btnCheckDataIntegrity.Text = "데이터 무결성 검사";
            btnCheckDataIntegrity.UseVisualStyleBackColor = true;
            btnCheckDataIntegrity.Click += btnCheckDataIntegrity_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.Font = new Font("한컴 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtFolderPath.ForeColor = SystemColors.GrayText;
            txtFolderPath.Location = new Point(161, 37);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(412, 35);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.Text = "(폴더경로)";
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.BackgroundImage = (Image)resources.GetObject("btnSelectFolder.BackgroundImage");
            btnSelectFolder.BackgroundImageLayout = ImageLayout.Zoom;
            btnSelectFolder.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectFolder.ForeColor = Color.Black;
            btnSelectFolder.Location = new Point(12, 34);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(140, 38);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectAdd_Click;
            // 
            // tpTrainingTest
            // 
            tpTrainingTest.Controls.Add(gbModelTest);
            tpTrainingTest.Controls.Add(gbTrainingSetup);
            tpTrainingTest.Location = new Point(4, 24);
            tpTrainingTest.Name = "tpTrainingTest";
            tpTrainingTest.Padding = new Padding(3);
            tpTrainingTest.Size = new Size(792, 782);
            tpTrainingTest.TabIndex = 1;
            tpTrainingTest.Text = "학습/테스트";
            tpTrainingTest.UseVisualStyleBackColor = true;
            // 
            // gbModelTest
            // 
            gbModelTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbModelTest.Controls.Add(tlpModelTest);
            gbModelTest.Font = new Font("함초롬바탕 확장", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbModelTest.ForeColor = Color.FromArgb(14, 61, 156);
            gbModelTest.Location = new Point(3, 139);
            gbModelTest.Name = "gbModelTest";
            gbModelTest.Size = new Size(786, 640);
            gbModelTest.TabIndex = 1;
            gbModelTest.TabStop = false;
            gbModelTest.Text = "모델 테스트";
            // 
            // tlpModelTest
            // 
            tlpModelTest.ColumnCount = 2;
            tlpModelTest.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 312F));
            tlpModelTest.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpModelTest.Controls.Add(tlpTestPreview, 0, 0);
            tlpModelTest.Controls.Add(tlpTestCharts, 1, 0);
            tlpModelTest.Controls.Add(tbTestImageNavigator, 0, 1);
            tlpModelTest.Dock = DockStyle.Fill;
            tlpModelTest.Location = new Point(3, 28);
            tlpModelTest.Name = "tlpModelTest";
            tlpModelTest.RowCount = 2;
            tlpModelTest.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpModelTest.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpModelTest.Size = new Size(780, 639);
            tlpModelTest.TabIndex = 0;
            // 
            // tlpTestPreview
            // 
            tlpTestPreview.ColumnCount = 1;
            tlpTestPreview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTestPreview.Controls.Add(pbTestPreview, 0, 0);
            tlpTestPreview.Controls.Add(btnStartTest, 0, 1);
            tlpTestPreview.Dock = DockStyle.Fill;
            tlpTestPreview.Location = new Point(3, 3);
            tlpTestPreview.Name = "tlpTestPreview";
            tlpTestPreview.RowCount = 3;
            tlpTestPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 289F));
            tlpTestPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpTestPreview.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTestPreview.Size = new Size(306, 588);
            tlpTestPreview.TabIndex = 0;
            tlpTestPreview.Paint += tlpTestPreview_Paint;
            // 
            // pbTestPreview
            // 
            pbTestPreview.BackColor = Color.White;
            pbTestPreview.BorderStyle = BorderStyle.FixedSingle;
            pbTestPreview.Location = new Point(3, 3);
            pbTestPreview.Name = "pbTestPreview";
            pbTestPreview.Size = new Size(300, 283);
            pbTestPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pbTestPreview.TabIndex = 0;
            pbTestPreview.TabStop = false;
            // 
            // btnStartTest
            // 
            btnStartTest.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnStartTest.ForeColor = Color.Black;
            btnStartTest.Location = new Point(3, 292);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(300, 44);
            btnStartTest.TabIndex = 1;
            btnStartTest.Text = "테스트 시작";
            btnStartTest.UseVisualStyleBackColor = true;
            // 
            // tlpTestCharts
            // 
            tlpTestCharts.ColumnCount = 1;
            tlpTestCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTestCharts.Controls.Add(chtTestSteeringValue, 0, 0);
            tlpTestCharts.Controls.Add(chtTestSpeedValue, 0, 1);
            tlpTestCharts.Dock = DockStyle.Fill;
            tlpTestCharts.Location = new Point(324, 3);
            tlpTestCharts.Margin = new Padding(12, 3, 3, 3);
            tlpTestCharts.Name = "tlpTestCharts";
            tlpTestCharts.RowCount = 2;
            tlpTestCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTestCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTestCharts.Size = new Size(453, 588);
            tlpTestCharts.TabIndex = 1;
            // 
            // tbTestImageNavigator
            // 
            tlpModelTest.SetColumnSpan(tbTestImageNavigator, 2);
            tbTestImageNavigator.Dock = DockStyle.Fill;
            tbTestImageNavigator.Location = new Point(3, 597);
            tbTestImageNavigator.Maximum = 100;
            tbTestImageNavigator.Name = "tbTestImageNavigator";
            tbTestImageNavigator.Size = new Size(774, 39);
            tbTestImageNavigator.TabIndex = 2;
            // 
            // chtTestSteeringValue
            // 
            chartArea3.AxisX.Maximum = 1000D;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.Name = "ChartArea1";
            chtTestSteeringValue.ChartAreas.Add(chartArea3);
            chtTestSteeringValue.Dock = DockStyle.Fill;
            legend3.Name = "Legend1";
            chtTestSteeringValue.Legends.Add(legend3);
            chtTestSteeringValue.Name = "chtTestSteeringValue";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = Color.FromArgb(255, 114, 16);
            series3.Legend = "Legend1";
            series3.Name = "실제값";
            series3.Points.AddXY(0D, 0D);
            series3.Points.AddXY(1000D, 0D);
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = Color.FromArgb(14, 61, 156);
            series4.Legend = "Legend1";
            series4.Name = "예측값";
            series4.Points.AddXY(0D, 0D);
            series4.Points.AddXY(1000D, 0D);
            chtTestSteeringValue.Series.Add(series3);
            chtTestSteeringValue.Series.Add(series4);
            chtTestSteeringValue.Size = new Size(453, 288);
            chtTestSteeringValue.TabIndex = 0;
            chtTestSteeringValue.BorderlineColor = Color.FromArgb(229, 231, 235);
            chtTestSteeringValue.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chtTestSteeringValue.BorderlineWidth = 1;
            // 
            // chtTestSpeedValue
            // 
            chartArea4.AxisX.Maximum = 1000D;
            chartArea4.AxisX.Minimum = 0D;
            chartArea4.Name = "ChartArea1";
            chtTestSpeedValue.ChartAreas.Add(chartArea4);
            chtTestSpeedValue.Dock = DockStyle.Fill;
            legend4.Name = "Legend1";
            chtTestSpeedValue.Legends.Add(legend4);
            chtTestSpeedValue.Name = "chtTestSpeedValue";
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = Color.FromArgb(255, 114, 16);
            series5.Legend = "Legend1";
            series5.Name = "실제값";
            series5.Points.AddXY(0D, 0D);
            series5.Points.AddXY(1000D, 0D);
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = Color.FromArgb(14, 61, 156);
            series6.Legend = "Legend1";
            series6.Name = "예측값";
            series6.Points.AddXY(0D, 0D);
            series6.Points.AddXY(1000D, 0D);
            chtTestSpeedValue.Series.Add(series5);
            chtTestSpeedValue.Series.Add(series6);
            chtTestSpeedValue.Size = new Size(453, 288);
            chtTestSpeedValue.TabIndex = 1;
            chtTestSpeedValue.BorderlineColor = Color.FromArgb(229, 231, 235);
            chtTestSpeedValue.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chtTestSpeedValue.BorderlineWidth = 1;
            // 
            // gbTrainingSetup
            // 
            gbTrainingSetup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTrainingSetup.Controls.Add(txtTrainingLog);
            gbTrainingSetup.Controls.Add(btnTrain);
            gbTrainingSetup.Font = new Font("함초롬바탕 확장", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbTrainingSetup.ForeColor = Color.FromArgb(14, 61, 156);
            gbTrainingSetup.Location = new Point(3, 3);
            gbTrainingSetup.Name = "gbTrainingSetup";
            gbTrainingSetup.Size = new Size(786, 130);
            gbTrainingSetup.TabIndex = 0;
            gbTrainingSetup.TabStop = false;
            gbTrainingSetup.Text = "데이터 학습";
            gbTrainingSetup.Enter += gbTrainingSetup_Enter;
            // 
            // txtTrainingLog
            // 
            txtTrainingLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTrainingLog.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtTrainingLog.ForeColor = Color.Black;
            txtTrainingLog.Location = new Point(161, 34);
            txtTrainingLog.Multiline = true;
            txtTrainingLog.Name = "txtTrainingLog";
            txtTrainingLog.ReadOnly = true;
            txtTrainingLog.ScrollBars = ScrollBars.Vertical;
            txtTrainingLog.Size = new Size(607, 72);
            txtTrainingLog.TabIndex = 1;
            // 
            // btnTrain
            // 
            btnTrain.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTrain.ForeColor = Color.Black;
            btnTrain.Location = new Point(12, 34);
            btnTrain.Name = "btnTrain";
            btnTrain.Size = new Size(140, 38);
            btnTrain.TabIndex = 0;
            btnTrain.Text = "학습";
            btnTrain.UseVisualStyleBackColor = true;
            btnTrain.Click += btnTrain_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Lucida Bright", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(255, 114, 16);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Data Manager";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 850);
            Controls.Add(lblTitle);
            Controls.Add(tcMain);
            MinimumSize = new Size(680, 620);
            Name = "Form1";
            Text = "Form1";
            tcMain.ResumeLayout(false);
            tpDataManager.ResumeLayout(false);
            gbDataContent.ResumeLayout(false);
            gbDataContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbImageNavigator).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbPlaybackSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDataInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbDataPreview).EndInit();
            gbDataLoad.ResumeLayout(false);
            gbDataLoad.PerformLayout();
            tpTrainingTest.ResumeLayout(false);
            gbModelTest.ResumeLayout(false);
            tlpModelTest.ResumeLayout(false);
            tlpModelTest.PerformLayout();
            tlpTestPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbTestPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbTestImageNavigator).EndInit();
            ((System.ComponentModel.ISupportInitialize)chtTestSteeringValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)chtTestSpeedValue).EndInit();
            gbTrainingSetup.ResumeLayout(false);
            gbTrainingSetup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chtSteeringValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)chtSpeedValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
