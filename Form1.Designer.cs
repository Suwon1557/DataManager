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
            tcMain.Location = new Point(0, 72);
            tcMain.Margin = new Padding(7, 5, 7, 5);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new Size(2850, 1347);
            tcMain.SizeMode = TabSizeMode.Fixed;
            tcMain.TabIndex = 0;
            // 
            // tpDataManager
            // 
            tpDataManager.BackColor = Color.FromArgb(28, 36, 54);
            tpDataManager.Controls.Add(gbDataContent);
            tpDataManager.Controls.Add(gbDataLoad);
            tpDataManager.Location = new Point(8, 38);
            tpDataManager.Margin = new Padding(7, 5, 7, 5);
            tpDataManager.Name = "tpDataManager";
            tpDataManager.Padding = new Padding(7, 5, 7, 5);
            tpDataManager.Size = new Size(2834, 1301);
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
            gbDataContent.Location = new Point(7, 197);
            gbDataContent.Margin = new Padding(7, 5, 7, 5);
            gbDataContent.Name = "gbDataContent";
            gbDataContent.Padding = new Padding(7, 5, 7, 5);
            gbDataContent.Size = new Size(2666, 1077);
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
            btnSetRange.Location = new Point(1989, 540);
            btnSetRange.Margin = new Padding(7, 5, 7, 5);
            btnSetRange.Name = "btnSetRange";
            btnSetRange.Size = new Size(338, 94);
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
            btnCancelRange.Location = new Point(2341, 540);
            btnCancelRange.Margin = new Padding(7, 5, 7, 5);
            btnCancelRange.Name = "btnCancelRange";
            btnCancelRange.Size = new Size(297, 94);
            btnCancelRange.TabIndex = 12;
            btnCancelRange.Text = "X";
            btnCancelRange.UseVisualStyleBackColor = false;
            btnCancelRange.Click += btnCancelRange_Click;
            // 
            // pnlImageRangeMarker
            // 
            pnlImageRangeMarker.BackColor = Color.FromArgb(245, 176, 65);
            pnlImageRangeMarker.Location = new Point(27, 661);
            pnlImageRangeMarker.Margin = new Padding(7, 5, 7, 5);
            pnlImageRangeMarker.Name = "pnlImageRangeMarker";
            pnlImageRangeMarker.Size = new Size(27, 22);
            pnlImageRangeMarker.TabIndex = 13;
            pnlImageRangeMarker.Visible = false;
            // 
            // tbImageNavigator
            // 
            tbImageNavigator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbImageNavigator.BackColor = Color.FromArgb(39, 50, 72);
            tbImageNavigator.Location = new Point(27, 637);
            tbImageNavigator.Margin = new Padding(7, 5, 7, 5);
            tbImageNavigator.Maximum = 100;
            tbImageNavigator.Name = "tbImageNavigator";
            tbImageNavigator.Size = new Size(2611, 90);
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
            btnCancelDelete.Location = new Point(1202, 337);
            btnCancelDelete.Margin = new Padding(7, 5, 7, 5);
            btnCancelDelete.Name = "btnCancelDelete";
            btnCancelDelete.Size = new Size(416, 135);
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
            btnDelete.Location = new Point(1632, 337);
            btnDelete.Margin = new Padding(7, 5, 7, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(331, 135);
            btnDelete.TabIndex = 9;
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
            btnFilter.Font = new Font("한컴 울주 천전리 각석체", 17.9999981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFilter.ForeColor = Color.FromArgb(45, 212, 191);
            btnFilter.Location = new Point(814, 337);
            btnFilter.Margin = new Padding(7, 5, 7, 5);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(375, 135);
            btnFilter.TabIndex = 8;
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click_1;
            // 
            // lblPlaybackSpeed
            // 
            lblPlaybackSpeed.AutoSize = true;
            lblPlaybackSpeed.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblPlaybackSpeed.ForeColor = Color.FromArgb(45, 212, 191);
            lblPlaybackSpeed.Location = new Point(729, 553);
            lblPlaybackSpeed.Margin = new Padding(7, 0, 7, 0);
            lblPlaybackSpeed.Name = "lblPlaybackSpeed";
            lblPlaybackSpeed.Size = new Size(90, 72);
            lblPlaybackSpeed.TabIndex = 7;
            lblPlaybackSpeed.Text = "x1";
            // 
            // tbPlaybackSpeed
            // 
            tbPlaybackSpeed.BackColor = Color.FromArgb(39, 50, 72);
            tbPlaybackSpeed.LargeChange = 1;
            tbPlaybackSpeed.Location = new Point(27, 553);
            tbPlaybackSpeed.Margin = new Padding(7, 5, 7, 5);
            tbPlaybackSpeed.Maximum = 4;
            tbPlaybackSpeed.Name = "tbPlaybackSpeed";
            tbPlaybackSpeed.Size = new Size(686, 90);
            tbPlaybackSpeed.TabIndex = 6;
            tbPlaybackSpeed.Value = 2;
            tbPlaybackSpeed.Scroll += tbPlaybackSpeed_Scroll;
            // 
            // btnReverse
            // 
            btnReverse.BackColor = Color.FromArgb(49, 62, 88);
            btnReverse.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnReverse.FlatStyle = FlatStyle.Flat;
            btnReverse.Font = new Font("굴림", 22.125F, FontStyle.Bold);
            btnReverse.ForeColor = Color.FromArgb(238, 243, 249);
            btnReverse.Location = new Point(1367, 553);
            btnReverse.Margin = new Padding(7, 5, 7, 5);
            btnReverse.Name = "btnReverse";
            btnReverse.Size = new Size(219, 72);
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
            btnStop.Font = new Font("굴림", 22.125F, FontStyle.Bold);
            btnStop.ForeColor = Color.FromArgb(238, 243, 249);
            btnStop.Location = new Point(1131, 554);
            btnStop.Margin = new Padding(7, 5, 7, 5);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(222, 72);
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
            btnPlay.Font = new Font("굴림", 22.125F, FontStyle.Bold);
            btnPlay.ForeColor = Color.FromArgb(238, 243, 249);
            btnPlay.Location = new Point(901, 554);
            btnPlay.Margin = new Padding(7, 5, 7, 5);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(217, 72);
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
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 14F, FontStyle.Bold, GraphicsUnit.Point, 129);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(49, 62, 88);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvDataInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvDataInfo.ColumnHeadersHeight = 48;
            dgvDataInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDataInfo.Columns.AddRange(new DataGridViewColumn[] { colDataName, colDataValue });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(22, 30, 46);
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(49, 62, 88);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(245, 176, 65);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvDataInfo.DefaultCellStyle = dataGridViewCellStyle3;
            dgvDataInfo.EnableHeadersVisualStyles = false;
            dgvDataInfo.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dgvDataInfo.GridColor = Color.FromArgb(103, 119, 148);
            dgvDataInfo.Location = new Point(814, 65);
            dgvDataInfo.Margin = new Padding(7, 5, 7, 5);
            dgvDataInfo.MultiSelect = false;
            dgvDataInfo.Name = "dgvDataInfo";
            dgvDataInfo.ReadOnly = true;
            dgvDataInfo.RowHeadersVisible = false;
            dgvDataInfo.RowHeadersWidth = 82;
            dgvDataInfo.ScrollBars = ScrollBars.None;
            dgvDataInfo.Size = new Size(1150, 248);
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
            lvDataItems.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lvDataItems.ForeColor = Color.FromArgb(238, 243, 249);
            lvDataItems.FullRowSelect = true;
            lvDataItems.GridLines = true;
            lvDataItems.Location = new Point(2000, 65);
            lvDataItems.Margin = new Padding(7, 5, 7, 5);
            lvDataItems.Name = "lvDataItems";
            lvDataItems.Size = new Size(636, 407);
            lvDataItems.TabIndex = 1;
            lvDataItems.UseCompatibleStateImageBehavior = false;
            lvDataItems.View = View.Details;
            lvDataItems.SelectedIndexChanged += lvDataItems_SelectedIndexChanged;
            // 
            // pbDataPreview
            // 
            pbDataPreview.BackColor = Color.FromArgb(12, 18, 30);
            pbDataPreview.BorderStyle = BorderStyle.FixedSingle;
            pbDataPreview.Location = new Point(27, 65);
            pbDataPreview.Margin = new Padding(7, 5, 7, 5);
            pbDataPreview.Name = "pbDataPreview";
            pbDataPreview.Size = new Size(770, 407);
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
            gbDataLoad.Location = new Point(7, 5);
            gbDataLoad.Margin = new Padding(7, 5, 7, 5);
            gbDataLoad.Name = "gbDataLoad";
            gbDataLoad.Padding = new Padding(7, 5, 7, 5);
            gbDataLoad.Size = new Size(2666, 180);
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
            btnCheckDataIntegrity.Font = new Font("한컴 고딕", 16.1249981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCheckDataIntegrity.ForeColor = Color.FromArgb(238, 243, 249);
            btnCheckDataIntegrity.Location = new Point(2199, 62);
            btnCheckDataIntegrity.Margin = new Padding(7, 5, 7, 5);
            btnCheckDataIntegrity.Name = "btnCheckDataIntegrity";
            btnCheckDataIntegrity.Size = new Size(439, 68);
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
            txtFolderPath.Font = new Font("Constantia", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFolderPath.ForeColor = Color.FromArgb(238, 243, 249);
            txtFolderPath.Location = new Point(368, 67);
            txtFolderPath.Margin = new Padding(7, 5, 7, 5);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(1808, 72);
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
            btnSelectFolder.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectFolder.ForeColor = Color.FromArgb(6, 42, 43);
            btnSelectFolder.Location = new Point(27, 62);
            btnSelectFolder.Margin = new Padding(7, 5, 7, 5);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(320, 68);
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
            tpTrainingTest.Margin = new Padding(7, 5, 7, 5);
            tpTrainingTest.Name = "tpTrainingTest";
            tpTrainingTest.Padding = new Padding(7, 5, 7, 5);
            tpTrainingTest.Size = new Size(2834, 1301);
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
            gbModelTest.Location = new Point(7, 251);
            gbModelTest.Margin = new Padding(7, 5, 7, 5);
            gbModelTest.Name = "gbModelTest";
            gbModelTest.Padding = new Padding(7, 5, 7, 5);
            gbModelTest.Size = new Size(2795, 1023);
            gbModelTest.TabIndex = 1;
            gbModelTest.TabStop = false;
            gbModelTest.Text = "모델 테스트";
            // 
            // tbTestImageNavigator
            // 
            tbTestImageNavigator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbTestImageNavigator.BackColor = Color.FromArgb(39, 50, 72);
            tbTestImageNavigator.Location = new Point(27, 920);
            tbTestImageNavigator.Margin = new Padding(7, 5, 7, 5);
            tbTestImageNavigator.Maximum = 100;
            tbTestImageNavigator.Name = "tbTestImageNavigator";
            tbTestImageNavigator.Size = new Size(2740, 90);
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
            btnStartTest.Location = new Point(27, 734);
            btnStartTest.Margin = new Padding(7, 5, 7, 5);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(907, 79);
            btnStartTest.TabIndex = 1;
            btnStartTest.Text = "테스트 시작";
            btnStartTest.UseVisualStyleBackColor = false;
            btnStartTest.Click += btnStartTest_Click;
            // 
            // pbTestPreview
            // 
            pbTestPreview.BackColor = Color.FromArgb(12, 18, 30);
            pbTestPreview.BorderStyle = BorderStyle.FixedSingle;
            pbTestPreview.Location = new Point(27, 62);
            pbTestPreview.Margin = new Padding(7, 5, 7, 5);
            pbTestPreview.Name = "pbTestPreview";
            pbTestPreview.Size = new Size(907, 650);
            pbTestPreview.SizeMode = PictureBoxSizeMode.StretchImage;
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
            gbTrainingSetup.Location = new Point(7, 5);
            gbTrainingSetup.Margin = new Padding(7, 5, 7, 5);
            gbTrainingSetup.Name = "gbTrainingSetup";
            gbTrainingSetup.Padding = new Padding(7, 5, 7, 5);
            gbTrainingSetup.Size = new Size(2795, 234);
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
            txtTrainingLog.Location = new Point(368, 62);
            txtTrainingLog.Margin = new Padding(7, 5, 7, 5);
            txtTrainingLog.Multiline = true;
            txtTrainingLog.Name = "txtTrainingLog";
            txtTrainingLog.ReadOnly = true;
            txtTrainingLog.ScrollBars = ScrollBars.Vertical;
            txtTrainingLog.Size = new Size(2383, 128);
            txtTrainingLog.TabIndex = 1;
            // 
            // btnTrain
            // 
            btnTrain.BackColor = Color.FromArgb(245, 176, 65);
            btnTrain.FlatAppearance.BorderColor = Color.FromArgb(245, 176, 65);
            btnTrain.FlatStyle = FlatStyle.Flat;
            btnTrain.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTrain.ForeColor = Color.FromArgb(48, 34, 8);
            btnTrain.Location = new Point(27, 62);
            btnTrain.Margin = new Padding(7, 5, 7, 5);
            btnTrain.Name = "btnTrain";
            btnTrain.Size = new Size(320, 126);
            btnTrain.TabIndex = 0;
            btnTrain.Text = "학습";
            btnTrain.UseVisualStyleBackColor = false;
            btnTrain.Click += btnTrain_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Britannic Bold", 19.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(245, 176, 65);
            lblTitle.Location = new Point(9, 12);
            lblTitle.Margin = new Padding(7, 0, 7, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(351, 58);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Data Manager";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(16F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 24, 38);
            ClientSize = new Size(2850, 1418);
            Controls.Add(lblTitle);
            Controls.Add(tcMain);
            Font = new Font("한컴 울주 천전리 각석체", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            Margin = new Padding(7, 5, 7, 5);
            MinimumSize = new Size(1700, 1029);
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
