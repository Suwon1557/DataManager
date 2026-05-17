namespace DataManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
