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
        private Label lblTestCurrentIndex;
        private Button btnStartTest;
        private Button btnShowCurrentPrediction;
        private Button btnPredictCurrentFrame;
        private Button btnSelectModelFile;
        private TextBox txtSelectedModelFile;
        private Button btnSelectPredictionCsv;
        private TextBox txtSelectedPredictionCsv;
        private TrackBar tbTestPlaybackSpeed;
        private Label lblTestPlaybackSpeed;
        private Label lblTestBrightness;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTestSteeringValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTestSpeedValue;
        private Button btnTrain;
        private Button btnSelectTrainingSave;
        private TextBox txtTrainingSavePath;
        private TextBox txtTrainingLog;
        private Label lblTrainingEpochCaption;
        private Label lblTrainingEpochValue;
        private Label lblTrainingLossCaption;
        private Label lblTrainingLossValue;
        private Label lblTrainingValLossCaption;
        private Label lblTrainingValLossValue;
        private Label lblTrainingStatusCaption;
        private Label lblTrainingStatusValue;
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
        private Button btnLoadSavedFolder;
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
        private Button btnSaveCatalogState;
        private Panel pnlImageRangeMarker;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtSteeringValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtSpeedValue;
        private ToolTip toolTip;

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
            components = new System.ComponentModel.Container();
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
            btnSaveCatalogState = new Button();
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
            btnLoadSavedFolder = new Button();
            txtFolderPath = new TextBox();
            btnSelectFolder = new Button();
            tpTrainingTest = new TabPage();
            gbModelTest = new GroupBox();
            btnTestReverse = new Button();
            tbTestBrightness = new TrackBar();
            lblTestBrightness = new Label();
            btnTestStop = new Button();
            btnTestPlay = new Button();
            tbTestImageNavigator = new TrackBar();
            lblTestCurrentIndex = new Label();
            btnPredictCurrentFrame = new Button();
            btnShowCurrentPrediction = new Button();
            btnStartTest = new Button();
            btnSelectModelFile = new Button();
            txtSelectedModelFile = new TextBox();
            btnSelectPredictionCsv = new Button();
            txtSelectedPredictionCsv = new TextBox();
            tbTestPlaybackSpeed = new TrackBar();
            lblTestPlaybackSpeed = new Label();
            pbTestPreview = new PictureBox();
            gbTrainingSetup = new GroupBox();
            txtTrainingLog = new TextBox();
            txtTrainingSavePath = new TextBox();
            btnSelectTrainingSave = new Button();
            lblTrainingStatusValue = new Label();
            lblTrainingStatusCaption = new Label();
            lblTrainingValLossValue = new Label();
            lblTrainingValLossCaption = new Label();
            lblTrainingLossValue = new Label();
            lblTrainingLossCaption = new Label();
            lblTrainingEpochValue = new Label();
            lblTrainingEpochCaption = new Label();
            btnTrain = new Button();
            lblTitle = new Label();
            toolTip = new ToolTip(components);
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
            ((System.ComponentModel.ISupportInitialize)tbTestBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbTestImageNavigator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbTestPlaybackSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTestPreview).BeginInit();
            gbTrainingSetup.SuspendLayout();
            SuspendLayout();
            // 
            // tcMain
            // 
            tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tcMain.Controls.Add(tpDataManager);
            tcMain.Controls.Add(tpTrainingTest);
            tcMain.Font = new Font("맑은 고딕", 11F, FontStyle.Bold, GraphicsUnit.Point, 129);
            tcMain.ItemSize = new Size(180, 38);
            tcMain.Location = new Point(0, 48);
            tcMain.Margin = new Padding(7, 5, 7, 5);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new Size(1920, 952);
            tcMain.SizeMode = TabSizeMode.Fixed;
            tcMain.TabIndex = 0;
            // 
            // tpDataManager
            // 
            tpDataManager.BackColor = Color.FromArgb(28, 36, 54);
            tpDataManager.Controls.Add(gbDataContent);
            tpDataManager.Controls.Add(gbDataLoad);
            tpDataManager.Location = new Point(4, 42);
            tpDataManager.Margin = new Padding(7, 5, 7, 5);
            tpDataManager.Name = "tpDataManager";
            tpDataManager.Padding = new Padding(7, 5, 7, 5);
            tpDataManager.Size = new Size(1912, 906);
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
            gbDataContent.Controls.Add(btnSaveCatalogState);
            gbDataContent.Controls.Add(lblPlaybackSpeed);
            gbDataContent.Controls.Add(tbPlaybackSpeed);
            gbDataContent.Controls.Add(btnReverse);
            gbDataContent.Controls.Add(btnStop);
            gbDataContent.Controls.Add(btnPlay);
            gbDataContent.Controls.Add(dgvDataInfo);
            gbDataContent.Controls.Add(lvDataItems);
            gbDataContent.Controls.Add(pbDataPreview);
            gbDataContent.Font = new Font("맑은 고딕", 9.35F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataContent.ForeColor = Color.FromArgb(245, 176, 65);
            gbDataContent.Location = new Point(5, 132);
            gbDataContent.Margin = new Padding(7, 5, 7, 5);
            gbDataContent.Name = "gbDataContent";
            gbDataContent.Padding = new Padding(7, 5, 7, 5);
            gbDataContent.Size = new Size(1784, 704);
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
            btnSetRange.Font = new Font("Microsoft Sans Serif", 17.9999981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSetRange.ForeColor = Color.FromArgb(238, 243, 249);
            btnSetRange.Location = new Point(1332, 361);
            btnSetRange.Margin = new Padding(7, 5, 7, 5);
            btnSetRange.Name = "btnSetRange";
            btnSetRange.Size = new Size(226, 63);
            btnSetRange.TabIndex = 14;
            btnSetRange.Text = "범위 설정";
            toolTip.SetToolTip(btnSetRange, "데이터 범위 설정");
            btnSetRange.UseVisualStyleBackColor = false;
            btnSetRange.Click += btnSetRange_Click;
            // 
            // btnCancelRange
            // 
            btnCancelRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelRange.BackColor = Color.FromArgb(49, 62, 88);
            btnCancelRange.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnCancelRange.FlatStyle = FlatStyle.Flat;
            btnCancelRange.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCancelRange.ForeColor = Color.FromArgb(245, 176, 65);
            btnCancelRange.Location = new Point(1567, 361);
            btnCancelRange.Margin = new Padding(7, 5, 7, 5);
            btnCancelRange.Name = "btnCancelRange";
            btnCancelRange.Size = new Size(198, 63);
            btnCancelRange.TabIndex = 12;
            btnCancelRange.Text = "X";
            toolTip.SetToolTip(btnCancelRange, "범위 설정 취소");
            btnCancelRange.UseVisualStyleBackColor = false;
            btnCancelRange.Click += btnCancelRange_Click;
            // 
            // pnlImageRangeMarker
            // 
            pnlImageRangeMarker.BackColor = Color.FromArgb(245, 176, 65);
            pnlImageRangeMarker.Location = new Point(18, 442);
            pnlImageRangeMarker.Margin = new Padding(7, 5, 7, 5);
            pnlImageRangeMarker.Name = "pnlImageRangeMarker";
            pnlImageRangeMarker.Size = new Size(18, 15);
            pnlImageRangeMarker.TabIndex = 13;
            pnlImageRangeMarker.Visible = false;
            // 
            // tbImageNavigator
            // 
            tbImageNavigator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbImageNavigator.BackColor = Color.FromArgb(39, 50, 72);
            tbImageNavigator.Location = new Point(18, 426);
            tbImageNavigator.Margin = new Padding(7, 5, 7, 5);
            tbImageNavigator.Maximum = 100;
            tbImageNavigator.Name = "tbImageNavigator";
            tbImageNavigator.Size = new Size(1747, 45);
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
            btnCancelDelete.Location = new Point(803, 260);
            btnCancelDelete.Margin = new Padding(7, 5, 7, 5);
            btnCancelDelete.Name = "btnCancelDelete";
            btnCancelDelete.Size = new Size(278, 90);
            btnCancelDelete.TabIndex = 10;
            toolTip.SetToolTip(btnCancelDelete, "삭제 작업 되돌리기");
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
            btnDelete.Location = new Point(1090, 259);
            btnDelete.Margin = new Padding(7, 5, 7, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(221, 90);
            btnDelete.TabIndex = 9;
            toolTip.SetToolTip(btnDelete, "데이터 삭제");
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(49, 62, 88);
            btnFilter.BackgroundImage = (Image)resources.GetObject("btnFilter.BackgroundImage");
            btnFilter.BackgroundImageLayout = ImageLayout.Zoom;
            btnFilter.FlatAppearance.BorderColor = Color.FromArgb(255, 114, 16);
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Microsoft Sans Serif", 12.02F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFilter.ForeColor = Color.FromArgb(45, 212, 191);
            btnFilter.Location = new Point(546, 259);
            btnFilter.Margin = new Padding(7, 5, 7, 5);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(251, 90);
            btnFilter.TabIndex = 8;
            toolTip.SetToolTip(btnFilter, "정지 데이터 필터링");
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click_1;
            // 
            // btnSaveCatalogState
            // 
            btnSaveCatalogState.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveCatalogState.BackColor = Color.FromArgb(49, 62, 88);
            btnSaveCatalogState.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnSaveCatalogState.FlatStyle = FlatStyle.Flat;
            btnSaveCatalogState.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSaveCatalogState.ForeColor = Color.FromArgb(238, 243, 249);
            btnSaveCatalogState.Location = new Point(1336, 323);
            btnSaveCatalogState.Margin = new Padding(7, 5, 7, 5);
            btnSaveCatalogState.Name = "btnSaveCatalogState";
            btnSaveCatalogState.Size = new Size(428, 32);
            btnSaveCatalogState.TabIndex = 15;
            btnSaveCatalogState.Text = "현재 데이터 저장";
            toolTip.SetToolTip(btnSaveCatalogState, "현재 카탈로그 상태를 저장본 폴더에 저장");
            btnSaveCatalogState.UseVisualStyleBackColor = false;
            btnSaveCatalogState.Click += btnSaveCatalogState_Click;
            // 
            // lblPlaybackSpeed
            // 
            lblPlaybackSpeed.AutoSize = true;
            lblPlaybackSpeed.Font = new Font("맑은 고딕", 13.36F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblPlaybackSpeed.ForeColor = Color.FromArgb(45, 212, 191);
            lblPlaybackSpeed.Location = new Point(487, 369);
            lblPlaybackSpeed.Margin = new Padding(7, 0, 7, 0);
            lblPlaybackSpeed.Name = "lblPlaybackSpeed";
            lblPlaybackSpeed.Size = new Size(32, 25);
            lblPlaybackSpeed.TabIndex = 7;
            lblPlaybackSpeed.Text = "x1";
            // 
            // tbPlaybackSpeed
            // 
            tbPlaybackSpeed.BackColor = Color.FromArgb(39, 50, 72);
            tbPlaybackSpeed.LargeChange = 1;
            tbPlaybackSpeed.Location = new Point(18, 369);
            tbPlaybackSpeed.Margin = new Padding(7, 5, 7, 5);
            tbPlaybackSpeed.Maximum = 4;
            tbPlaybackSpeed.Name = "tbPlaybackSpeed";
            tbPlaybackSpeed.Size = new Size(458, 45);
            tbPlaybackSpeed.TabIndex = 6;
            tbPlaybackSpeed.Value = 2;
            tbPlaybackSpeed.Scroll += tbPlaybackSpeed_Scroll;
            // 
            // btnReverse
            // 
            btnReverse.BackColor = Color.FromArgb(49, 62, 88);
            btnReverse.BackgroundImage = Properties.Resources.icon_reverse_theme;
            btnReverse.BackgroundImageLayout = ImageLayout.Zoom;
            btnReverse.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnReverse.FlatStyle = FlatStyle.Flat;
            btnReverse.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnReverse.ForeColor = Color.FromArgb(238, 243, 249);
            btnReverse.Location = new Point(1090, 359);
            btnReverse.Margin = new Padding(7, 5, 7, 5);
            btnReverse.Name = "btnReverse";
            btnReverse.Size = new Size(222, 48);
            btnReverse.TabIndex = 5;
            toolTip.SetToolTip(btnReverse, "데이터 역재생");
            btnReverse.UseVisualStyleBackColor = false;
            btnReverse.Click += btnReverse_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(49, 62, 88);
            btnStop.BackgroundImage = Properties.Resources.icon_stop_theme;
            btnStop.BackgroundImageLayout = ImageLayout.Zoom;
            btnStop.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnStop.ForeColor = Color.FromArgb(238, 243, 249);
            btnStop.Location = new Point(803, 359);
            btnStop.Margin = new Padding(7, 5, 7, 5);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(278, 48);
            btnStop.TabIndex = 4;
            toolTip.SetToolTip(btnStop, "데이터 재생 정지");
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(49, 62, 88);
            btnPlay.BackgroundImage = Properties.Resources.icon_play_theme;
            btnPlay.BackgroundImageLayout = ImageLayout.Zoom;
            btnPlay.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnPlay.ForeColor = Color.FromArgb(238, 243, 249);
            btnPlay.Location = new Point(544, 359);
            btnPlay.Margin = new Padding(7, 5, 7, 5);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(251, 48);
            btnPlay.TabIndex = 3;
            toolTip.SetToolTip(btnPlay, "데이터 재생");
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
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(49, 62, 88);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvDataInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvDataInfo.ColumnHeadersHeight = 42;
            dgvDataInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDataInfo.Columns.AddRange(new DataGridViewColumn[] { colDataName, colDataValue });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(22, 30, 46);
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(49, 62, 88);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvDataInfo.DefaultCellStyle = dataGridViewCellStyle3;
            dgvDataInfo.EnableHeadersVisualStyles = false;
            dgvDataInfo.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dgvDataInfo.GridColor = Color.FromArgb(103, 119, 148);
            dgvDataInfo.Location = new Point(544, 43);
            dgvDataInfo.Margin = new Padding(7, 5, 7, 5);
            dgvDataInfo.MultiSelect = false;
            dgvDataInfo.Name = "dgvDataInfo";
            dgvDataInfo.ReadOnly = true;
            dgvDataInfo.RowHeadersVisible = false;
            dgvDataInfo.RowHeadersWidth = 82;
            dgvDataInfo.RowTemplate.Height = 38;
            dgvDataInfo.ScrollBars = ScrollBars.None;
            dgvDataInfo.Size = new Size(768, 207);
            dgvDataInfo.TabIndex = 2;
            dgvDataInfo.Text = "(폴더경로)";
            dgvDataInfo.CellContentClick += dgvDataInfo_CellContentClick;
            // 
            // colDataName
            // 
            colDataName.HeaderText = "데이터";
            colDataName.MinimumWidth = 180;
            colDataName.Name = "colDataName";
            colDataName.ReadOnly = true;
            colDataName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colDataName.Width = 180;
            // 
            // colDataValue
            // 
            colDataValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDataValue.HeaderText = "값";
            colDataValue.MinimumWidth = 80;
            colDataValue.Name = "colDataValue";
            colDataValue.ReadOnly = true;
            colDataValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // lvDataItems
            // 
            lvDataItems.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lvDataItems.BackColor = Color.FromArgb(22, 30, 46);
            lvDataItems.BorderStyle = BorderStyle.FixedSingle;
            lvDataItems.Font = new Font("맑은 고딕", 11F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lvDataItems.ForeColor = Color.FromArgb(238, 243, 249);
            lvDataItems.FullRowSelect = true;
            lvDataItems.GridLines = true;
            lvDataItems.Location = new Point(1336, 43);
            lvDataItems.Margin = new Padding(7, 5, 7, 5);
            lvDataItems.Name = "lvDataItems";
            lvDataItems.OwnerDraw = true;
            lvDataItems.Size = new Size(428, 272);
            lvDataItems.TabIndex = 1;
            lvDataItems.UseCompatibleStateImageBehavior = false;
            lvDataItems.View = View.Details;
            lvDataItems.DrawColumnHeader += lvDataItems_DrawColumnHeader;
            lvDataItems.DrawItem += lvDataItems_DrawItem;
            lvDataItems.DrawSubItem += lvDataItems_DrawSubItem;
            lvDataItems.SelectedIndexChanged += lvDataItems_SelectedIndexChanged;
            lvDataItems.Resize += lvDataItems_Resize;
            // 
            // pbDataPreview
            // 
            pbDataPreview.BackColor = Color.FromArgb(12, 18, 30);
            pbDataPreview.BorderStyle = BorderStyle.FixedSingle;
            pbDataPreview.Location = new Point(18, 43);
            pbDataPreview.Margin = new Padding(7, 5, 7, 5);
            pbDataPreview.Name = "pbDataPreview";
            pbDataPreview.Size = new Size(514, 316);
            pbDataPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDataPreview.TabIndex = 0;
            pbDataPreview.TabStop = false;
            // 
            // gbDataLoad
            // 
            gbDataLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataLoad.BackColor = Color.FromArgb(39, 50, 72);
            gbDataLoad.Controls.Add(btnCheckDataIntegrity);
            gbDataLoad.Controls.Add(btnLoadSavedFolder);
            gbDataLoad.Controls.Add(txtFolderPath);
            gbDataLoad.Controls.Add(btnSelectFolder);
            gbDataLoad.Font = new Font("맑은 고딕", 9.35F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbDataLoad.ForeColor = Color.FromArgb(245, 176, 65);
            gbDataLoad.Location = new Point(5, 3);
            gbDataLoad.Margin = new Padding(7, 5, 7, 5);
            gbDataLoad.Name = "gbDataLoad";
            gbDataLoad.Padding = new Padding(7, 5, 7, 5);
            gbDataLoad.Size = new Size(1784, 120);
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
            btnCheckDataIntegrity.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCheckDataIntegrity.ForeColor = Color.FromArgb(238, 243, 249);
            btnCheckDataIntegrity.Location = new Point(1472, 41);
            btnCheckDataIntegrity.Margin = new Padding(7, 5, 7, 5);
            btnCheckDataIntegrity.Name = "btnCheckDataIntegrity";
            btnCheckDataIntegrity.Size = new Size(293, 45);
            btnCheckDataIntegrity.TabIndex = 2;
            btnCheckDataIntegrity.Text = "무결성 검사";
            toolTip.SetToolTip(btnCheckDataIntegrity, "데이터 무결성 검사");
            btnCheckDataIntegrity.UseVisualStyleBackColor = false;
            btnCheckDataIntegrity.Click += btnCheckDataIntegrity_Click;
            // 
            // btnLoadSavedFolder
            // 
            btnLoadSavedFolder.BackColor = Color.FromArgb(49, 62, 88);
            btnLoadSavedFolder.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnLoadSavedFolder.FlatStyle = FlatStyle.Flat;
            btnLoadSavedFolder.Font = new Font("맑은 고딕", 11F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLoadSavedFolder.ForeColor = Color.FromArgb(238, 243, 249);
            btnLoadSavedFolder.Location = new Point(246, 41);
            btnLoadSavedFolder.Margin = new Padding(7, 5, 7, 5);
            btnLoadSavedFolder.Name = "btnLoadSavedFolder";
            btnLoadSavedFolder.Size = new Size(214, 45);
            btnLoadSavedFolder.TabIndex = 3;
            btnLoadSavedFolder.Text = "저장본 불러오기";
            toolTip.SetToolTip(btnLoadSavedFolder, "저장본 폴더 아래 저장본 불러오기");
            btnLoadSavedFolder.UseVisualStyleBackColor = false;
            btnLoadSavedFolder.Click += btnLoadSavedFolder_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.BackColor = Color.FromArgb(22, 30, 46);
            txtFolderPath.BorderStyle = BorderStyle.FixedSingle;
            txtFolderPath.Font = new Font("Constantia", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFolderPath.ForeColor = Color.FromArgb(238, 243, 249);
            txtFolderPath.Location = new Point(474, 45);
            txtFolderPath.Margin = new Padding(7, 5, 7, 5);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(983, 40);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.Text = "(폴더경로)";
            txtFolderPath.TextChanged += txtFolderPath_TextChanged;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.BackColor = Color.FromArgb(45, 212, 191);
            btnSelectFolder.BackgroundImage = (Image)resources.GetObject("btnSelectFolder.BackgroundImage");
            btnSelectFolder.BackgroundImageLayout = ImageLayout.Zoom;
            btnSelectFolder.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnSelectFolder.FlatStyle = FlatStyle.Flat;
            btnSelectFolder.Font = new Font("Microsoft Sans Serif", 10.52F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectFolder.ForeColor = Color.FromArgb(6, 42, 43);
            btnSelectFolder.Location = new Point(18, 41);
            btnSelectFolder.Margin = new Padding(7, 5, 7, 5);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(214, 45);
            btnSelectFolder.TabIndex = 0;
            toolTip.SetToolTip(btnSelectFolder, "데이터 폴더 선택");
            btnSelectFolder.UseVisualStyleBackColor = false;
            btnSelectFolder.Click += btnSelectAdd_Click;
            // 
            // tpTrainingTest
            // 
            tpTrainingTest.BackColor = Color.FromArgb(28, 36, 54);
            tpTrainingTest.Controls.Add(gbModelTest);
            tpTrainingTest.Controls.Add(gbTrainingSetup);
            tpTrainingTest.Location = new Point(4, 42);
            tpTrainingTest.Margin = new Padding(7, 5, 7, 5);
            tpTrainingTest.Name = "tpTrainingTest";
            tpTrainingTest.Padding = new Padding(7, 5, 7, 5);
            tpTrainingTest.Size = new Size(1912, 906);
            tpTrainingTest.TabIndex = 1;
            tpTrainingTest.Text = "학습/테스트";
            // 
            // gbModelTest
            // 
            gbModelTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbModelTest.BackColor = Color.FromArgb(39, 50, 72);
            gbModelTest.Controls.Add(btnTestReverse);
            gbModelTest.Controls.Add(tbTestBrightness);
            gbModelTest.Controls.Add(lblTestBrightness);
            gbModelTest.Controls.Add(btnTestStop);
            gbModelTest.Controls.Add(btnTestPlay);
            gbModelTest.Controls.Add(tbTestImageNavigator);
            gbModelTest.Controls.Add(lblTestCurrentIndex);
            gbModelTest.Controls.Add(btnPredictCurrentFrame);
            gbModelTest.Controls.Add(btnShowCurrentPrediction);
            gbModelTest.Controls.Add(btnStartTest);
            gbModelTest.Controls.Add(btnSelectModelFile);
            gbModelTest.Controls.Add(txtSelectedModelFile);
            gbModelTest.Controls.Add(btnSelectPredictionCsv);
            gbModelTest.Controls.Add(txtSelectedPredictionCsv);
            gbModelTest.Controls.Add(tbTestPlaybackSpeed);
            gbModelTest.Controls.Add(lblTestPlaybackSpeed);
            gbModelTest.Controls.Add(pbTestPreview);
            gbModelTest.Font = new Font("맑은 고딕", 9.35F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbModelTest.ForeColor = Color.FromArgb(245, 176, 65);
            gbModelTest.Location = new Point(5, 340);
            gbModelTest.Margin = new Padding(7, 5, 7, 5);
            gbModelTest.Name = "gbModelTest";
            gbModelTest.Padding = new Padding(7, 5, 7, 5);
            gbModelTest.Size = new Size(1870, 556);
            gbModelTest.TabIndex = 1;
            gbModelTest.TabStop = false;
            gbModelTest.Text = "모델 테스트";
            // 
            // btnTestReverse
            // 
            btnTestReverse.BackColor = Color.FromArgb(49, 62, 88);
            btnTestReverse.BackgroundImage = Properties.Resources.icon_reverse_theme;
            btnTestReverse.BackgroundImageLayout = ImageLayout.Zoom;
            btnTestReverse.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnTestReverse.FlatStyle = FlatStyle.Flat;
            btnTestReverse.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnTestReverse.ForeColor = Color.FromArgb(238, 243, 249);
            btnTestReverse.Location = new Point(962, 415);
            btnTestReverse.Margin = new Padding(7, 5, 7, 5);
            btnTestReverse.Name = "btnTestReverse";
            btnTestReverse.Size = new Size(146, 48);
            btnTestReverse.TabIndex = 8;
            toolTip.SetToolTip(btnTestReverse, "테스트 프레임 역재생");
            btnTestReverse.UseVisualStyleBackColor = false;
            // 
            // tbTestBrightness
            // 
            tbTestBrightness.BackColor = Color.FromArgb(39, 50, 72);
            tbTestBrightness.LargeChange = 1;
            tbTestBrightness.Location = new Point(650, 294);
            tbTestBrightness.Margin = new Padding(7, 5, 7, 5);
            tbTestBrightness.Maximum = 4;
            tbTestBrightness.Name = "tbTestBrightness";
            tbTestBrightness.Size = new Size(458, 45);
            tbTestBrightness.TabIndex = 7;
            tbTestBrightness.Value = 2;
            // 
            // lblTestBrightness
            // 
            lblTestBrightness.AutoSize = true;
            lblTestBrightness.Font = new Font("맑은 고딕", 18F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTestBrightness.ForeColor = Color.FromArgb(45, 212, 191);
            lblTestBrightness.Location = new Point(1116, 297);
            lblTestBrightness.Margin = new Padding(7, 0, 7, 0);
            lblTestBrightness.Name = "lblTestBrightness";
            lblTestBrightness.Size = new Size(97, 32);
            lblTestBrightness.TabIndex = 16;
            lblTestBrightness.Text = "밝기 x1";
            // 
            // btnTestStop
            // 
            btnTestStop.BackColor = Color.FromArgb(49, 62, 88);
            btnTestStop.BackgroundImage = Properties.Resources.icon_stop_theme;
            btnTestStop.BackgroundImageLayout = ImageLayout.Zoom;
            btnTestStop.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnTestStop.FlatStyle = FlatStyle.Flat;
            btnTestStop.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnTestStop.ForeColor = Color.FromArgb(238, 243, 249);
            btnTestStop.Location = new Point(804, 415);
            btnTestStop.Margin = new Padding(7, 5, 7, 5);
            btnTestStop.Name = "btnTestStop";
            btnTestStop.Size = new Size(148, 48);
            btnTestStop.TabIndex = 7;
            toolTip.SetToolTip(btnTestStop, "테스트 프레임 재생 정지");
            btnTestStop.UseVisualStyleBackColor = false;
            // 
            // btnTestPlay
            // 
            btnTestPlay.BackColor = Color.FromArgb(49, 62, 88);
            btnTestPlay.BackgroundImage = Properties.Resources.icon_play_theme;
            btnTestPlay.BackgroundImageLayout = ImageLayout.Zoom;
            btnTestPlay.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnTestPlay.FlatStyle = FlatStyle.Flat;
            btnTestPlay.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnTestPlay.ForeColor = Color.FromArgb(238, 243, 249);
            btnTestPlay.Location = new Point(650, 415);
            btnTestPlay.Margin = new Padding(7, 5, 7, 5);
            btnTestPlay.Name = "btnTestPlay";
            btnTestPlay.Size = new Size(145, 48);
            btnTestPlay.TabIndex = 6;
            toolTip.SetToolTip(btnTestPlay, "테스트 프레임 재생");
            btnTestPlay.UseVisualStyleBackColor = false;
            // 
            // tbTestImageNavigator
            // 
            tbTestImageNavigator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbTestImageNavigator.BackColor = Color.FromArgb(39, 50, 72);
            tbTestImageNavigator.Location = new Point(18, 488);
            tbTestImageNavigator.Margin = new Padding(7, 5, 7, 5);
            tbTestImageNavigator.Maximum = 100;
            tbTestImageNavigator.Name = "tbTestImageNavigator";
            tbTestImageNavigator.Size = new Size(1833, 45);
            tbTestImageNavigator.TabIndex = 2;
            tbTestImageNavigator.Scroll += tbTestImageNavigator_Scroll_1;
            // 
            // lblTestCurrentIndex
            // 
            lblTestCurrentIndex.BackColor = Color.FromArgb(22, 30, 46);
            lblTestCurrentIndex.BorderStyle = BorderStyle.FixedSingle;
            lblTestCurrentIndex.Font = new Font("맑은 고딕", 11F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTestCurrentIndex.ForeColor = Color.FromArgb(245, 176, 65);
            lblTestCurrentIndex.Location = new Point(647, 41);
            lblTestCurrentIndex.Margin = new Padding(7, 5, 7, 5);
            lblTestCurrentIndex.Name = "lblTestCurrentIndex";
            lblTestCurrentIndex.Size = new Size(220, 74);
            lblTestCurrentIndex.TabIndex = 3;
            lblTestCurrentIndex.Text = "현재 인덱스\r\n- / -";
            lblTestCurrentIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPredictCurrentFrame
            // 
            btnPredictCurrentFrame.BackColor = Color.FromArgb(16, 185, 129);
            btnPredictCurrentFrame.FlatAppearance.BorderColor = Color.FromArgb(16, 185, 129);
            btnPredictCurrentFrame.FlatStyle = FlatStyle.Flat;
            btnPredictCurrentFrame.Font = new Font("Microsoft Sans Serif", 10.52F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnPredictCurrentFrame.ForeColor = Color.FromArgb(6, 42, 43);
            btnPredictCurrentFrame.Location = new Point(881, 139);
            btnPredictCurrentFrame.Margin = new Padding(7, 5, 7, 5);
            btnPredictCurrentFrame.Name = "btnPredictCurrentFrame";
            btnPredictCurrentFrame.Size = new Size(227, 46);
            btnPredictCurrentFrame.TabIndex = 12;
            btnPredictCurrentFrame.Text = "실시간 예측 적용";
            toolTip.SetToolTip(btnPredictCurrentFrame, "현재 밝기 슬라이더 값으로 전체 테스트 이미지를 예측하고 차트에 반영");
            btnPredictCurrentFrame.UseVisualStyleBackColor = false;
            btnPredictCurrentFrame.Click += btnPredictCurrentFrame_Click;
            // 
            // btnShowCurrentPrediction
            // 
            btnShowCurrentPrediction.BackColor = Color.FromArgb(59, 130, 246);
            btnShowCurrentPrediction.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            btnShowCurrentPrediction.FlatStyle = FlatStyle.Flat;
            btnShowCurrentPrediction.Font = new Font("Microsoft Sans Serif", 10.52F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnShowCurrentPrediction.ForeColor = Color.White;
            btnShowCurrentPrediction.Location = new Point(647, 139);
            btnShowCurrentPrediction.Margin = new Padding(7, 5, 7, 5);
            btnShowCurrentPrediction.Name = "btnShowCurrentPrediction";
            btnShowCurrentPrediction.Size = new Size(220, 46);
            btnShowCurrentPrediction.TabIndex = 4;
            btnShowCurrentPrediction.Text = "현재 예측 보기";
            toolTip.SetToolTip(btnShowCurrentPrediction, "현재 예측 결과 보기");
            btnShowCurrentPrediction.UseVisualStyleBackColor = false;
            btnShowCurrentPrediction.Click += btnShowCurrentPrediction_Click;
            // 
            // btnStartTest
            // 
            btnStartTest.BackColor = Color.FromArgb(45, 212, 191);
            btnStartTest.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.Font = new Font("Microsoft Sans Serif", 10.52F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnStartTest.ForeColor = Color.FromArgb(6, 42, 43);
            btnStartTest.Location = new Point(881, 41);
            btnStartTest.Margin = new Padding(7, 5, 7, 5);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(227, 74);
            btnStartTest.TabIndex = 1;
            btnStartTest.Text = "테스트 시작";
            toolTip.SetToolTip(btnStartTest, "모델 예측 테스트 시작");
            btnStartTest.UseVisualStyleBackColor = false;
            btnStartTest.Click += btnStartTest_Click;
            // 
            // btnSelectModelFile
            // 
            btnSelectModelFile.BackColor = Color.FromArgb(49, 62, 88);
            btnSelectModelFile.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnSelectModelFile.FlatStyle = FlatStyle.Flat;
            btnSelectModelFile.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectModelFile.ForeColor = Color.FromArgb(238, 243, 249);
            btnSelectModelFile.Location = new Point(647, 206);
            btnSelectModelFile.Margin = new Padding(7, 5, 7, 5);
            btnSelectModelFile.Name = "btnSelectModelFile";
            btnSelectModelFile.Size = new Size(132, 32);
            btnSelectModelFile.TabIndex = 10;
            btnSelectModelFile.Text = "모델 선택";
            toolTip.SetToolTip(btnSelectModelFile, "사용할 .h5 모델 파일 선택");
            btnSelectModelFile.UseVisualStyleBackColor = false;
            // 
            // txtSelectedModelFile
            // 
            txtSelectedModelFile.BackColor = Color.FromArgb(12, 18, 30);
            txtSelectedModelFile.BorderStyle = BorderStyle.FixedSingle;
            txtSelectedModelFile.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtSelectedModelFile.ForeColor = Color.FromArgb(238, 243, 249);
            txtSelectedModelFile.Location = new Point(787, 206);
            txtSelectedModelFile.Margin = new Padding(7, 5, 7, 5);
            txtSelectedModelFile.Name = "txtSelectedModelFile";
            txtSelectedModelFile.ReadOnly = true;
            txtSelectedModelFile.Size = new Size(321, 23);
            txtSelectedModelFile.TabIndex = 11;
            txtSelectedModelFile.Text = "최신 파일 자동 선택";
            // 
            // btnSelectPredictionCsv
            // 
            btnSelectPredictionCsv.BackColor = Color.FromArgb(49, 62, 88);
            btnSelectPredictionCsv.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnSelectPredictionCsv.FlatStyle = FlatStyle.Flat;
            btnSelectPredictionCsv.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectPredictionCsv.ForeColor = Color.FromArgb(238, 243, 249);
            btnSelectPredictionCsv.Location = new Point(647, 246);
            btnSelectPredictionCsv.Margin = new Padding(7, 5, 7, 5);
            btnSelectPredictionCsv.Name = "btnSelectPredictionCsv";
            btnSelectPredictionCsv.Size = new Size(132, 32);
            btnSelectPredictionCsv.TabIndex = 12;
            btnSelectPredictionCsv.Text = "CSV 선택";
            toolTip.SetToolTip(btnSelectPredictionCsv, "불러올 .csv 예측 결과 파일 선택");
            btnSelectPredictionCsv.UseVisualStyleBackColor = false;
            // 
            // txtSelectedPredictionCsv
            // 
            txtSelectedPredictionCsv.BackColor = Color.FromArgb(12, 18, 30);
            txtSelectedPredictionCsv.BorderStyle = BorderStyle.FixedSingle;
            txtSelectedPredictionCsv.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtSelectedPredictionCsv.ForeColor = Color.FromArgb(238, 243, 249);
            txtSelectedPredictionCsv.Location = new Point(787, 246);
            txtSelectedPredictionCsv.Margin = new Padding(7, 5, 7, 5);
            txtSelectedPredictionCsv.Name = "txtSelectedPredictionCsv";
            txtSelectedPredictionCsv.ReadOnly = true;
            txtSelectedPredictionCsv.Size = new Size(321, 23);
            txtSelectedPredictionCsv.TabIndex = 13;
            txtSelectedPredictionCsv.Text = "최신 파일 자동 선택";
            // 
            // tbTestPlaybackSpeed
            // 
            tbTestPlaybackSpeed.BackColor = Color.FromArgb(39, 50, 72);
            tbTestPlaybackSpeed.LargeChange = 1;
            tbTestPlaybackSpeed.Location = new Point(659, 349);
            tbTestPlaybackSpeed.Margin = new Padding(7, 5, 7, 5);
            tbTestPlaybackSpeed.Maximum = 400;
            tbTestPlaybackSpeed.Minimum = 25;
            tbTestPlaybackSpeed.Name = "tbTestPlaybackSpeed";
            tbTestPlaybackSpeed.Size = new Size(439, 45);
            tbTestPlaybackSpeed.TabIndex = 14;
            tbTestPlaybackSpeed.Value = 100;
            // 
            // lblTestPlaybackSpeed
            // 
            lblTestPlaybackSpeed.AutoSize = true;
            lblTestPlaybackSpeed.Font = new Font("맑은 고딕", 18F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTestPlaybackSpeed.ForeColor = Color.FromArgb(45, 212, 191);
            lblTestPlaybackSpeed.Location = new Point(1116, 352);
            lblTestPlaybackSpeed.Margin = new Padding(7, 0, 7, 0);
            lblTestPlaybackSpeed.Name = "lblTestPlaybackSpeed";
            lblTestPlaybackSpeed.Size = new Size(97, 32);
            lblTestPlaybackSpeed.TabIndex = 15;
            lblTestPlaybackSpeed.Text = "배속 x1";
            lblTestPlaybackSpeed.Click += lblTestPlaybackSpeed_Click;
            // 
            // pbTestPreview
            // 
            pbTestPreview.BackColor = Color.FromArgb(12, 18, 30);
            pbTestPreview.BorderStyle = BorderStyle.FixedSingle;
            pbTestPreview.Location = new Point(18, 41);
            pbTestPreview.Margin = new Padding(7, 5, 7, 5);
            pbTestPreview.Name = "pbTestPreview";
            pbTestPreview.Size = new Size(606, 434);
            pbTestPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTestPreview.TabIndex = 0;
            pbTestPreview.TabStop = false;
            // 
            // gbTrainingSetup
            // 
            gbTrainingSetup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTrainingSetup.BackColor = Color.FromArgb(39, 50, 72);
            gbTrainingSetup.Controls.Add(txtTrainingLog);
            gbTrainingSetup.Controls.Add(txtTrainingSavePath);
            gbTrainingSetup.Controls.Add(btnSelectTrainingSave);
            gbTrainingSetup.Controls.Add(lblTrainingStatusValue);
            gbTrainingSetup.Controls.Add(lblTrainingStatusCaption);
            gbTrainingSetup.Controls.Add(lblTrainingValLossValue);
            gbTrainingSetup.Controls.Add(lblTrainingValLossCaption);
            gbTrainingSetup.Controls.Add(lblTrainingLossValue);
            gbTrainingSetup.Controls.Add(lblTrainingLossCaption);
            gbTrainingSetup.Controls.Add(lblTrainingEpochValue);
            gbTrainingSetup.Controls.Add(lblTrainingEpochCaption);
            gbTrainingSetup.Controls.Add(btnTrain);
            gbTrainingSetup.Font = new Font("맑은 고딕", 9.35F, FontStyle.Bold, GraphicsUnit.Point, 129);
            gbTrainingSetup.ForeColor = Color.FromArgb(245, 176, 65);
            gbTrainingSetup.Location = new Point(5, 3);
            gbTrainingSetup.Margin = new Padding(7, 5, 7, 5);
            gbTrainingSetup.Name = "gbTrainingSetup";
            gbTrainingSetup.Padding = new Padding(7, 5, 7, 5);
            gbTrainingSetup.Size = new Size(1870, 327);
            gbTrainingSetup.TabIndex = 0;
            gbTrainingSetup.TabStop = false;
            gbTrainingSetup.Text = "데이터 학습";
            gbTrainingSetup.Enter += gbTrainingSetup_Enter;
            // 
            // txtTrainingLog
            // 
            txtTrainingLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTrainingLog.BackColor = Color.FromArgb(12, 18, 30);
            txtTrainingLog.BorderStyle = BorderStyle.FixedSingle;
            txtTrainingLog.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtTrainingLog.ForeColor = Color.FromArgb(238, 243, 249);
            txtTrainingLog.Location = new Point(769, 31);
            txtTrainingLog.Margin = new Padding(7, 5, 7, 5);
            txtTrainingLog.Multiline = true;
            txtTrainingLog.Name = "txtTrainingLog";
            txtTrainingLog.ReadOnly = true;
            txtTrainingLog.ScrollBars = ScrollBars.Vertical;
            txtTrainingLog.Size = new Size(1083, 286);
            txtTrainingLog.TabIndex = 1;
            // 
            // txtTrainingSavePath
            // 
            txtTrainingSavePath.BackColor = Color.FromArgb(12, 18, 30);
            txtTrainingSavePath.BorderStyle = BorderStyle.FixedSingle;
            txtTrainingSavePath.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtTrainingSavePath.ForeColor = Color.FromArgb(238, 243, 249);
            txtTrainingSavePath.Location = new Point(250, 135);
            txtTrainingSavePath.Margin = new Padding(7, 5, 7, 5);
            txtTrainingSavePath.Name = "txtTrainingSavePath";
            txtTrainingSavePath.ReadOnly = true;
            txtTrainingSavePath.Size = new Size(505, 35);
            txtTrainingSavePath.TabIndex = 2;
            txtTrainingSavePath.Text = "(학습용 저장본 경로)";
            // 
            // btnSelectTrainingSave
            // 
            btnSelectTrainingSave.BackColor = Color.FromArgb(49, 62, 88);
            btnSelectTrainingSave.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnSelectTrainingSave.FlatStyle = FlatStyle.Flat;
            btnSelectTrainingSave.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectTrainingSave.ForeColor = Color.FromArgb(238, 243, 249);
            btnSelectTrainingSave.Location = new Point(18, 135);
            btnSelectTrainingSave.Margin = new Padding(7, 5, 7, 5);
            btnSelectTrainingSave.Name = "btnSelectTrainingSave";
            btnSelectTrainingSave.Size = new Size(214, 48);
            btnSelectTrainingSave.TabIndex = 3;
            btnSelectTrainingSave.Text = "저장된 폴더 선택";
            toolTip.SetToolTip(btnSelectTrainingSave, "학습에 사용할 저장본 폴더 선택");
            btnSelectTrainingSave.UseVisualStyleBackColor = false;
            btnSelectTrainingSave.Click += btnSelectTrainingSave_Click;
            // 
            // lblTrainingStatusValue
            // 
            lblTrainingStatusValue.Font = new Font("맑은 고딕", 18F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingStatusValue.ForeColor = Color.FromArgb(45, 212, 191);
            lblTrainingStatusValue.Location = new Point(396, 262);
            lblTrainingStatusValue.Name = "lblTrainingStatusValue";
            lblTrainingStatusValue.Size = new Size(359, 32);
            lblTrainingStatusValue.TabIndex = 11;
            lblTrainingStatusValue.Text = "-";
            // 
            // lblTrainingStatusCaption
            // 
            lblTrainingStatusCaption.Font = new Font("맑은 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingStatusCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingStatusCaption.Location = new Point(396, 223);
            lblTrainingStatusCaption.Name = "lblTrainingStatusCaption";
            lblTrainingStatusCaption.Size = new Size(489, 32);
            lblTrainingStatusCaption.TabIndex = 10;
            lblTrainingStatusCaption.Text = "상태";
            // 
            // lblTrainingValLossValue
            // 
            lblTrainingValLossValue.Font = new Font("맑은 고딕", 22F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingValLossValue.ForeColor = Color.FromArgb(238, 243, 249);
            lblTrainingValLossValue.Location = new Point(258, 255);
            lblTrainingValLossValue.Name = "lblTrainingValLossValue";
            lblTrainingValLossValue.Size = new Size(73, 48);
            lblTrainingValLossValue.TabIndex = 9;
            lblTrainingValLossValue.Text = "-";
            // 
            // lblTrainingValLossCaption
            // 
            lblTrainingValLossCaption.Font = new Font("맑은 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingValLossCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingValLossCaption.Location = new Point(250, 223);
            lblTrainingValLossCaption.Name = "lblTrainingValLossCaption";
            lblTrainingValLossCaption.Size = new Size(117, 32);
            lblTrainingValLossCaption.TabIndex = 8;
            lblTrainingValLossCaption.Text = "검증 손실";
            // 
            // lblTrainingLossValue
            // 
            lblTrainingLossValue.Font = new Font("맑은 고딕", 22F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingLossValue.ForeColor = Color.FromArgb(238, 243, 249);
            lblTrainingLossValue.Location = new Point(151, 255);
            lblTrainingLossValue.Name = "lblTrainingLossValue";
            lblTrainingLossValue.Size = new Size(81, 48);
            lblTrainingLossValue.TabIndex = 7;
            lblTrainingLossValue.Text = "-";
            // 
            // lblTrainingLossCaption
            // 
            lblTrainingLossCaption.Font = new Font("맑은 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingLossCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingLossCaption.Location = new Point(151, 223);
            lblTrainingLossCaption.Name = "lblTrainingLossCaption";
            lblTrainingLossCaption.Size = new Size(81, 43);
            lblTrainingLossCaption.TabIndex = 6;
            lblTrainingLossCaption.Text = "손실";
            // 
            // lblTrainingEpochValue
            // 
            lblTrainingEpochValue.Font = new Font("맑은 고딕", 22F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingEpochValue.ForeColor = Color.FromArgb(238, 243, 249);
            lblTrainingEpochValue.Location = new Point(33, 255);
            lblTrainingEpochValue.Name = "lblTrainingEpochValue";
            lblTrainingEpochValue.Size = new Size(89, 48);
            lblTrainingEpochValue.TabIndex = 3;
            lblTrainingEpochValue.Text = "- / -";
            // 
            // lblTrainingEpochCaption
            // 
            lblTrainingEpochCaption.Font = new Font("맑은 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingEpochCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingEpochCaption.Location = new Point(33, 223);
            lblTrainingEpochCaption.Name = "lblTrainingEpochCaption";
            lblTrainingEpochCaption.Size = new Size(70, 43);
            lblTrainingEpochCaption.TabIndex = 2;
            lblTrainingEpochCaption.Text = "회차";
            // 
            // btnTrain
            // 
            btnTrain.BackColor = Color.FromArgb(45, 212, 191);
            btnTrain.BackgroundImage = (Image)resources.GetObject("btnTrain.BackgroundImage");
            btnTrain.BackgroundImageLayout = ImageLayout.Zoom;
            btnTrain.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnTrain.FlatStyle = FlatStyle.Flat;
            btnTrain.Font = new Font("Microsoft Sans Serif", 10.52F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTrain.ForeColor = Color.FromArgb(6, 42, 43);
            btnTrain.Location = new Point(18, 41);
            btnTrain.Margin = new Padding(7, 5, 7, 5);
            btnTrain.Name = "btnTrain";
            btnTrain.Size = new Size(214, 84);
            btnTrain.TabIndex = 0;
            toolTip.SetToolTip(btnTrain, "모델 학습 시작 또는 중지");
            btnTrain.UseVisualStyleBackColor = false;
            btnTrain.Click += btnTrain_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Mongolian Baiti", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(245, 176, 65);
            lblTitle.Location = new Point(6, 8);
            lblTitle.Margin = new Padding(7, 0, 7, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(162, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Data Manager";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(18, 24, 38);
            ClientSize = new Size(1920, 1000);
            Controls.Add(lblTitle);
            Controls.Add(tcMain);
            Font = new Font("Microsoft Sans Serif", 6.01F, FontStyle.Bold, GraphicsUnit.Point, 129);
            Margin = new Padding(7, 5, 7, 5);
            MinimumSize = new Size(1280, 720);
            Name = "Form1";
            Text = "Data Manager";
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
            ((System.ComponentModel.ISupportInitialize)tbTestBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbTestImageNavigator).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbTestPlaybackSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTestPreview).EndInit();
            gbTrainingSetup.ResumeLayout(false);
            gbTrainingSetup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar tbTestBrightness;
        private Button btnTestReverse;
        private Button btnTestStop;
        private Button btnTestPlay;
    }
}
