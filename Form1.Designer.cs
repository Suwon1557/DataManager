namespace DataManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ThemedTabControl tcMain;
        private TabPage tpDataManager;
        private TabPage tpTrainingTest;
        private GroupBox gbTrainingSetup;
        private GroupBox gbModelTest;
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tcMain = new ThemedTabControl();
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
            tbTestImageNavigator = new TrackBar();
            btnStartTest = new Button();
            pbTestPreview = new PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)tbTestImageNavigator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTestPreview).BeginInit();
            gbTrainingSetup.SuspendLayout();
            SuspendLayout();
            // 
            // tcMain
            // 
            tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tcMain.Controls.Add(tpDataManager);
            tcMain.Controls.Add(tpTrainingTest);
            tcMain.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            tcMain.ItemSize = new Size(160, 30);
            tcMain.Location = new Point(0, 85);
            tcMain.Margin = new Padding(6, 6, 6, 6);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new Size(2636, 1596);
            tcMain.SizeMode = TabSizeMode.Fixed;
            tcMain.TabIndex = 0;
            // 
            // tpDataManager
            // 
            tpDataManager.BackColor = Color.FromArgb(28, 36, 54);
            tpDataManager.Controls.Add(gbDataContent);
            tpDataManager.Controls.Add(gbDataLoad);
            tpDataManager.Location = new Point(8, 38);
            tpDataManager.Margin = new Padding(6, 6, 6, 6);
            tpDataManager.Name = "tpDataManager";
            tpDataManager.Padding = new Padding(6, 6, 6, 6);
            tpDataManager.Size = new Size(2620, 1550);
            tpDataManager.TabIndex = 0;
            tpDataManager.Text = "데이터 관리";
            tpDataManager.Click += tpDataManager_Click;
            // 
            // gbDataContent
            // 
            gbDataContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbDataContent.BackColor = Color.FromArgb(39, 50, 72);
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
            gbDataContent.Font = new Font("맑은 고딕", 14F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataContent.ForeColor = Color.FromArgb(245, 176, 65);
            gbDataContent.Location = new Point(6, 233);
            gbDataContent.Margin = new Padding(6, 6, 6, 6);
            gbDataContent.Name = "gbDataContent";
            gbDataContent.Padding = new Padding(6, 6, 6, 6);
            gbDataContent.Size = new Size(2608, 1276);
            gbDataContent.TabIndex = 1;
            gbDataContent.TabStop = false;
            gbDataContent.Text = "데이터 탐색";
            gbDataContent.Enter += gbDataContent_Enter;
            gbDataContent.Resize += gbDataContent_Resize;
            // 
            // btnSetRange
            // 
            btnSetRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSetRange.BackColor = Color.FromArgb(49, 62, 88);
            btnSetRange.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnSetRange.FlatStyle = FlatStyle.Flat;
            btnSetRange.ForeColor = Color.FromArgb(238, 243, 249);
            btnSetRange.Location = new Point(2016, 640);
            btnSetRange.Margin = new Padding(6, 6, 6, 6);
            btnSetRange.Name = "btnSetRange";
            btnSetRange.Size = new Size(296, 111);
            btnSetRange.TabIndex = 14;
            btnSetRange.Text = "범위 설정";
            btnSetRange.UseVisualStyleBackColor = false;
            btnSetRange.Click += btnSetRange_Click;
            // 
            // btnCancelRange
            // 
            btnCancelRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelRange.BackColor = Color.FromArgb(49, 62, 88);
            btnCancelRange.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnCancelRange.FlatStyle = FlatStyle.Flat;
            btnCancelRange.ForeColor = Color.FromArgb(245, 176, 65);
            btnCancelRange.Location = new Point(2324, 640);
            btnCancelRange.Margin = new Padding(6, 6, 6, 6);
            btnCancelRange.Name = "btnCancelRange";
            btnCancelRange.Size = new Size(260, 111);
            btnCancelRange.TabIndex = 12;
            btnCancelRange.Text = "X";
            btnCancelRange.UseVisualStyleBackColor = false;
            btnCancelRange.Click += btnCancelRange_Click;
            // 
            // pnlImageRangeMarker
            // 
            pnlImageRangeMarker.BackColor = Color.FromArgb(245, 176, 65);
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
            tbImageNavigator.BackColor = Color.FromArgb(39, 50, 72);
            tbImageNavigator.Location = new Point(24, 755);
            tbImageNavigator.Margin = new Padding(6, 6, 6, 6);
            tbImageNavigator.Maximum = 100;
            tbImageNavigator.Name = "tbImageNavigator";
            tbImageNavigator.Size = new Size(2560, 90);
            tbImageNavigator.TabIndex = 11;
            tbImageNavigator.MouseUp += tbImageNavigator_MouseUp;
            // 
            // btnCancelDelete
            // 
            btnCancelDelete.BackColor = Color.FromArgb(22, 30, 46);
            btnCancelDelete.BackgroundImage = (Image)resources.GetObject("btnCancelDelete.BackgroundImage");
            btnCancelDelete.BackgroundImageLayout = ImageLayout.Zoom;
            btnCancelDelete.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnCancelDelete.FlatStyle = FlatStyle.Flat;
            btnCancelDelete.ForeColor = Color.FromArgb(45, 212, 191);
            btnCancelDelete.Location = new Point(1052, 399);
            btnCancelDelete.Margin = new Padding(6, 6, 6, 6);
            btnCancelDelete.Name = "btnCancelDelete";
            btnCancelDelete.Size = new Size(364, 73);
            btnCancelDelete.TabIndex = 10;
            btnCancelDelete.UseVisualStyleBackColor = false;
            btnCancelDelete.Click += btnCancelDelete_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(49, 62, 88);
            btnDelete.BackgroundImage = (Image)resources.GetObject("btnDelete.BackgroundImage");
            btnDelete.BackgroundImageLayout = ImageLayout.Zoom;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.FromArgb(248, 113, 113);
            btnDelete.Location = new Point(1428, 399);
            btnDelete.Margin = new Padding(6, 6, 6, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(290, 73);
            btnDelete.TabIndex = 9;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(45, 212, 191);
            btnFilter.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.ForeColor = Color.FromArgb(6, 42, 43);
            btnFilter.Location = new Point(712, 399);
            btnFilter.Margin = new Padding(6, 6, 6, 6);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(328, 73);
            btnFilter.TabIndex = 8;
            btnFilter.Text = "필터링";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click_1;
            // 
            // lblPlaybackSpeed
            // 
            lblPlaybackSpeed.AutoSize = true;
            lblPlaybackSpeed.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblPlaybackSpeed.ForeColor = Color.FromArgb(45, 212, 191);
            lblPlaybackSpeed.Location = new Point(638, 655);
            lblPlaybackSpeed.Margin = new Padding(6, 0, 6, 0);
            lblPlaybackSpeed.Name = "lblPlaybackSpeed";
            lblPlaybackSpeed.Size = new Size(90, 72);
            lblPlaybackSpeed.TabIndex = 7;
            lblPlaybackSpeed.Text = "x1";
            // 
            // tbPlaybackSpeed
            // 
            tbPlaybackSpeed.BackColor = Color.FromArgb(39, 50, 72);
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
            btnReverse.BackColor = Color.FromArgb(49, 62, 88);
            btnReverse.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnReverse.FlatStyle = FlatStyle.Flat;
            btnReverse.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnReverse.ForeColor = Color.FromArgb(238, 243, 249);
            btnReverse.Location = new Point(1196, 655);
            btnReverse.Margin = new Padding(6, 6, 6, 6);
            btnReverse.Name = "btnReverse";
            btnReverse.Size = new Size(192, 85);
            btnReverse.TabIndex = 5;
            btnReverse.Text = "<<";
            btnReverse.UseVisualStyleBackColor = false;
            btnReverse.Click += btnReverse_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(49, 62, 88);
            btnStop.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.ForeColor = Color.FromArgb(238, 243, 249);
            btnStop.Location = new Point(990, 657);
            btnStop.Margin = new Padding(6, 6, 6, 6);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(194, 85);
            btnStop.TabIndex = 4;
            btnStop.Text = "||";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(49, 62, 88);
            btnPlay.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.ForeColor = Color.FromArgb(238, 243, 249);
            btnPlay.Location = new Point(788, 657);
            btnPlay.Margin = new Padding(6, 6, 6, 6);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(190, 85);
            btnPlay.TabIndex = 3;
            btnPlay.Text = ">>";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click_1;
            // 
            // dgvDataInfo
            // 
            dgvDataInfo.AllowUserToAddRows = false;
            dgvDataInfo.AllowUserToDeleteRows = false;
            dgvDataInfo.AllowUserToResizeColumns = false;
            dgvDataInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(28, 36, 54);
            dgvDataInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvDataInfo.BackgroundColor = Color.FromArgb(22, 30, 46);
            dgvDataInfo.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(49, 62, 88);
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(49, 62, 88);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvDataInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvDataInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDataInfo.Columns.AddRange(new DataGridViewColumn[] { colDataName, colDataValue });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(22, 30, 46);
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(49, 62, 88);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvDataInfo.DefaultCellStyle = dataGridViewCellStyle3;
            dgvDataInfo.EnableHeadersVisualStyles = false;
            dgvDataInfo.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dgvDataInfo.GridColor = Color.FromArgb(103, 119, 148);
            dgvDataInfo.Location = new Point(712, 77);
            dgvDataInfo.Margin = new Padding(6, 6, 6, 6);
            dgvDataInfo.MultiSelect = false;
            dgvDataInfo.Name = "dgvDataInfo";
            dgvDataInfo.ReadOnly = true;
            dgvDataInfo.RowHeadersVisible = false;
            dgvDataInfo.RowHeadersWidth = 82;
            dgvDataInfo.ScrollBars = ScrollBars.None;
            dgvDataInfo.Size = new Size(1006, 294);
            dgvDataInfo.TabIndex = 2;
            dgvDataInfo.Text = "(폴더경로)";
            dgvDataInfo.CellContentClick += dgvDataInfo_CellContentClick;
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
            lvDataItems.BackColor = Color.FromArgb(22, 30, 46);
            lvDataItems.BorderStyle = BorderStyle.FixedSingle;
            lvDataItems.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lvDataItems.ForeColor = Color.FromArgb(238, 243, 249);
            lvDataItems.FullRowSelect = true;
            lvDataItems.GridLines = true;
            lvDataItems.Location = new Point(1750, 77);
            lvDataItems.Margin = new Padding(6, 6, 6, 6);
            lvDataItems.Name = "lvDataItems";
            lvDataItems.Size = new Size(832, 482);
            lvDataItems.TabIndex = 1;
            lvDataItems.UseCompatibleStateImageBehavior = false;
            lvDataItems.View = View.Details;
            lvDataItems.SelectedIndexChanged += lvDataItems_SelectedIndexChanged;
            // 
            // pbDataPreview
            // 
            pbDataPreview.BackColor = Color.FromArgb(12, 18, 30);
            pbDataPreview.BorderStyle = BorderStyle.FixedSingle;
            pbDataPreview.Location = new Point(24, 77);
            pbDataPreview.Margin = new Padding(6, 6, 6, 6);
            pbDataPreview.Name = "pbDataPreview";
            pbDataPreview.Size = new Size(674, 482);
            pbDataPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDataPreview.TabIndex = 0;
            pbDataPreview.TabStop = false;
            // 
            // gbDataLoad
            // 
            gbDataLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataLoad.BackColor = Color.FromArgb(39, 50, 72);
            gbDataLoad.Controls.Add(btnCheckDataIntegrity);
            gbDataLoad.Controls.Add(txtFolderPath);
            gbDataLoad.Controls.Add(btnSelectFolder);
            gbDataLoad.Font = new Font("맑은 고딕", 14F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataLoad.ForeColor = Color.FromArgb(245, 176, 65);
            gbDataLoad.Location = new Point(6, 6);
            gbDataLoad.Margin = new Padding(6, 6, 6, 6);
            gbDataLoad.Name = "gbDataLoad";
            gbDataLoad.Padding = new Padding(6, 6, 6, 6);
            gbDataLoad.Size = new Size(2608, 213);
            gbDataLoad.TabIndex = 0;
            gbDataLoad.TabStop = false;
            gbDataLoad.Text = "데이터 불러오기";
            // 
            // btnCheckDataIntegrity
            // 
            btnCheckDataIntegrity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCheckDataIntegrity.BackColor = Color.FromArgb(49, 62, 88);
            btnCheckDataIntegrity.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnCheckDataIntegrity.FlatStyle = FlatStyle.Flat;
            btnCheckDataIntegrity.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCheckDataIntegrity.ForeColor = Color.FromArgb(238, 243, 249);
            btnCheckDataIntegrity.Location = new Point(2200, 73);
            btnCheckDataIntegrity.Margin = new Padding(6, 6, 6, 6);
            btnCheckDataIntegrity.Name = "btnCheckDataIntegrity";
            btnCheckDataIntegrity.Size = new Size(384, 81);
            btnCheckDataIntegrity.TabIndex = 2;
            btnCheckDataIntegrity.Text = "무결성 검사";
            btnCheckDataIntegrity.UseVisualStyleBackColor = false;
            btnCheckDataIntegrity.Click += btnCheckDataIntegrity_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.BackColor = Color.FromArgb(22, 30, 46);
            txtFolderPath.BorderStyle = BorderStyle.FixedSingle;
            txtFolderPath.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtFolderPath.ForeColor = Color.FromArgb(238, 243, 249);
            txtFolderPath.Location = new Point(322, 79);
            txtFolderPath.Margin = new Padding(6, 6, 6, 6);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(1858, 43);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.Text = "(폴더경로)";
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.BackColor = Color.FromArgb(45, 212, 191);
            btnSelectFolder.BackgroundImage = (Image)resources.GetObject("btnSelectFolder.BackgroundImage");
            btnSelectFolder.BackgroundImageLayout = ImageLayout.Zoom;
            btnSelectFolder.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnSelectFolder.FlatStyle = FlatStyle.Flat;
            btnSelectFolder.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectFolder.ForeColor = Color.FromArgb(6, 42, 43);
            btnSelectFolder.Location = new Point(24, 73);
            btnSelectFolder.Margin = new Padding(6, 6, 6, 6);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(280, 81);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.UseVisualStyleBackColor = false;
            btnSelectFolder.Click += btnSelectAdd_Click;
            // 
            // tpTrainingTest
            // 
            tpTrainingTest.BackColor = Color.FromArgb(28, 36, 54);
            tpTrainingTest.Controls.Add(gbModelTest);
            tpTrainingTest.Controls.Add(gbTrainingSetup);
            tpTrainingTest.Location = new Point(8, 38);
            tpTrainingTest.Margin = new Padding(6, 6, 6, 6);
            tpTrainingTest.Name = "tpTrainingTest";
            tpTrainingTest.Padding = new Padding(6, 6, 6, 6);
            tpTrainingTest.Size = new Size(2620, 1550);
            tpTrainingTest.TabIndex = 1;
            tpTrainingTest.Text = "학습/테스트";
            // 
            // gbModelTest
            // 
            gbModelTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbModelTest.BackColor = Color.FromArgb(39, 50, 72);
            gbModelTest.Controls.Add(tbTestImageNavigator);
            gbModelTest.Controls.Add(btnStartTest);
            gbModelTest.Controls.Add(pbTestPreview);
            gbModelTest.Font = new Font("맑은 고딕", 14F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbModelTest.ForeColor = Color.FromArgb(245, 176, 65);
            gbModelTest.Location = new Point(6, 297);
            gbModelTest.Margin = new Padding(6, 6, 6, 6);
            gbModelTest.Name = "gbModelTest";
            gbModelTest.Padding = new Padding(6, 6, 6, 6);
            gbModelTest.Size = new Size(2588, 1212);
            gbModelTest.TabIndex = 1;
            gbModelTest.TabStop = false;
            gbModelTest.Text = "모델 테스트";
            // 
            // tbTestImageNavigator
            // 
            tbTestImageNavigator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbTestImageNavigator.BackColor = Color.FromArgb(39, 50, 72);
            tbTestImageNavigator.Location = new Point(24, 990);
            tbTestImageNavigator.Margin = new Padding(6, 6, 6, 6);
            tbTestImageNavigator.Maximum = 100;
            tbTestImageNavigator.Name = "tbTestImageNavigator";
            tbTestImageNavigator.Size = new Size(2540, 90);
            tbTestImageNavigator.TabIndex = 2;
            tbTestImageNavigator.Scroll += tbTestImageNavigator_Scroll_1;
            // 
            // btnStartTest
            // 
            btnStartTest.BackColor = Color.FromArgb(245, 176, 65);
            btnStartTest.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnStartTest.ForeColor = Color.FromArgb(48, 34, 8);
            btnStartTest.Location = new Point(24, 870);
            btnStartTest.Margin = new Padding(6, 6, 6, 6);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(794, 94);
            btnStartTest.TabIndex = 1;
            btnStartTest.Text = "테스트 시작";
            btnStartTest.UseVisualStyleBackColor = false;
            btnStartTest.Click += btnStartTest_Click;
            // 
            // pbTestPreview
            // 
            pbTestPreview.BackColor = Color.FromArgb(12, 18, 30);
            pbTestPreview.BorderStyle = BorderStyle.FixedSingle;
            pbTestPreview.Location = new Point(24, 73);
            pbTestPreview.Margin = new Padding(6, 6, 6, 6);
            pbTestPreview.Name = "pbTestPreview";
            pbTestPreview.Size = new Size(794, 770);
            pbTestPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pbTestPreview.TabIndex = 0;
            pbTestPreview.TabStop = false;
            // 
            // gbTrainingSetup
            // 
            gbTrainingSetup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTrainingSetup.BackColor = Color.FromArgb(39, 50, 72);
            gbTrainingSetup.Controls.Add(txtTrainingLog);
            gbTrainingSetup.Controls.Add(btnTrain);
            gbTrainingSetup.Font = new Font("맑은 고딕", 14F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbTrainingSetup.ForeColor = Color.FromArgb(245, 176, 65);
            gbTrainingSetup.Location = new Point(6, 6);
            gbTrainingSetup.Margin = new Padding(6, 6, 6, 6);
            gbTrainingSetup.Name = "gbTrainingSetup";
            gbTrainingSetup.Padding = new Padding(6, 6, 6, 6);
            gbTrainingSetup.Size = new Size(2588, 277);
            gbTrainingSetup.TabIndex = 0;
            gbTrainingSetup.TabStop = false;
            gbTrainingSetup.Text = "데이터 학습";
            gbTrainingSetup.Enter += gbTrainingSetup_Enter;
            // 
            // txtTrainingLog
            // 
            txtTrainingLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTrainingLog.BackColor = Color.FromArgb(12, 18, 30);
            txtTrainingLog.BorderStyle = BorderStyle.FixedSingle;
            txtTrainingLog.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtTrainingLog.ForeColor = Color.FromArgb(238, 243, 249);
            txtTrainingLog.Location = new Point(322, 73);
            txtTrainingLog.Margin = new Padding(6, 6, 6, 6);
            txtTrainingLog.Multiline = true;
            txtTrainingLog.Name = "txtTrainingLog";
            txtTrainingLog.ReadOnly = true;
            txtTrainingLog.ScrollBars = ScrollBars.Vertical;
            txtTrainingLog.Size = new Size(2228, 151);
            txtTrainingLog.TabIndex = 1;
            // 
            // btnTrain
            // 
            btnTrain.BackColor = Color.FromArgb(245, 176, 65);
            btnTrain.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnTrain.FlatStyle = FlatStyle.Flat;
            btnTrain.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTrain.ForeColor = Color.FromArgb(48, 34, 8);
            btnTrain.Location = new Point(24, 73);
            btnTrain.Margin = new Padding(6, 6, 6, 6);
            btnTrain.Name = "btnTrain";
            btnTrain.Size = new Size(280, 149);
            btnTrain.TabIndex = 0;
            btnTrain.Text = "학습";
            btnTrain.UseVisualStyleBackColor = false;
            btnTrain.Click += btnTrain_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTitle.ForeColor = Color.FromArgb(245, 176, 65);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(396, 72);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Data Manager";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 24, 38);
            ClientSize = new Size(2636, 1681);
            Controls.Add(lblTitle);
            Controls.Add(tcMain);
            Margin = new Padding(6, 6, 6, 6);
            MinimumSize = new Size(1324, 1206);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
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
            gbModelTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbTestImageNavigator).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTestPreview).EndInit();
            gbTrainingSetup.ResumeLayout(false);
            gbTrainingSetup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

    }
}
