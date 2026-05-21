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
            gbTrainingSetup = new GroupBox();
            txtTrainingLog = new TextBox();
            btnTrain = new Button();
            lblTitle = new Label();
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
            gbTrainingSetup.SuspendLayout();
            SuspendLayout();
            // 
            // tcMain
            // 
            tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tcMain.Controls.Add(tpDataManager);
            tcMain.Controls.Add(tpTrainingTest);
            tcMain.Location = new Point(0, 85);
            tcMain.Margin = new Padding(6, 6, 6, 6);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new Size(1600, 1728);
            tcMain.TabIndex = 0;
            // 
            // tpDataManager
            // 
            tpDataManager.Controls.Add(gbDataContent);
            tpDataManager.Controls.Add(gbDataLoad);
            tpDataManager.Location = new Point(8, 46);
            tpDataManager.Margin = new Padding(6, 6, 6, 6);
            tpDataManager.Name = "tpDataManager";
            tpDataManager.Padding = new Padding(6, 6, 6, 6);
            tpDataManager.Size = new Size(1584, 1674);
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
            gbDataContent.Font = new Font("Microsoft Sans Serif", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataContent.ForeColor = Color.FromArgb(14, 61, 156);
            gbDataContent.Location = new Point(6, 233);
            gbDataContent.Margin = new Padding(6, 6, 6, 6);
            gbDataContent.Name = "gbDataContent";
            gbDataContent.Padding = new Padding(6, 6, 6, 6);
            gbDataContent.Size = new Size(1572, 1429);
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
            btnSetRange.Location = new Point(1080, 678);
            btnSetRange.Margin = new Padding(6, 6, 6, 6);
            btnSetRange.Name = "btnSetRange";
            btnSetRange.Size = new Size(222, 73);
            btnSetRange.TabIndex = 14;
            btnSetRange.Text = "범위 설정";
            btnSetRange.UseVisualStyleBackColor = true;
            btnSetRange.Click += btnSetRange_Click;
            // 
            // btnCancelRange
            // 
            btnCancelRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelRange.ForeColor = Color.Black;
            btnCancelRange.Location = new Point(1326, 678);
            btnCancelRange.Margin = new Padding(6, 6, 6, 6);
            btnCancelRange.Name = "btnCancelRange";
            btnCancelRange.Size = new Size(222, 73);
            btnCancelRange.TabIndex = 12;
            btnCancelRange.Text = "X";
            btnCancelRange.UseVisualStyleBackColor = true;
            btnCancelRange.Click += btnCancelRange_Click;
            // 
            // pnlImageRangeMarker
            // 
            pnlImageRangeMarker.BackColor = Color.FromArgb(255, 114, 16);
            pnlImageRangeMarker.Location = new Point(24, 783);
            pnlImageRangeMarker.Margin = new Padding(6, 6, 6, 6);
            pnlImageRangeMarker.Name = "pnlImageRangeMarker";
            pnlImageRangeMarker.Size = new Size(24, 26);
            pnlImageRangeMarker.TabIndex = 13;
            pnlImageRangeMarker.Visible = false;
            // 
            // tbImageNavigator
            // 
            tbImageNavigator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbImageNavigator.Location = new Point(24, 755);
            tbImageNavigator.Margin = new Padding(6, 6, 6, 6);
            tbImageNavigator.Maximum = 100;
            tbImageNavigator.Name = "tbImageNavigator";
            tbImageNavigator.Size = new Size(1524, 90);
            tbImageNavigator.TabIndex = 11;
            tbImageNavigator.MouseUp += tbImageNavigator_MouseUp;
            // 
            // btnCancelDelete
            // 
            btnCancelDelete.BackgroundImage = (Image)resources.GetObject("btnCancelDelete.BackgroundImage");
            btnCancelDelete.BackgroundImageLayout = ImageLayout.Zoom;
            btnCancelDelete.ForeColor = Color.Black;
            btnCancelDelete.Location = new Point(638, 499);
            btnCancelDelete.Margin = new Padding(6, 6, 6, 6);
            btnCancelDelete.Name = "btnCancelDelete";
            btnCancelDelete.Size = new Size(416, 73);
            btnCancelDelete.TabIndex = 10;
            btnCancelDelete.UseVisualStyleBackColor = true;
            btnCancelDelete.Click += btnCancelDelete_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackgroundImage = (Image)resources.GetObject("btnDelete.BackgroundImage");
            btnDelete.BackgroundImageLayout = ImageLayout.Zoom;
            btnDelete.ForeColor = Color.Black;
            btnDelete.Location = new Point(862, 414);
            btnDelete.Margin = new Padding(6, 6, 6, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(192, 73);
            btnDelete.TabIndex = 9;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnFilter
            // 
            btnFilter.ForeColor = Color.Black;
            btnFilter.Location = new Point(638, 414);
            btnFilter.Margin = new Padding(6, 6, 6, 6);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(192, 73);
            btnFilter.TabIndex = 8;
            btnFilter.Text = "필터링";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // lblPlaybackSpeed
            // 
            lblPlaybackSpeed.AutoSize = true;
            lblPlaybackSpeed.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblPlaybackSpeed.ForeColor = Color.FromArgb(14, 61, 156);
            lblPlaybackSpeed.Location = new Point(638, 655);
            lblPlaybackSpeed.Margin = new Padding(6, 0, 6, 0);
            lblPlaybackSpeed.Name = "lblPlaybackSpeed";
            lblPlaybackSpeed.Size = new Size(86, 63);
            lblPlaybackSpeed.TabIndex = 7;
            lblPlaybackSpeed.Text = "x1";
            // 
            // tbPlaybackSpeed
            // 
            tbPlaybackSpeed.LargeChange = 1;
            tbPlaybackSpeed.Location = new Point(24, 655);
            tbPlaybackSpeed.Margin = new Padding(6, 6, 6, 6);
            tbPlaybackSpeed.Maximum = 4;
            tbPlaybackSpeed.Name = "tbPlaybackSpeed";
            tbPlaybackSpeed.Size = new Size(600, 90);
            tbPlaybackSpeed.TabIndex = 6;
            tbPlaybackSpeed.Value = 2;
            tbPlaybackSpeed.Scroll += tbPlaybackSpeed_Scroll;
            // 
            // btnReverse
            // 
            btnReverse.ForeColor = Color.Black;
            btnReverse.Location = new Point(352, 574);
            btnReverse.Margin = new Padding(6, 6, 6, 6);
            btnReverse.Name = "btnReverse";
            btnReverse.Size = new Size(152, 68);
            btnReverse.TabIndex = 5;
            btnReverse.Text = "<<";
            btnReverse.UseVisualStyleBackColor = true;
            btnReverse.Click += btnReverse_Click;
            // 
            // btnStop
            // 
            btnStop.ForeColor = Color.Black;
            btnStop.Location = new Point(188, 574);
            btnStop.Margin = new Padding(6, 6, 6, 6);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(152, 68);
            btnStop.TabIndex = 4;
            btnStop.Text = "| |";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnPlay
            // 
            btnPlay.ForeColor = Color.Black;
            btnPlay.Location = new Point(24, 574);
            btnPlay.Margin = new Padding(6, 6, 6, 6);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(152, 68);
            btnPlay.TabIndex = 3;
            btnPlay.Text = ">>";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click_1;
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
            dgvDataInfo.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dgvDataInfo.Location = new Point(636, 77);
            dgvDataInfo.Margin = new Padding(6, 6, 6, 6);
            dgvDataInfo.MultiSelect = false;
            dgvDataInfo.Name = "dgvDataInfo";
            dgvDataInfo.ReadOnly = true;
            dgvDataInfo.RowHeadersVisible = false;
            dgvDataInfo.RowHeadersWidth = 82;
            dgvDataInfo.ScrollBars = ScrollBars.None;
            dgvDataInfo.Size = new Size(432, 294);
            dgvDataInfo.TabIndex = 2;
            dgvDataInfo.Text = "(폴더경로)";
            // 
            // colDataName
            // 
            colDataName.HeaderText = "데이터";
            colDataName.MinimumWidth = 10;
            colDataName.Name = "colDataName";
            colDataName.ReadOnly = true;
            colDataName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colDataName.Width = 128;
            // 
            // colDataValue
            // 
            colDataValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDataValue.HeaderText = "값";
            colDataValue.MinimumWidth = 10;
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
            lvDataItems.Location = new Point(1080, 77);
            lvDataItems.Margin = new Padding(6, 6, 6, 6);
            lvDataItems.Name = "lvDataItems";
            lvDataItems.Size = new Size(464, 480);
            lvDataItems.TabIndex = 1;
            lvDataItems.UseCompatibleStateImageBehavior = false;
            lvDataItems.View = View.Details;
            // 
            // pbDataPreview
            // 
            pbDataPreview.BackColor = Color.White;
            pbDataPreview.BorderStyle = BorderStyle.FixedSingle;
            pbDataPreview.Location = new Point(24, 77);
            pbDataPreview.Margin = new Padding(6, 6, 6, 6);
            pbDataPreview.Name = "pbDataPreview";
            pbDataPreview.Size = new Size(598, 482);
            pbDataPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pbDataPreview.TabIndex = 0;
            pbDataPreview.TabStop = false;
            // 
            // gbDataLoad
            // 
            gbDataLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataLoad.Controls.Add(btnCheckDataIntegrity);
            gbDataLoad.Controls.Add(txtFolderPath);
            gbDataLoad.Controls.Add(btnSelectFolder);
            gbDataLoad.Font = new Font("Microsoft Sans Serif", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataLoad.ForeColor = Color.FromArgb(14, 61, 156);
            gbDataLoad.Location = new Point(6, 6);
            gbDataLoad.Margin = new Padding(6, 6, 6, 6);
            gbDataLoad.Name = "gbDataLoad";
            gbDataLoad.Padding = new Padding(6, 6, 6, 6);
            gbDataLoad.Size = new Size(1572, 213);
            gbDataLoad.TabIndex = 0;
            gbDataLoad.TabStop = false;
            gbDataLoad.Text = "데이터 불러오기";
            // 
            // btnCheckDataIntegrity
            // 
            btnCheckDataIntegrity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCheckDataIntegrity.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCheckDataIntegrity.ForeColor = Color.Black;
            btnCheckDataIntegrity.Location = new Point(1164, 73);
            btnCheckDataIntegrity.Margin = new Padding(6, 6, 6, 6);
            btnCheckDataIntegrity.Name = "btnCheckDataIntegrity";
            btnCheckDataIntegrity.Size = new Size(384, 81);
            btnCheckDataIntegrity.TabIndex = 2;
            btnCheckDataIntegrity.Text = "데이터 무결성 검사";
            btnCheckDataIntegrity.UseVisualStyleBackColor = true;
            btnCheckDataIntegrity.Click += btnCheckDataIntegrity_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtFolderPath.ForeColor = SystemColors.GrayText;
            txtFolderPath.Location = new Point(322, 79);
            txtFolderPath.Margin = new Padding(6, 6, 6, 6);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(820, 55);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.Text = "(폴더경로)";
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.BackgroundImage = (Image)resources.GetObject("btnSelectFolder.BackgroundImage");
            btnSelectFolder.BackgroundImageLayout = ImageLayout.Zoom;
            btnSelectFolder.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectFolder.ForeColor = Color.Black;
            btnSelectFolder.Location = new Point(24, 73);
            btnSelectFolder.Margin = new Padding(6, 6, 6, 6);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(280, 81);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectAdd_Click;
            // 
            // tpTrainingTest
            // 
            tpTrainingTest.Controls.Add(gbModelTest);
            tpTrainingTest.Controls.Add(gbTrainingSetup);
            tpTrainingTest.Location = new Point(8, 46);
            tpTrainingTest.Margin = new Padding(6, 6, 6, 6);
            tpTrainingTest.Name = "tpTrainingTest";
            tpTrainingTest.Padding = new Padding(6, 6, 6, 6);
            tpTrainingTest.Size = new Size(1584, 1674);
            tpTrainingTest.TabIndex = 1;
            tpTrainingTest.Text = "학습/테스트";
            tpTrainingTest.UseVisualStyleBackColor = true;
            // 
            // gbModelTest
            // 
            gbModelTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbModelTest.Controls.Add(tlpModelTest);
            gbModelTest.Font = new Font("Microsoft Sans Serif", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbModelTest.ForeColor = Color.FromArgb(14, 61, 156);
            gbModelTest.Location = new Point(6, 297);
            gbModelTest.Margin = new Padding(6, 6, 6, 6);
            gbModelTest.Name = "gbModelTest";
            gbModelTest.Padding = new Padding(6, 6, 6, 6);
            gbModelTest.Size = new Size(1572, 1365);
            gbModelTest.TabIndex = 1;
            gbModelTest.TabStop = false;
            gbModelTest.Text = "모델 테스트";
            // 
            // tlpModelTest
            // 
            tlpModelTest.ColumnCount = 2;
            tlpModelTest.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 624F));
            tlpModelTest.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpModelTest.Controls.Add(tlpTestPreview, 0, 0);
            tlpModelTest.Controls.Add(tlpTestCharts, 1, 0);
            tlpModelTest.Controls.Add(tbTestImageNavigator, 0, 1);
            tlpModelTest.Dock = DockStyle.Fill;
            tlpModelTest.Location = new Point(6, 50);
            tlpModelTest.Margin = new Padding(6, 6, 6, 6);
            tlpModelTest.Name = "tlpModelTest";
            tlpModelTest.RowCount = 2;
            tlpModelTest.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpModelTest.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            tlpModelTest.Size = new Size(1560, 1309);
            tlpModelTest.TabIndex = 0;
            // 
            // tlpTestPreview
            // 
            tlpTestPreview.ColumnCount = 1;
            tlpTestPreview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTestPreview.Controls.Add(pbTestPreview, 0, 0);
            tlpTestPreview.Controls.Add(btnStartTest, 0, 1);
            tlpTestPreview.Dock = DockStyle.Fill;
            tlpTestPreview.Location = new Point(6, 6);
            tlpTestPreview.Margin = new Padding(6, 6, 6, 6);
            tlpTestPreview.Name = "tlpTestPreview";
            tlpTestPreview.RowCount = 3;
            tlpTestPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 617F));
            tlpTestPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 107F));
            tlpTestPreview.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTestPreview.Size = new Size(612, 1201);
            tlpTestPreview.TabIndex = 0;
            tlpTestPreview.Paint += tlpTestPreview_Paint;
            // 
            // pbTestPreview
            // 
            pbTestPreview.BackColor = Color.White;
            pbTestPreview.BorderStyle = BorderStyle.FixedSingle;
            pbTestPreview.Location = new Point(6, 6);
            pbTestPreview.Margin = new Padding(6, 6, 6, 6);
            pbTestPreview.Name = "pbTestPreview";
            pbTestPreview.Size = new Size(598, 601);
            pbTestPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pbTestPreview.TabIndex = 0;
            pbTestPreview.TabStop = false;
            // 
            // btnStartTest
            // 
            btnStartTest.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnStartTest.ForeColor = Color.Black;
            btnStartTest.Location = new Point(6, 623);
            btnStartTest.Margin = new Padding(6, 6, 6, 6);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(600, 94);
            btnStartTest.TabIndex = 1;
            btnStartTest.Text = "테스트 시작";
            btnStartTest.UseVisualStyleBackColor = true;
            // 
            // tlpTestCharts
            // 
            tlpTestCharts.ColumnCount = 1;
            tlpTestCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTestCharts.Dock = DockStyle.Fill;
            tlpTestCharts.Location = new Point(648, 6);
            tlpTestCharts.Margin = new Padding(24, 6, 6, 6);
            tlpTestCharts.Name = "tlpTestCharts";
            tlpTestCharts.RowCount = 2;
            tlpTestCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTestCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTestCharts.Size = new Size(906, 1201);
            tlpTestCharts.TabIndex = 1;
            // 
            // tbTestImageNavigator
            // 
            tlpModelTest.SetColumnSpan(tbTestImageNavigator, 2);
            tbTestImageNavigator.Dock = DockStyle.Fill;
            tbTestImageNavigator.Location = new Point(6, 1219);
            tbTestImageNavigator.Margin = new Padding(6, 6, 6, 6);
            tbTestImageNavigator.Maximum = 100;
            tbTestImageNavigator.Name = "tbTestImageNavigator";
            tbTestImageNavigator.Size = new Size(1548, 84);
            tbTestImageNavigator.TabIndex = 2;
            // 
            // gbTrainingSetup
            // 
            gbTrainingSetup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTrainingSetup.Controls.Add(txtTrainingLog);
            gbTrainingSetup.Controls.Add(btnTrain);
            gbTrainingSetup.Font = new Font("Microsoft Sans Serif", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbTrainingSetup.ForeColor = Color.FromArgb(14, 61, 156);
            gbTrainingSetup.Location = new Point(6, 6);
            gbTrainingSetup.Margin = new Padding(6, 6, 6, 6);
            gbTrainingSetup.Name = "gbTrainingSetup";
            gbTrainingSetup.Padding = new Padding(6, 6, 6, 6);
            gbTrainingSetup.Size = new Size(1572, 277);
            gbTrainingSetup.TabIndex = 0;
            gbTrainingSetup.TabStop = false;
            gbTrainingSetup.Text = "데이터 학습";
            gbTrainingSetup.Enter += gbTrainingSetup_Enter;
            // 
            // txtTrainingLog
            // 
            txtTrainingLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTrainingLog.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtTrainingLog.ForeColor = Color.Black;
            txtTrainingLog.Location = new Point(322, 73);
            txtTrainingLog.Margin = new Padding(6, 6, 6, 6);
            txtTrainingLog.Multiline = true;
            txtTrainingLog.Name = "txtTrainingLog";
            txtTrainingLog.ReadOnly = true;
            txtTrainingLog.ScrollBars = ScrollBars.Vertical;
            txtTrainingLog.Size = new Size(1210, 149);
            txtTrainingLog.TabIndex = 1;
            // 
            // btnTrain
            // 
            btnTrain.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTrain.ForeColor = Color.Black;
            btnTrain.Location = new Point(24, 73);
            btnTrain.Margin = new Padding(6, 6, 6, 6);
            btnTrain.Name = "btnTrain";
            btnTrain.Size = new Size(280, 81);
            btnTrain.TabIndex = 0;
            btnTrain.Text = "학습";
            btnTrain.UseVisualStyleBackColor = true;
            btnTrain.Click += btnTrain_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(255, 114, 16);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(380, 63);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Data Manager";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1600, 1813);
            Controls.Add(lblTitle);
            Controls.Add(tcMain);
            Margin = new Padding(6, 6, 6, 6);
            MinimumSize = new Size(1334, 1242);
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
            gbTrainingSetup.ResumeLayout(false);
            gbTrainingSetup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
