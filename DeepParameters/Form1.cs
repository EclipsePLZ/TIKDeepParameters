using DeepParameters.Work_WIth_Files;
using DeepParameters.Work_WIth_Files.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepParameters {
    public partial class MainFrom : Form {
        private IFileService fileService;
        private IDialogService dialogService = new DefaultDialogService();

        private List<double> AccidentValues { get; set; } = new List<double>();
        private string AccidentHeader { get; set; }

        BackgroundWorker resizeWorker = new BackgroundWorker();
        bool isResizeNeeded = false;

        public MainFrom() {
            InitializeComponent();

            // Centered Main From on the screen
            this.CenterToScreen();

            // Locks all tabs except the first one
            LocksAllTabs();
            loadDataTab.Enabled = true;

            helpAllSteps.ToolTipText = StepsInfo.Step1;

            numberOfStdForMaxLevel.Maximum = Decimal.MaxValue;

            resizeWorker.DoWork += new DoWorkEventHandler(DoResizeComponents);
            resizeWorker.WorkerSupportsCancellation = true;
            resizeWorker.RunWorkerAsync();
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
        /// Function for clear controls start with step1
        /// </summary>
        private void ClearControlsStep1() {
            ClearDataGV(accidentsData);
            selectAccident.Items.Clear();
            ClearControlsStep2();
        }

        /// <summary>
        /// Funciton for clear controls start with step2
        /// </summary>
        private void ClearControlsStep2() {
            numberOfValuesForNormLevel.Value = 1;
            numberOfStdForMaxLevel.Value = 1;
            numberOfValuesInAcc.Clear();
            indexOfMaxValue.Clear();
            ClearDataGV(dataSignalReliability);
            ClearControlsStep3();
        }

        /// <summary>
        /// Function for clear controls start with step3
        /// </summary>
        private void ClearControlsStep3() {

        }

        /// <summary>
        /// Clear headers from dataGridView
        /// </summary>
        /// <param name="data">dataGridView</param>
        private void ClearDataGV(DataGridView data) {
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

        /// <summary>
        /// Run background worker for load dataset
        /// </summary>
        private void RunBackgroundWorkerLoadFile() {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.ProgressChanged += new ProgressChangedEventHandler((sender, e) => ProgressBarChanged(sender, e, progressBarDataLoad));
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => LoadData(sender, e, bgWorker));
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            accidentsData.Size = new Size(accidentsData.Width, accidentsData.Height - 25);
            progressBarDataLoad.Value = 0;
            progressBarDataLoad.Visible = true;
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Load dataset from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void LoadData(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending == true) {
                e.Cancel = true;
            }
            else {
                List<List<string>> allRows = fileService.Open(dialogService.FilePath);
                if (allRows.Count > 0) {
                    // Add headers to data grid veiw
                    accidentsData.Invoke(new Action<List<string>>((s) => SetDataGVColumnHeaders(s, accidentsData, false)), allRows[0]);

                    // Create start parameters for progress bar
                    int progress = 0;
                    int step = (allRows.Count - 1) / 100;
                    int oneBarInProgress = 1;
                    if ((allRows.Count - 1) < 100) {
                        step = 1;
                        oneBarInProgress = (100 / (allRows.Count - 1)) + 1;
                    }

                    for (int i = 1; i < allRows.Count; i++) {
                        // Find progress
                        if ((i - 1) % step == 0) {
                            progress += oneBarInProgress;
                            bgWorker.ReportProgress(progress);
                        }

                        // Add row to dataGridView
                        accidentsData.Invoke(new Action<List<string>>((s) => accidentsData.Rows.Add(s.ToArray())), allRows[i]);
                    }

                    // Add headers to comboBox
                    selectAccident.Invoke(new Action<List<string>>((s) => selectAccident.Items.AddRange(s.ToArray())), allRows[0].Skip(1).ToList());
                    selectAccident.Invoke(new Action<int>((n) => selectAccident.SelectedIndex = n), 0);

                    // Hide progress bar
                    progressBarDataLoad.Invoke(new Action<bool>((b) => progressBarDataLoad.Visible = b), false);

                    // Resize dataGrid
                    accidentsData.Invoke(new Action<Size>((size) => accidentsData.Size = size),
                        new Size(accidentsData.Width, accidentsData.Height + 25));

                    // Enable select accident button
                    acceptFaultButton.Invoke(new Action<bool>((b) => acceptFaultButton.Enabled = b), true);

                    bgWorker.CancelAsync();
                }
            }
        }

        /// <summary>
        /// Change progress bar value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="progressBar">Progress bar that will be changed</param>
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
            dataGV.ColumnCount = headers.Count;
            for (int i = 0; i < dataGV.Columns.Count; i++) {
                dataGV.Columns[i].HeaderText = headers[i];
                dataGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (autoSize) {
                    dataGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            dataGV.ColumnHeadersVisible = true;
        }

        /// <summary>
        /// Select accident for research
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acceptFaultButton_Click(object sender, EventArgs e) {
            AccidentHeader = selectAccident.Text;
            AccidentValues.Clear();
            int selectedAccidentIndex = selectAccident.SelectedIndex + 1;

            // Fill AccidentValues list from dataGridView
            for(int row = 0; row < accidentsData.Rows.Count; row++) {
                if (accidentsData[selectedAccidentIndex, row].Value != "") {
                    AccidentValues.Add(Convert.ToDouble(accidentsData[selectedAccidentIndex, row].Value));
                }
            }
            ClearControlsStep2();

            findReliabIntervalTab.Enabled = true;

            // Fill accident statistics on second tab
            int maxValueIndex = AccidentValues.IndexOf(AccidentValues.Max()) + 1;
            numberOfValuesInAcc.Text = AccidentValues.Count.ToString();
            indexOfMaxValue.Text = maxValueIndex.ToString();
            numberOfValuesForNormLevel.Maximum = maxValueIndex;

            allTabs.SelectTab(findReliabIntervalTab);
        }

        private void buttonCalcReliabilityInterval_Click(object sender, EventArgs e) {
            List<double> reliabilityInterval = Statistics.GetReliabilityInterval(AccidentValues, 
                Convert.ToInt32(numberOfValuesForNormLevel.Value), Convert.ToDouble(numberOfStdForMaxLevel.Value));

            int currReliab = 0;

            // Clear data grid view
            ClearDataGV(dataSignalReliability);

            RunBackgrounWorkerGetReliabilityForSignal();

            ClearControlsStep3();
            // Установка значений для следующего шага
        }

        /// <summary>
        /// Find reliability for each value in accident in background
        /// </summary>
        private void RunBackgrounWorkerGetReliabilityForSignal() {
            SetDataGVColumnHeaders(new List<string>() { AccidentHeader, "Надежность" }, dataSignalReliability, false);

            // Run background worker
            BackgroundWorker bgWoker = new BackgroundWorker();
            bgWoker.ProgressChanged += new ProgressChangedEventHandler((sender, e) => ProgressBarChanged(sender, e, progressBarReliability));
            bgWoker.DoWork += new DoWorkEventHandler((sender, e) => FindReliabilityForSignal(sender, e, bgWoker));
            bgWoker.WorkerReportsProgress = true;
            bgWoker.WorkerSupportsCancellation = true;
            dataSignalReliability.Size = new Size(dataSignalReliability.Width, dataSignalReliability.Height - 25);
            progressBarReliability.Value = 0;
            progressBarReliability.Visible = true;
            bgWoker.RunWorkerAsync();
        }

        private void FindReliabilityForSignal(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            List<double> reliabilityInterval = Statistics.GetReliabilityInterval(AccidentValues,
                Convert.ToInt32(numberOfValuesForNormLevel.Value), Convert.ToDouble(numberOfStdForMaxLevel.Value));


            // Create start parameters for progress bar
            int progress = 0;
            int step = AccidentValues.Count / 100;
            int oneBarInProgress = 1;
            if (AccidentValues.Count < 100) {
                step = 1;
                oneBarInProgress = (100 / AccidentValues.Count) + 1;
            }

            int currReliab = 0;

            for (int i = 0; i < AccidentValues.Count; i++) {
                // Find progress
                if (i % step == 0) {
                    progress += oneBarInProgress;
                    bgWorker.ReportProgress(progress);
                }

                // Find reliability for the current signal
                while (currReliab < 100 && ((currReliab == 0 && AccidentValues[i] > reliabilityInterval[currReliab]) || 
                    (currReliab > 0 && AccidentValues[i] >= reliabilityInterval[currReliab]))) {
                    currReliab++;
                }

                // Add vibration value with reliability to data grid view
                dataSignalReliability.Invoke(new Action<List<string>>((row) => dataSignalReliability.Rows.Add(row.ToArray())),
                    new List<string>() { AccidentValues[i].ToString(), (100 - currReliab).ToString() });
            }

            // Hide progress bar
            progressBarReliability.Invoke(new Action<bool>((b) => progressBarReliability.Visible = b), false);

            // Resize dataGrid
            dataSignalReliability.Invoke(new Action<Size>((size) => dataSignalReliability.Size = size),
                new Size(dataSignalReliability.Width, dataSignalReliability.Height + 25));

            bgWorker.CancelAsync();
        }

        /// <summary>
        /// Change information text about step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allTabs_Selected(object sender, TabControlEventArgs e) {
            switch (allTabs.SelectedIndex) {
                case 0:
                    helpAllSteps.ToolTipText = StepsInfo.Step1;
                    break;
                case 1:
                    helpAllSteps.ToolTipText = StepsInfo.Step2;
                    break;
            }
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

        private void MainFrom_ResizeBegin(object sender, EventArgs e) {
            isResizeNeeded = true;
        }

        private void MainFrom_ResizeEnd(object sender, EventArgs e) {
            isResizeNeeded = false;
        }

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e) {
            resizeWorker.CancelAsync();
        }

        /// <summary>
        /// Resize all main from components
        /// </summary>
        private void DoResizeComponents(object sender, DoWorkEventArgs e) {
            while (true) {
                if (isResizeNeeded) {
                    int newWidth = this.Width - 28;
                    int newHeight = this.Height - 67;
                    int widthDiff = newWidth - allTabs.Width;
                    int heightDiff = newHeight - allTabs.Height;
                    allTabs.Invoke(new Action<Size>((size) => allTabs.Size = size), new Size(newWidth, newHeight));

                    // tab1
                    choosenAccidentLabel.Invoke(new Action<Point>((loc) => choosenAccidentLabel.Location = loc),
                        new Point(choosenAccidentLabel.Location.X + widthDiff, choosenAccidentLabel.Location.Y));

                    selectAccident.Invoke(new Action<Point>((loc) => selectAccident.Location = loc),
                        new Point(selectAccident.Location.X + widthDiff, selectAccident.Location.Y));

                    acceptFaultButton.Invoke(new Action<Point>((loc) => acceptFaultButton.Location = loc),
                        new Point(acceptFaultButton.Location.X + widthDiff, acceptFaultButton.Location.Y + heightDiff));

                    accidentsData.Invoke(new Action<Size>((size) => accidentsData.Size = size), 
                        new Size(accidentsData.Width + widthDiff, accidentsData.Height + heightDiff));

                    progressBarDataLoad.Invoke(new Action<Point>((loc) => progressBarDataLoad.Location = loc),
                        new Point(progressBarDataLoad.Location.X, progressBarDataLoad.Location.Y + heightDiff));

                    progressBarDataLoad.Invoke(new Action<Size>((size) => progressBarDataLoad.Size = size), 
                        new Size(accidentsData.Width, progressBarDataLoad.Height));


                    // tab2
                    buttonCalcReliabilityInterval.Invoke(new Action<Point>((loc) => buttonCalcReliabilityInterval.Location = loc),
                        new Point(buttonCalcReliabilityInterval.Location.X, buttonCalcReliabilityInterval.Location.Y + heightDiff));

                    dataSignalReliability.Invoke(new Action<Size>((size) => dataSignalReliability.Size = size),
                        new Size(dataSignalReliability.Width + widthDiff, dataSignalReliability.Height + heightDiff));

                    progressBarReliability.Invoke(new Action<Point>((loc) => progressBarReliability.Location = loc),
                        new Point(progressBarReliability.Location.X, progressBarReliability.Location.Y + heightDiff));

                    progressBarReliability.Invoke(new Action<Size>((size) => progressBarReliability.Size = size),
                        new Size(dataSignalReliability.Width, progressBarReliability.Height));
                } 
            }
        }

        
    }
}
