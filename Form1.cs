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
            decimal[] playbackSpeeds = { 0.25m, 0.5m, 1m, 1.5m, 2m };
            decimal playbackSpeed = playbackSpeeds[tbPlaybackSpeed.Value];
            lblPlaybackSpeed.Text = $"x{playbackSpeed:0.##}";
        }

        private int GetImageNavigatorMarkerLeft(int value, Size markerSize)
        {
            int minimum = tbImageNavigator.Minimum;
            int maximum = tbImageNavigator.Maximum;

            double ratio = maximum == minimum
                ? 0
                : (double)(value - minimum) / (maximum - minimum);

            const int horizontalPadding = 10;
            int usableWidth = Math.Max(1, tbImageNavigator.Width - (horizontalPadding * 2));
            int markerCenterX = tbImageNavigator.Left + horizontalPadding + (int)Math.Round(usableWidth * ratio);

            return markerCenterX - (markerSize.Width / 2);
        }

        private void PositionImageRangeMarker(Panel marker, int value)
        {
            marker.Left = GetImageNavigatorMarkerLeft(value, marker.Size);
            marker.Top = tbImageNavigator.Top + 13;
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
            int value = tbImageNavigator.Value;

            Panel marker = CreateImageRangeMarker();
            marker.Tag = value;
            PositionImageRangeMarker(marker, value);

            _imageRangeMarkers.Add(marker);

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

        private void UpdateImageRangeMarkerPositions()
        {
            foreach (Panel marker in _imageRangeMarkers)
            {
                if (marker.Tag is int value)
                {
                    PositionImageRangeMarker(marker, value);
                }
            }
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
            tbPlaybackSpeed.Value = 2;
            UpdatePlaybackSpeedLabel();
            _isRangeSettingMode = false;
            pnlImageRangeMarker.Visible = false;

            EnsureChartVisible(chtSteeringValue);
            EnsureChartVisible(chtSpeedValue);
        }

        private static void EnsureChartVisible(System.Windows.Forms.DataVisualization.Charting.Chart? chart)
        {
            if (chart is null)
            {
                return;
            }

            chart.Visible = true;
            chart.BringToFront();

            if (chart.Series.Count > 0 && chart.Series[0].Points.Count == 0)
            {
                chart.Series[0].Points.AddXY(0, 0);
                chart.Series[0].Points.AddXY(1, 1);
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
            folderBrowserDialog.Description = "불러올 데이터 폴더를 선택";
            folderBrowserDialog.ShowNewFolderButton = false;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog.SelectedPath;
                txtFolderPath.ForeColor = Color.Black;
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

        private void btnDelete_Click(object sender, EventArgs e)
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

        private void gbDataContent_Resize(object sender, EventArgs e)
        {
            UpdateImageRangeMarkerPositions();
        }

        private void gbTrainingSetup_Enter(object sender, EventArgs e)
        {
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
        }

        private void tlpTestPreview_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancelDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
