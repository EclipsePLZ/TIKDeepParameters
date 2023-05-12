using DeepParameters.Work_WIth_Files;
using DeepParameters.Work_WIth_Files.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepParameters {
    public partial class MainFrom : Form {
        IFileService fileService;
        IDialogService dialogService = new DefaultDialogService();

        public MainFrom() {
            InitializeComponent();

            // Centered Main From on the screen
            this.CenterToScreen();

            // Locks all tabs except the first one
            LocksAllTabs();
            loadDataTab.Enabled = true;

            helpAllSteps.ToolTipText = StepsInfo.Step1;
        }

        /// <summary>
        /// Locks all tabs in tab collection
        /// </summary>
        private void LocksAllTabs() {
            foreach (TabPage tab in allTabs.TabPages) {
                tab.Enabled = false;
            }
        }

        /// <summary>
        /// Open file (csv, excel) with Vibration Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, EventArgs e) {
            ClearControlsStep1();
            allTabs.SelectTab(loadDataTab);

            if (dialogService.OpenFileDialog() == true) {
                fileService = GetFileService(dialogService.FilePath);

                RunBackgroundWorkerLoadFile();
            }
        }

        /// <summary>
        /// Function for clear controls on step1
        /// </summary>
        private void ClearControlsStep1() {
            ClearDataGVHeaders(accidentsData);
            selectAccident.Items.Clear();
        }

        /// <summary>
        /// Clear headers from dataGridView
        /// </summary>
        /// <param name="data">dataGridView</param>
        private void ClearDataGVHeaders(DataGridView data) {
            data.Rows.Clear();
            data.ColumnHeadersVisible = false;
            data.Refresh();
        }

        /// <summary>
        /// Get right file service for reading file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <returns>File Service</returns>
        IFileService GetFileService(string filename) {
            switch (filename.Split('.').Last()) {
                case "xls":
                    return new ExcelFileService();

                case "xlsx":
                    return new ExcelFileService();

                default:
                    return new ExcelFileService();
            }
        }

        private void RunBackgroundWorkerLoadFile() {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.ProgressChanged += new ProgressChangedEventHandler((sender, e) => ProgressBarChanged(sender, e, progressBarDataLoad));
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => LoadData(sender, e, bgWorker));
            bgWorker.WorkerReportsProgress = true;
            accidentsData.Size = new Size(accidentsData.Width, accidentsData.Height - 25);
            progressBarDataLoad.Value = 0;
            progressBarDataLoad.Visible = true;
            bgWorker.RunWorkerAsync();
        }

        private void LoadData(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            List<List<string>> allRows = fileService.Open(dialogService.FilePath, bgWorker);
            if (allRows.Count > 0) {
                accidentsData.Invoke(new Action<List<string>>((s) => SetDataGVColumnHeaders(s, accidentsData, false)), allRows[0]);

                for (int i = 1; i < allRows.Count; i++) {
                    accidentsData.Invoke(new Action<List<string>>((s) => accidentsData.Rows.Add(s.ToArray())), allRows[i]);
                }
            }
            // Добавить столбцы в дата грид
        }

        private void ProgressBarChanged(object sender, ProgressChangedEventArgs e, ProgressBar progressBar) {
            if (e.ProgressPercentage > 100) {
                progressBar.Value = 100;
            }
            else {
                progressBar.Value = e.ProgressPercentage;
            }
        }

        /// <summary>
        /// Set column headers and column settings to dataGV
        /// </summary>
        /// <param name="headers">List of column headers</param>
        /// <param name="dataGV">DataGridView</param>
        /// <param name="autoSize">AutoSize column width</param>
        private void SetDataGVColumnHeaders(List<string> headers, DataGridView dataGV, bool autoSize) {
            for (int i = 0; i < dataGV.Columns.Count; i++) {
                dataGV.Columns[i].HeaderText = headers[i];
                dataGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (autoSize) {
                    dataGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            dataGV.ColumnHeadersVisible = true;
        }

        private void MainFrom_Resize(object sender, EventArgs e) {
            int newWidth = this.Width - 28;
            int newHeight = this.Height - 67;
            int widthDiff = newWidth - allTabs.Width;
            int heightDiff = newHeight - allTabs.Height;
            allTabs.Size = new Size(newWidth, newHeight);

            // tab1
            choosenAccidentLabel.Location = new Point(choosenAccidentLabel.Location.X + widthDiff, choosenAccidentLabel.Location.Y);
            selectAccident.Location = new Point(selectAccident.Location.X + widthDiff, selectAccident.Location.Y);
            acceptFaultButton.Location = new Point(acceptFaultButton.Location.X + widthDiff, acceptFaultButton.Location.Y + heightDiff);
            accidentsData.Size = new Size(newWidth - 192, newHeight - 35);
            progressBarDataLoad.Location = new Point(progressBarDataLoad.Location.X, accidentsData.Size.Height - 17);
            progressBarDataLoad.Size = new Size(accidentsData.Width, progressBarDataLoad.Height);
        }

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitApp_Click(object sender, EventArgs e) {
            var exitForm = new ExitForm();
            exitForm.Show();
        }
    }
}
