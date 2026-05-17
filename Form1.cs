namespace DataManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataInfoGrid();
            Shown += Form1_Shown;
        }

        private void InitializeDataInfoGrid()
        {
            dgvDataInfo.Rows.Clear();
            dgvDataInfo.Rows.Add("데이터", "0");
            dgvDataInfo.Rows.Add("이미지", "0");
            dgvDataInfo.Rows.Add("조향값", "0");
            dgvDataInfo.Rows.Add("속도값", "0");
        }

        private void Form1_Shown(object? sender, EventArgs e)
        {
            dgvDataInfo.ClearSelection();
            dgvDataInfo.CurrentCell = null;
        }

        private void tpDataManager_Click(object sender, EventArgs e)
        {
        }

        private void btnCheckDataIntegrity_Click(object sender, EventArgs e)
        {
        }

        private void btnSelectAdd_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog folderBrowserDialog = new();
            folderBrowserDialog.Description = "불러올 데이터 폴더를 선택해";
            folderBrowserDialog.ShowNewFolderButton = false;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
