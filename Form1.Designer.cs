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
        private GroupBox gbDataLoad;
        private Button btnFolderAdd;
        private TextBox txtFolderPath;
        private Button btnCheckDataIntegrity;
        private Label lblTitle;

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
            tcMain = new TabControl();
            tpDataManager = new TabPage();
            gbDataLoad = new GroupBox();
            btnCheckDataIntegrity = new Button();
            txtFolderPath = new TextBox();
            btnFolderAdd = new Button();
            tpTrainingTest = new TabPage();
            lblTitle = new Label();
            tcMain.SuspendLayout();
            tpDataManager.SuspendLayout();
            gbDataLoad.SuspendLayout();
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
            tcMain.Size = new Size(800, 580);
            tcMain.TabIndex = 0;
            // 
            // tpDataManager
            // 
            tpDataManager.Controls.Add(gbDataLoad);
            tpDataManager.Location = new Point(4, 24);
            tpDataManager.Name = "tpDataManager";
            tpDataManager.Padding = new Padding(3);
            tpDataManager.Size = new Size(792, 552);
            tpDataManager.TabIndex = 0;
            tpDataManager.Text = "데이터 관리";
            tpDataManager.UseVisualStyleBackColor = true;
            tpDataManager.Click += tpDataManager_Click;
            // 
            // gbDataLoad
            // 
            gbDataLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataLoad.Controls.Add(btnCheckDataIntegrity);
            gbDataLoad.Controls.Add(txtFolderPath);
            gbDataLoad.Controls.Add(btnFolderAdd);
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
            txtFolderPath.ForeColor = Color.Black;
            txtFolderPath.Location = new Point(161, 37);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(412, 35);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.Text = "(폴더경로)";
            // 
            // btnFolderAdd
            // 
            btnFolderAdd.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFolderAdd.ForeColor = Color.Black;
            btnFolderAdd.Location = new Point(12, 34);
            btnFolderAdd.Name = "btnFolderAdd";
            btnFolderAdd.Size = new Size(140, 38);
            btnFolderAdd.TabIndex = 0;
            btnFolderAdd.Text = "폴더선택";
            btnFolderAdd.UseVisualStyleBackColor = true;
            btnFolderAdd.Click += btnSelectAdd_Click;
            // 
            // tpTrainingTest
            // 
            tpTrainingTest.Location = new Point(4, 24);
            tpTrainingTest.Name = "tpTrainingTest";
            tpTrainingTest.Padding = new Padding(3);
            tpTrainingTest.Size = new Size(792, 552);
            tpTrainingTest.TabIndex = 1;
            tpTrainingTest.Text = "학습/테스트";
            tpTrainingTest.UseVisualStyleBackColor = true;
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
            ClientSize = new Size(800, 620);
            Controls.Add(lblTitle);
            Controls.Add(tcMain);
            Name = "Form1";
            Text = "Form1";
            tcMain.ResumeLayout(false);
            tpDataManager.ResumeLayout(false);
            gbDataLoad.ResumeLayout(false);
            gbDataLoad.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
