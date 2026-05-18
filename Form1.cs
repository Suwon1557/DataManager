namespace DataManager
{
    public partial class Form1 : Form
    {
        private readonly List<Panel> _imageRangeMarkers = new();
        private bool _isRangeSettingMode;

        public Form1()
        {
            InitializeComponent();
            InitializeDataInfoGrid();
            UpdatePlaybackSpeedLabel();
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

        private void UpdatePlaybackSpeedLabel()
        {
            decimal playbackSpeed = tbPlaybackSpeed.Value / 100m;
            lblPlaybackSpeed.Text = $"x{playbackSpeed:0.##}";
        }

        private Panel CreateImageRangeMarker()
        {
            return new Panel
            {
                BackColor = Color.FromArgb(255, 114, 16),
                Size = pnlImageRangeMarker.Size,
                Visible = true
            };
        }

        private void AddImageRangeMarker()
        {
            int minimum = tbImageNavigator.Minimum;
            int maximum = tbImageNavigator.Maximum;
            int value = tbImageNavigator.Value;

            double ratio = maximum == minimum
                ? 0
                : (double)(value - minimum) / (maximum - minimum);

            const int horizontalPadding = 10;
            int usableWidth = Math.Max(1, tbImageNavigator.Width - (horizontalPadding * 2));
            int markerCenterX = tbImageNavigator.Left + horizontalPadding + (int)Math.Round(usableWidth * ratio);

            Panel marker = CreateImageRangeMarker();
            marker.Left = markerCenterX - (marker.Width / 2);
            marker.Top = tbImageNavigator.Top + 13;

            _imageRangeMarkers.Add(marker);

            // 마커는 최대 2개까지만 유지하고, 3번째가 들어오면 가장 오래된 마커를 지운다.
            if (_imageRangeMarkers.Count > 2)
            {
                Panel oldestMarker = _imageRangeMarkers[0];
                gbDataContent.Controls.Remove(oldestMarker);
                oldestMarker.Dispose();
                _imageRangeMarkers.RemoveAt(0);
            }

            gbDataContent.Controls.Add(marker);
            marker.BringToFront();
        }

        private void ClearImageRangeMarkers()
        {
            foreach (Panel marker in _imageRangeMarkers)
            {
                gbDataContent.Controls.Remove(marker);
                marker.Dispose();
            }

            _imageRangeMarkers.Clear();
            _isRangeSettingMode = false;
        }

        private void Form1_Shown(object? sender, EventArgs e)
        {
            dgvDataInfo.ClearSelection();
            dgvDataInfo.CurrentCell = null;
            tbPlaybackSpeed.Value = 100;
            UpdatePlaybackSpeedLabel();
            _isRangeSettingMode = false;
            pnlImageRangeMarker.Visible = false;

            // 디자이너/런타임 상태가 꼬여도 차트는 항상 표시되게 강제로 맞춘다.
            chtSteeringValue.Visible = true;
            chtSpeedValue.Visible = true;
            chtSteeringValue.BringToFront();
            chtSpeedValue.BringToFront();

            if (chtSteeringValue.Series.Count > 0 && chtSteeringValue.Series[0].Points.Count == 0)
            {
                chtSteeringValue.Series[0].Points.AddXY(0, 0);
                chtSteeringValue.Series[0].Points.AddXY(1, 1);
            }

            if (chtSpeedValue.Series.Count > 0 && chtSpeedValue.Series[0].Points.Count == 0)
            {
                chtSpeedValue.Series[0].Points.AddXY(0, 0);
                chtSpeedValue.Series[0].Points.AddXY(1, 1);
            }
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

        private void tbPlaybackSpeed_Scroll(object sender, EventArgs e)
        {
            UpdatePlaybackSpeedLabel();
        }

        private void btnSetRange_Click(object sender, EventArgs e)
        {
            _isRangeSettingMode = true;
        }

        private void tbImageNavigator_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isRangeSettingMode)
            {
                return;
            }

            AddImageRangeMarker();
        }

        private void btnDeleteRange_Click(object sender, EventArgs e)
        {
            ClearImageRangeMarkers();
        }

        private void btnCancelRange_Click(object sender, EventArgs e)
        {
            ClearImageRangeMarkers();
        }

        private void gbDataContent_Enter(object sender, EventArgs e)
        {

        }
    }
}
