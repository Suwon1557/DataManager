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
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTestSteeringValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTestSpeedValue;
        private Button btnTrain;
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
        private Label lblTitle;
        private Button btnReverse;
        private Button btnStop;
        private Button btnPlay;
        private TrackBar tbPlaybackSpeed;
        private Label lblPlaybackSpeed;
        private Label lblPlaybackSpeed_tab2;
        private TrackBar tbImageNavigator;
        private Button btnSetRange;
        private Button btnCancelRange;
        private Button btnCancelDelete;
        private Button btnDelete;
        private Button btnFilter;
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
            btnReverse_tab2 = new Button();
            trackBar_tab2 = new TrackBar();
            lblPlaybackSpeed_tab2 = new Label();
            btnStop_tab2 = new Button();
            btnPlay_tab2 = new Button();
            tbTestImageNavigator = new TrackBar();
            lblTestCurrentIndex = new Label();
            btnShowCurrentPrediction = new Button();
            btnStartTest = new Button();
            pbTestPreview = new PictureBox();
            gbTrainingSetup = new GroupBox();
            txtTrainingLog = new TextBox();
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
            ((System.ComponentModel.ISupportInitialize)trackBar_tab2).BeginInit();
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
            btnSetRange.Font = new Font("한컴 고딕", 17.9999981F, FontStyle.Bold, GraphicsUnit.Point, 129);
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
            btnCheckDataIntegrity.Font = new Font("한컴 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
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
            // txtFolderPath
            // 
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.BackColor = Color.FromArgb(22, 30, 46);
            txtFolderPath.BorderStyle = BorderStyle.FixedSingle;
            txtFolderPath.Font = new Font("Constantia", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFolderPath.ForeColor = Color.FromArgb(238, 243, 249);
            txtFolderPath.Location = new Point(246, 45);
            txtFolderPath.Margin = new Padding(7, 5, 7, 5);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(1211, 40);
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
            gbModelTest.Controls.Add(btnReverse_tab2);
            gbModelTest.Controls.Add(trackBar_tab2);
            gbModelTest.Controls.Add(lblPlaybackSpeed_tab2);
            gbModelTest.Controls.Add(btnStop_tab2);
            gbModelTest.Controls.Add(btnPlay_tab2);
            gbModelTest.Controls.Add(tbTestImageNavigator);
            gbModelTest.Controls.Add(lblTestCurrentIndex);
            gbModelTest.Controls.Add(btnShowCurrentPrediction);
            gbModelTest.Controls.Add(btnStartTest);
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
            // btnReverse_tab2
            // 
            btnReverse_tab2.BackColor = Color.FromArgb(49, 62, 88);
            btnReverse_tab2.BackgroundImage = Properties.Resources.icon_reverse_theme;
            btnReverse_tab2.BackgroundImageLayout = ImageLayout.Zoom;
            btnReverse_tab2.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnReverse_tab2.FlatStyle = FlatStyle.Flat;
            btnReverse_tab2.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnReverse_tab2.ForeColor = Color.FromArgb(238, 243, 249);
            btnReverse_tab2.Location = new Point(962, 144);
            btnReverse_tab2.Margin = new Padding(7, 5, 7, 5);
            btnReverse_tab2.Name = "btnReverse_tab2";
            btnReverse_tab2.Size = new Size(146, 48);
            btnReverse_tab2.TabIndex = 8;
            toolTip.SetToolTip(btnReverse_tab2, "테스트 프레임 역재생");
            btnReverse_tab2.UseVisualStyleBackColor = false;
            // 
            // trackBar_tab2
            // 
            trackBar_tab2.BackColor = Color.FromArgb(39, 50, 72);
            trackBar_tab2.LargeChange = 1;
            trackBar_tab2.Location = new Point(647, 224);
            trackBar_tab2.Margin = new Padding(7, 5, 7, 5);
            trackBar_tab2.Maximum = 4;
            trackBar_tab2.Name = "trackBar_tab2";
            trackBar_tab2.Size = new Size(458, 45);
            trackBar_tab2.TabIndex = 7;
            trackBar_tab2.Value = 2;
            // 
            // lblPlaybackSpeed_tab2
            // 
            lblPlaybackSpeed_tab2.AutoSize = true;
            lblPlaybackSpeed_tab2.Font = new Font("맑은 고딕", 13.36F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblPlaybackSpeed_tab2.ForeColor = Color.FromArgb(45, 212, 191);
            lblPlaybackSpeed_tab2.Location = new Point(1112, 213);
            lblPlaybackSpeed_tab2.Margin = new Padding(7, 0, 7, 0);
            lblPlaybackSpeed_tab2.Name = "lblPlaybackSpeed_tab2";
            lblPlaybackSpeed_tab2.Size = new Size(32, 25);
            lblPlaybackSpeed_tab2.TabIndex = 9;
            lblPlaybackSpeed_tab2.Text = "x1";
            // 
            // btnStop_tab2
            // 
            btnStop_tab2.BackColor = Color.FromArgb(49, 62, 88);
            btnStop_tab2.BackgroundImage = Properties.Resources.icon_stop_theme;
            btnStop_tab2.BackgroundImageLayout = ImageLayout.Zoom;
            btnStop_tab2.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnStop_tab2.FlatStyle = FlatStyle.Flat;
            btnStop_tab2.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnStop_tab2.ForeColor = Color.FromArgb(238, 243, 249);
            btnStop_tab2.Location = new Point(805, 145);
            btnStop_tab2.Margin = new Padding(7, 5, 7, 5);
            btnStop_tab2.Name = "btnStop_tab2";
            btnStop_tab2.Size = new Size(148, 48);
            btnStop_tab2.TabIndex = 7;
            toolTip.SetToolTip(btnStop_tab2, "테스트 프레임 재생 정지");
            btnStop_tab2.UseVisualStyleBackColor = false;
            // 
            // btnPlay_tab2
            // 
            btnPlay_tab2.BackColor = Color.FromArgb(49, 62, 88);
            btnPlay_tab2.BackgroundImage = Properties.Resources.icon_play_theme;
            btnPlay_tab2.BackgroundImageLayout = ImageLayout.Zoom;
            btnPlay_tab2.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnPlay_tab2.FlatStyle = FlatStyle.Flat;
            btnPlay_tab2.Font = new Font("굴림", 14.78F, FontStyle.Bold);
            btnPlay_tab2.ForeColor = Color.FromArgb(238, 243, 249);
            btnPlay_tab2.Location = new Point(651, 145);
            btnPlay_tab2.Margin = new Padding(7, 5, 7, 5);
            btnPlay_tab2.Name = "btnPlay_tab2";
            btnPlay_tab2.Size = new Size(145, 48);
            btnPlay_tab2.TabIndex = 6;
            toolTip.SetToolTip(btnPlay_tab2, "테스트 프레임 재생");
            btnPlay_tab2.UseVisualStyleBackColor = false;
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
            // btnShowCurrentPrediction
            // 
            btnShowCurrentPrediction.BackColor = Color.FromArgb(59, 130, 246);
            btnShowCurrentPrediction.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            btnShowCurrentPrediction.FlatStyle = FlatStyle.Flat;
            btnShowCurrentPrediction.Font = new Font("Microsoft Sans Serif", 10.52F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnShowCurrentPrediction.ForeColor = Color.White;
            btnShowCurrentPrediction.Location = new Point(647, 372);
            btnShowCurrentPrediction.Margin = new Padding(7, 5, 7, 5);
            btnShowCurrentPrediction.Name = "btnShowCurrentPrediction";
            btnShowCurrentPrediction.Size = new Size(458, 46);
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
            btnStartTest.Location = new Point(647, 290);
            btnStartTest.Margin = new Padding(7, 5, 7, 5);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(458, 53);
            btnStartTest.TabIndex = 1;
            btnStartTest.Text = "테스트 시작";
            toolTip.SetToolTip(btnStartTest, "모델 예측 테스트 시작");
            btnStartTest.UseVisualStyleBackColor = false;
            btnStartTest.Click += btnStartTest_Click;
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
            // lblTrainingStatusValue
            // 
            lblTrainingStatusValue.AutoSize = true;
            lblTrainingStatusValue.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingStatusValue.ForeColor = Color.FromArgb(45, 212, 191);
            lblTrainingStatusValue.Location = new Point(350, 106);
            lblTrainingStatusValue.Name = "lblTrainingStatusValue";
            lblTrainingStatusValue.Size = new Size(15, 19);
            lblTrainingStatusValue.TabIndex = 11;
            lblTrainingStatusValue.Text = "-";
            // 
            // lblTrainingStatusCaption
            // 
            lblTrainingStatusCaption.AutoSize = true;
            lblTrainingStatusCaption.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingStatusCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingStatusCaption.Location = new Point(275, 109);
            lblTrainingStatusCaption.Name = "lblTrainingStatusCaption";
            lblTrainingStatusCaption.Size = new Size(31, 15);
            lblTrainingStatusCaption.TabIndex = 10;
            lblTrainingStatusCaption.Text = "상태";
            // 
            // lblTrainingValLossValue
            // 
            lblTrainingValLossValue.AutoSize = true;
            lblTrainingValLossValue.Font = new Font("맑은 고딕", 11F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingValLossValue.ForeColor = Color.FromArgb(238, 243, 249);
            lblTrainingValLossValue.Location = new Point(600, 72);
            lblTrainingValLossValue.Name = "lblTrainingValLossValue";
            lblTrainingValLossValue.Size = new Size(15, 20);
            lblTrainingValLossValue.TabIndex = 9;
            lblTrainingValLossValue.Text = "-";
            // 
            // lblTrainingValLossCaption
            // 
            lblTrainingValLossCaption.AutoSize = true;
            lblTrainingValLossCaption.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingValLossCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingValLossCaption.Location = new Point(515, 76);
            lblTrainingValLossCaption.Name = "lblTrainingValLossCaption";
            lblTrainingValLossCaption.Size = new Size(59, 15);
            lblTrainingValLossCaption.TabIndex = 8;
            lblTrainingValLossCaption.Text = "검증 손실";
            // 
            // lblTrainingLossValue
            // 
            lblTrainingLossValue.AutoSize = true;
            lblTrainingLossValue.Font = new Font("맑은 고딕", 11F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingLossValue.ForeColor = Color.FromArgb(238, 243, 249);
            lblTrainingLossValue.Location = new Point(350, 72);
            lblTrainingLossValue.Name = "lblTrainingLossValue";
            lblTrainingLossValue.Size = new Size(15, 20);
            lblTrainingLossValue.TabIndex = 7;
            lblTrainingLossValue.Text = "-";
            // 
            // lblTrainingLossCaption
            // 
            lblTrainingLossCaption.AutoSize = true;
            lblTrainingLossCaption.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingLossCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingLossCaption.Location = new Point(275, 76);
            lblTrainingLossCaption.Name = "lblTrainingLossCaption";
            lblTrainingLossCaption.Size = new Size(31, 15);
            lblTrainingLossCaption.TabIndex = 6;
            lblTrainingLossCaption.Text = "손실";
            // 
            // lblTrainingEpochValue
            // 
            lblTrainingEpochValue.AutoSize = true;
            lblTrainingEpochValue.Font = new Font("맑은 고딕", 11F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingEpochValue.ForeColor = Color.FromArgb(238, 243, 249);
            lblTrainingEpochValue.Location = new Point(350, 39);
            lblTrainingEpochValue.Name = "lblTrainingEpochValue";
            lblTrainingEpochValue.Size = new Size(38, 20);
            lblTrainingEpochValue.TabIndex = 3;
            lblTrainingEpochValue.Text = "- / -";
            // 
            // lblTrainingEpochCaption
            // 
            lblTrainingEpochCaption.AutoSize = true;
            lblTrainingEpochCaption.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTrainingEpochCaption.ForeColor = Color.FromArgb(245, 176, 65);
            lblTrainingEpochCaption.Location = new Point(275, 43);
            lblTrainingEpochCaption.Name = "lblTrainingEpochCaption";
            lblTrainingEpochCaption.Size = new Size(31, 15);
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
            ((System.ComponentModel.ISupportInitialize)trackBar_tab2).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbTestImageNavigator).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTestPreview).EndInit();
            gbTrainingSetup.ResumeLayout(false);
            gbTrainingSetup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar trackBar_tab2;
        private Button btnReverse_tab2;
        private Button btnStop_tab2;
        private Button btnPlay_tab2;
    }
}
