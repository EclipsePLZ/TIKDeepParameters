using DeepParameters.Work_WIth_Files;
using DeepParameters.Work_WIth_Files.Interfaces;
using ExcelDataReader.Log;
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
        private const double MIN_NUMBER_VALUES_FOR_DEEP_LEVEL = 20;

        private IFileService fileService;
        private IDialogService dialogService = new DefaultDialogService();

        private List<double> AccidentValues { get; set; } = new List<double>();
        private string AccidentHeader { get; set; }
        private List<double> ReliabilityInterval { get; set; } = new List<double>();

        private Dictionary<string, double> correlCoeffs = new Dictionary<string, double>();

        private BackgroundWorker resizeWorker = new BackgroundWorker();
        private bool isResizeNeeded = false;

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
            printReliabToDataGV.Checked = false;
            ClearControlsStep3();
        }

        /// <summary>
        /// Function for clear controls start with step3
        /// </summary>
        private void ClearControlsStep3() {
            calcDeepLevelsButton.Enabled = false;
            numberOfMaxDeepLevel.Value = 1;
            deepLevelInfo.Items.Clear();
            selectedStatisticList.Items.Clear();
            allStatisticList.Items.Clear();
            ClearControlsStep4();
        }

        /// <summary>
        /// Function for clear controls start with step4
        /// </summary>
        private void ClearControlsStep4() {
            ClearDataGV(resultCorrelationCoefficients);
            thresholdCorrCoeff.Value = 0;
            filterCorrelCoeffsButton.Enabled = false;
            returnCoeffs.Enabled = false;
        }

        /// <summary>
        /// Clear headers and data from dataGridView
        /// </summary>
        /// <param name="data">dataGridView</param>
        /// <param name="columnHeadersVisible">Show headers of columns</param>
        private void ClearDataGV(DataGridView data, bool columnHeadersVisible=false) {
            data.Rows.Clear();
            data.ColumnHeadersVisible = columnHeadersVisible;
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
        private void SetDataGVColumnHeaders(List<string> headers, DataGridView dataGV, bool autoSize, List<int> indexOfSortableColumns = null) {
            dataGV.ColumnCount = headers.Count;
            for (int i = 0; i < dataGV.Columns.Count; i++) {
                dataGV.Columns[i].HeaderText = headers[i];
                dataGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (autoSize) {
                    dataGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            if (indexOfSortableColumns != null) {
                foreach (int index in indexOfSortableColumns) {
                    dataGV.Columns[index].SortMode = DataGridViewColumnSortMode.Automatic;
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
                if (accidentsData[selectedAccidentIndex, row].Value.ToString() != "") {
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
            ReliabilityInterval.Clear();
            ReliabilityInterval = Statistics.GetReliabilityInterval(AccidentValues, 
                Convert.ToInt32(numberOfValuesForNormLevel.Value), Convert.ToDouble(numberOfStdForMaxLevel.Value));

            // Clear data grid view
            ClearDataGV(dataSignalReliability);

            if (printReliabToDataGV.Checked) {
                RunBackgrounWorkerGetReliabilityForSignal();
            }

            ClearControlsStep3();
            
            // Set values for third step
            AddStatisticsToList();

            numberOfMaxDeepLevel.Maximum = Convert.ToDecimal(Math.Truncate(Math.Log
                (Convert.ToDouble(indexOfMaxValue.Text), MIN_NUMBER_VALUES_FOR_DEEP_LEVEL)));

            deepLevelInfo.Items.Add(GetNewRowOfDeepLevel(1));

            researchParametersTab.Enabled = true;

            if (!printReliabToDataGV.Checked) {
                allTabs.SelectTab(researchParametersTab);
            }
        }

        /// <summary>
        /// Find reliability for each value in accident in background
        /// </summary>
        private void RunBackgrounWorkerGetReliabilityForSignal() {
            SetDataGVColumnHeaders(new List<string>() { AccidentHeader, "Надежность" }, dataSignalReliability, true);

            // Run background worker
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.ProgressChanged += new ProgressChangedEventHandler((sender, e) => ProgressBarChanged(sender, e, progressBarReliability));
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => FindReliabilityForSignal(sender, e, bgWorker));
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            dataSignalReliability.Size = new Size(dataSignalReliability.Width, dataSignalReliability.Height - 25);
            progressBarReliability.Value = 0;
            progressBarReliability.Visible = true;
            bgWorker.RunWorkerAsync();
        }

        private void FindReliabilityForSignal(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Create start parameters for progress bar
            int progress = 0;
            int step = AccidentValues.Count / 100;
            int oneBarInProgress = 1;
            if (AccidentValues.Count < 100) {
                step = 1;
                oneBarInProgress = (100 / AccidentValues.Count) + 1;
            }

            int prevReliab = 100;

            for (int i = 0; i < AccidentValues.Count; i++) {
                // Find progress
                if (i % step == 0) {
                    progress += oneBarInProgress;
                    bgWorker.ReportProgress(progress);
                }

                prevReliab = GetReliabCoeff(prevReliab, AccidentValues[i]);
                // Add vibration value with reliability to data grid view
                dataSignalReliability.Invoke(new Action<List<string>>((row) => dataSignalReliability.Rows.Add(row.ToArray())),
                    new List<string>() { AccidentValues[i].ToString(), (prevReliab).ToString() + "%" });
            }

            // Hide progress bar
            progressBarReliability.Invoke(new Action<bool>((b) => progressBarReliability.Visible = b), false);

            // Resize dataGrid
            dataSignalReliability.Invoke(new Action<Size>((size) => dataSignalReliability.Size = size),
                new Size(dataSignalReliability.Width, dataSignalReliability.Height + 25));

            bgWorker.CancelAsync();
        }

        /// <summary>
        /// Get reliability coefficient for value from reliability interval
        /// </summary>
        /// <param name="prevReliab"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int GetReliabCoeff(int prevReliab, double value) {
            int currReliab = 100 - prevReliab;

            if (value == ReliabilityInterval.Last()) {
                return 0;
            }
            while (currReliab < 100 && value > ReliabilityInterval[currReliab]) {
                currReliab++;
            }
            return 100 - currReliab;
        }

        /// <summary>
        /// Add all statistics to list
        /// </summary>
        private void AddStatisticsToList() {
            foreach (string key in Statistics.Functions.Keys) {
                allStatisticList.Items.Add(key);
            }
        }

        private void toSelectList_Click(object sender, EventArgs e) {
            AddItemToSelectedList();
        }

        private void allStatisticList_DoubleClick(object sender, EventArgs e) {
            AddItemToSelectedList();
        }

        private void toAllList_Click(object sender, EventArgs e) {
            AddItemToAllList();
        }

        private void selectedStatisticList_DoubleClick(object sender, EventArgs e) {
            AddItemToAllList();
        }

        /// <summary>
        /// Move selected item from all list to selected list
        /// </summary>
        private void AddItemToSelectedList() {
            if (allStatisticList.SelectedItems.Count == 1) {
                int selectedIndex = allStatisticList.SelectedIndex;
                selectedStatisticList.Items.Add(allStatisticList.SelectedItem);
                allStatisticList.Items.Remove(allStatisticList.SelectedItem);
                if (allStatisticList.Items.Count > 0) {
                    if (selectedIndex < allStatisticList.Items.Count) {
                        allStatisticList.SelectedIndex = selectedIndex;
                    }
                    else {
                        allStatisticList.SelectedIndex = selectedIndex - 1;
                    }
                }
                CheckRulesForAcceptParamters();
            }
        }

        /// <summary>
        /// Move selected item from selected list to all list
        /// </summary>
        private void AddItemToAllList() {
            if (selectedStatisticList.SelectedItems.Count == 1) {
                int selectedIndex = selectedStatisticList.SelectedIndex;
                allStatisticList.Items.Add(selectedStatisticList.SelectedItem);
                selectedStatisticList.Items.Remove(selectedStatisticList.SelectedItem);
                if (selectedStatisticList.Items.Count > 0) {
                    if (selectedIndex < selectedStatisticList.Items.Count) {
                        selectedStatisticList.SelectedIndex = selectedIndex;
                    }
                    else {
                        selectedStatisticList.SelectedIndex = selectedIndex - 1;
                    }
                }
                CheckRulesForAcceptParamters();
            }
        }

        private void allToSelectList_Click(object sender, EventArgs e) {
            if (allStatisticList.Items.Count > 0) {
                selectedStatisticList.Items.AddRange(allStatisticList.Items);
                allStatisticList.Items.Clear();
                CheckRulesForAcceptParamters();
            }
        }

        private void allToAllList_Click(object sender, EventArgs e) {
            if (selectedStatisticList.Items.Count > 0) {
                allStatisticList.Items.AddRange(selectedStatisticList.Items);
                selectedStatisticList.Items.Clear();
                CheckRulesForAcceptParamters();
            }
        }

        /// <summary>
        /// Checking rules for enable accept button
        /// </summary>
        private void CheckRulesForAcceptParamters() {
            if (selectedStatisticList.Items.Count > 0) {
                calcDeepLevelsButton.Enabled = true;
            }
            else {
                calcDeepLevelsButton.Enabled = false;
            }
        }

        private void numberOfMaxDeepLevel_ValueChanged(object sender, EventArgs e) {
            // Add new deep levels to list
            if (numberOfMaxDeepLevel.Value > deepLevelInfo.Items.Count) {
                int numberOfNewLevels = (int)numberOfMaxDeepLevel.Value - deepLevelInfo.Items.Count;
                int lastLevelInlist = deepLevelInfo.Items.Count;

                for (int i = 1; i <= numberOfNewLevels; i++) {
                    deepLevelInfo.Items.Add(GetNewRowOfDeepLevel(lastLevelInlist + i));
                    int requiredNumberOfObser = CalcRequireNumberOfObservations();
                    if (requiredNumberOfObser > Convert.ToInt32(indexOfMaxValue.Text)) {
                        deepLevelInfo.Items.RemoveAt(deepLevelInfo.Items.Count - 1);
                        ShowTooMuchIntervalInLevel(Convert.ToInt32(indexOfMaxValue.Text), requiredNumberOfObser);
                        numberOfMaxDeepLevel.Value = numberOfMaxDeepLevel.Value + i - 2;
                        break;
                    }
                }
            }
            // Remove deep levels from list
            else if (numberOfMaxDeepLevel.Value < deepLevelInfo.Items.Count) {
                int numberOfLevelToRemove = deepLevelInfo.Items.Count - (int)numberOfMaxDeepLevel.Value;

                for (int i = 0; i < numberOfLevelToRemove; i++) {
                    deepLevelInfo.Items.RemoveAt(deepLevelInfo.Items.Count - 1);
                }
            }
        }


        /// <summary>
        /// Get row with infromation about deeo level
        /// </summary>
        /// <param name="level">Number of deep level</param>
        /// <param name="numberOfInterval">Number of values for calc of statistics</param>
        /// <returns>Row with information aboout deep level</returns>
        private string GetNewRowOfDeepLevel(int level, int numberOfInterval = (int)MIN_NUMBER_VALUES_FOR_DEEP_LEVEL) {
            return $"Уровень {level}: {numberOfInterval} наблюдений";
        }

        /// <summary>
        /// Show form for changing number of values for deep parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deepLevelInfo_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (deepLevelInfo.SelectedItem != null) {
                int oldNumValues = Convert.ToInt32(GetNumberOfObservationsFromLevel(deepLevelInfo.SelectedItem.ToString()));
                int selectIndex = deepLevelInfo.SelectedIndex;
                ChangeLevelValueForm formChangeValue = new ChangeLevelValueForm(selectIndex + 1, oldNumValues);
                formChangeValue.ShowDialog();

                deepLevelInfo.Items.RemoveAt(selectIndex);
                deepLevelInfo.Items.Insert(selectIndex, GetNewRowOfDeepLevel(formChangeValue.Level, formChangeValue.NumberOfValuesForLevel));

                // if number of require observations more than we have, then return the previous value
                int requiredNumberOfObser = CalcRequireNumberOfObservations();
                if (requiredNumberOfObser > Convert.ToInt32(indexOfMaxValue.Text)) {
                    deepLevelInfo.Items.RemoveAt(selectIndex);
                    deepLevelInfo.Items.Insert(selectIndex, GetNewRowOfDeepLevel(formChangeValue.Level, oldNumValues));
                    ShowTooMuchIntervalInLevel(Convert.ToInt32(indexOfMaxValue.Text), requiredNumberOfObser);
                }
            }
        }

        /// <summary>
        /// Show message that we have to much require number of observations
        /// </summary>
        /// <param name="maxObser">Max number of available observations</param>
        /// <param name="requiredNumberOfObser">Number of requiredNumberOfObser</param>
        private void ShowTooMuchIntervalInLevel(int maxObser, int requiredNumberOfObser) {
            MessageBox.Show("При заданном значении интервала невозможно построить необходимый глубинный уровень\n" +
                    $"Общее количество наблюдений: {maxObser}\n" +
                    $"Необходимое количество наблюдений: {requiredNumberOfObser}");
        }

        /// <summary>
        /// Calculating the required number of observations for find all statistics
        /// </summary>
        /// <returns></returns>
        private int CalcRequireNumberOfObservations() {
            int result = 1;
            foreach (var row in deepLevelInfo.Items) {
                result *= GetNumberOfObservationsFromLevel(row.ToString());
            }
            return result;
        }

        /// <summary>
        /// Get number of observations from one row from list of levels
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private int GetNumberOfObservationsFromLevel(string row) {
            return Convert.ToInt32(row.Split()[2]);
        }

        private void calcDeepLevelsButton_Click(object sender, EventArgs e) {
            ClearControlsStep4();

            RunBackgroundWorkerGetCorrCoeffs();

            // Set values for fourth step
            thresholdCorrCoeff.Value = 0;
            corrResultTab.Enabled = true;
            allTabs.SelectTab(corrResultTab);
        }

        /// <summary>
        /// Find correlation coefficients for each deep level for each statistics in background
        /// </summary>
        private void RunBackgroundWorkerGetCorrCoeffs() {
            SetDataGVColumnHeaders(new List<string>() { "Статистика", "Модуль коэффициента корреляции" },
                resultCorrelationCoefficients, true, new List<int>() { 1 });

            // Run background worker
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => GetCorrelationCoefficients(sender, e, bgWorker));
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();
        }

        private void GetCorrelationCoefficients(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            correlCoeffs.Clear();
            
            // Find Information about intervals
            int sizeOfInterval = CalcRequireNumberOfObservations();
            int indexWithMaxSignal = Convert.ToInt32(indexOfMaxValue.Text) - 1;
            int numberOfIntervals = (indexWithMaxSignal + 1) / sizeOfInterval;

            // List that contains all statistics calculating by all values
            List<List<Dictionary<string, List<double>>>> statisticsValues = new List<List<Dictionary<string, List<double>>>>();

            // List that contains intervals for first deep level
            List<List<double>> zeroLevel = SplitVibroSignalIntoIntervals(sizeOfInterval, 
                indexWithMaxSignal - (sizeOfInterval * numberOfIntervals), indexWithMaxSignal);

            // Dictionary for values of each statistic for calc correl
            Dictionary<string, List<double>> statisticValuesForCorrel = new Dictionary<string, List<double>>();

            // Get reliability list for calc correlation
            List<double> reliabList = GetReliabilityList(zeroLevel);

            // Find all deep statistics
            for (int i = 0; i < deepLevelInfo.Items.Count; i++) {

                // List with all statistics for every level
                List<Dictionary<string, List<double>>> nextIntervalStatistics = new List<Dictionary<string, List<double>>>();

                // Find number of require observation for current level
                int oneStep = GetNumberOfObservationsFromLevel(deepLevelInfo.Items[i].ToString());

                for (int interval = 0; interval < numberOfIntervals; interval++) {

                    // List that contains statistics values for one interval for each level step
                    Dictionary<string, List<double>> nextStatistics = new Dictionary<string, List<double>>();

                    // If we explore the first level, we will use zeroLevel list
                    if (i == 0) {
                        // Find number of steps in each interval
                        int numberOfSteps = (sizeOfInterval / oneStep);

                        // Find values for each statistics
                        foreach (var item in Statistics.Functions) {
                            if (!statisticValuesForCorrel.Keys.Contains(item.Key)) {
                                statisticValuesForCorrel[item.Key] = new List<double>();
                            }
                            nextStatistics[item.Key] = new List<double>();

                            // Add function result of each step to nextStatistics
                            for (int step = 0; step < numberOfSteps; step++) {
                                nextStatistics[item.Key].Add(item.Value(zeroLevel[interval].GetRange(step * oneStep, oneStep)));
                            }
                            statisticValuesForCorrel[item.Key].Add(nextStatistics[item.Key].Last());
                        }
                    }
                    // Ohterwise we will use data on previous levels (statisticsValues)
                    else {
                        // Find values for each statistics for each statistics that was calculated on the previous level
                        foreach (var prevStat in statisticsValues[i - 1][interval]) {
                            foreach (var item in Statistics.Functions) {
                                string combineStatName = item.Key + ", " + prevStat.Key;
                                if (!statisticValuesForCorrel.Keys.Contains(combineStatName)) {
                                    statisticValuesForCorrel[combineStatName] = new List<double>();
                                }
                                nextStatistics[combineStatName] = new List<double>();

                                // Find number of availavle step in previous level statistic
                                int numberOfSteps = prevStat.Value.Count / oneStep;

                                // Add function result of each step to nextStatistics
                                for (int step = 0; step < numberOfSteps; step++) {
                                    nextStatistics[combineStatName].
                                        Add(item.Value(prevStat.Value.GetRange(step * oneStep, oneStep)));
                                }

                                statisticValuesForCorrel[combineStatName].Add(nextStatistics[combineStatName].Last());
                            }
                        }
                    }
                    nextIntervalStatistics.Add(nextStatistics);
                }
                statisticsValues.Add(nextIntervalStatistics);
            }

            // Find correlation coefficients for each statistics
            foreach (var item in statisticValuesForCorrel) {
                double correl = Math.Abs(Statistics.PearsonCorrelationCoefficient(item.Value, reliabList));
                correlCoeffs[item.Key] = correl;
            }

            Action action = () => PrintCorrelationResult(correlCoeffs);
            resultCorrelationCoefficients.Invoke(action);

            filterCorrelCoeffsButton.Invoke(new Action<bool>((b) => filterCorrelCoeffsButton.Enabled = b), true);
            bgWorker.CancelAsync();
        }

        /// <summary>
        /// Split vibration signal on intervals
        /// </summary>
        /// <param name="numberValuesInInterval">Number of values in interval</param>
        /// <param name="startPosition">Start position for splitting</param>
        /// <param name="endPosition">End position for splitting</param>
        /// <returns>Intervals of vibration signal</returns>
        private List<List<double>> SplitVibroSignalIntoIntervals(int numberValuesInInterval, int startPosition, int endPosition) {
            List<List<double>> splitSignal = new List<List<double>>();

            for (int i = startPosition; i <= endPosition - numberValuesInInterval; i += numberValuesInInterval) {
                splitSignal.Add(AccidentValues.GetRange(i, numberValuesInInterval));
            }

            return splitSignal;
        }

        /// <summary>
        /// Get max values from each interval
        /// </summary>
        /// <param name="intervals">List of interval</param>
        /// <returns>Max values form each interval</returns>
        private List<double> GetMaxValuesOfEachInterval(List<List<double>> intervals) {
            List<double> maxValues = new List<double>();
            foreach (List<double> interval in intervals) {
                maxValues.Add(interval.Max());
            }
            return maxValues;
        }

        /// <summary>
        /// Get reliability list from values list
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>List of realiabilities</returns>
        private List<double> GetReliabilityList(List<List<double>> values) {
            // Find max values in each interval
            List<double> maxValuesOfEachInterval = GetMaxValuesOfEachInterval(values);
            List<double> reliabList = new List<double>();
            int prevReliab = 100;
            
            foreach (double value in maxValuesOfEachInterval) {
                prevReliab = GetReliabCoeff(prevReliab, value);
                reliabList.Add(prevReliab);
            }
            return reliabList;
        }

        /// <summary>
        /// Print correlation results in data grid view
        /// </summary>
        private void PrintCorrelationResult(Dictionary<string, double> corr) {
            ClearDataGV(resultCorrelationCoefficients, true);
            foreach (var item in corr) {
                resultCorrelationCoefficients.Rows.Add(new string[] { item.Key, item.Value.ToString() });
            }
        }


        private void filterCorrelCoeffsButton_Click(object sender, EventArgs e) {
            try {
                FilterCorrelationCoefficients(Convert.ToDouble(thresholdCorrCoeff.Value));
            }
            catch {
                MessageBox.Show("Вы ввели недопустипое пороговое значение");
            }
        }

        /// <summary>
        /// Filter correlation coefficients by threshold value
        /// </summary>
        /// <param name="thresholdValue">Threshold value</param>
        private void FilterCorrelationCoefficients(double thresholdValue) {
            Dictionary<string, double> corr = new Dictionary<string, double>();

            // Leave the statistics whose correlation coefficients are greater than the threshold value
            foreach (var item in correlCoeffs) {
                if (item.Value >= thresholdValue) {
                    corr.Add(item.Key, item.Value);
                }
            }

            ClearDataGV(resultCorrelationCoefficients, true);
            PrintCorrelationResult(corr);
            returnCoeffs.Enabled = true;
        }

        /// <summary>
        /// Print all correlation coefficients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnCoeffs_Click(object sender, EventArgs e) {
            ClearDataGV(resultCorrelationCoefficients, true);
            PrintCorrelationResult(correlCoeffs);
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
                case 2:
                    helpAllSteps.ToolTipText = StepsInfo.Step3;
                    break;
                case 3:
                    helpAllSteps.ToolTipText = StepsInfo.Step4;
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

        private void ValidateKeyPressedOnlyNums(object sender, KeyPressEventArgs e) {
            e.Handled = CheckNumericIntValue(e);
        }

        /// <summary>
        /// Check if predded numeric of backspace
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CheckNumericIntValue(KeyPressEventArgs e) {
            return (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8;
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
                    dataSignalReliability.Invoke(new Action<Size>((size) => dataSignalReliability.Size = size),
                        new Size(dataSignalReliability.Width + widthDiff, dataSignalReliability.Height + heightDiff));

                    progressBarReliability.Invoke(new Action<Point>((loc) => progressBarReliability.Location = loc),
                        new Point(progressBarReliability.Location.X, progressBarReliability.Location.Y + heightDiff));

                    progressBarReliability.Invoke(new Action<Size>((size) => progressBarReliability.Size = size),
                        new Size(dataSignalReliability.Width, progressBarReliability.Height));


                    // tab3
                    selectedStatisticList.Invoke(new Action<Size>((size) => selectedStatisticList.Size = size),
                        new Size(selectedStatisticList.Width, allTabs.Height - 113));

                    selectedStatisticList.Invoke(new Action<Point>((loc) => selectedStatisticList.Location = loc),
                        new Point(selectedStatisticList.Location.X + widthDiff, selectedStatisticList.Location.Y));

                    selectStatisticLabel.Invoke(new Action<Point>((loc) => selectStatisticLabel.Location = loc),
                        new Point(selectStatisticLabel.Location.X + widthDiff, selectStatisticLabel.Location.Y));

                    allStatisticList.Invoke(new Action<Size>((size) => allStatisticList.Size = size),
                        new Size(allStatisticList.Width, allTabs.Height - 113));

                    allStatisticList.Invoke(new Action<Point>((loc) => allStatisticList.Location = loc),
                        new Point(allStatisticList.Location.X + widthDiff, allStatisticList.Location.Y));

                    allStatisticLabel.Invoke(new Action<Point>((loc) => allStatisticLabel.Location = loc),
                        new Point(allStatisticLabel.Location.X + widthDiff, allStatisticLabel.Location.Y));

                    toSelectList.Invoke(new Action<Point>((loc) => toSelectList.Location = loc),
                        new Point(toSelectList.Location.X + widthDiff, toSelectList.Location.Y));

                    toAllList.Invoke(new Action<Point>((loc) => toAllList.Location = loc),
                        new Point(toAllList.Location.X + widthDiff, toAllList.Location.Y));

                    allToAllList.Invoke(new Action<Point>((loc) => allToAllList.Location = loc),
                        new Point(allToAllList.Location.X + widthDiff, allToAllList.Location.Y));

                    allToSelectList.Invoke(new Action<Point>((loc) => allToSelectList.Location = loc),
                        new Point(allToSelectList.Location.X + widthDiff, allToSelectList.Location.Y));

                    calcDeepLevelsButton.Invoke(new Action<Point>((loc) => calcDeepLevelsButton.Location = loc),
                        new Point(calcDeepLevelsButton.Location.X, calcDeepLevelsButton.Location.Y + heightDiff));

                    deepLevelInfo.Invoke(new Action<Size>((size) => deepLevelInfo.Size = size),
                        new Size(deepLevelInfo.Width, calcDeepLevelsButton.Location.Y - deepLevelInfo.Location.Y - 22));


                    // tab4
                    thresholdCorrCoeffLabel.Invoke(new Action<Point>((loc) => thresholdCorrCoeffLabel.Location = loc),
                        new Point(thresholdCorrCoeffLabel.Location.X + widthDiff, thresholdCorrCoeffLabel.Location.Y));

                    thresholdCorrCoeff.Invoke(new Action<Point>((loc) => thresholdCorrCoeff.Location = loc),
                        new Point(thresholdCorrCoeff.Location.X + widthDiff, thresholdCorrCoeff.Location.Y));

                    filterCorrelCoeffsButton.Invoke(new Action<Point>((loc) => filterCorrelCoeffsButton.Location = loc),
                       new Point(filterCorrelCoeffsButton.Location.X + widthDiff, filterCorrelCoeffsButton.Location.Y));

                    returnCoeffs.Invoke(new Action<Point>((loc) => returnCoeffs.Location = loc),
                       new Point(returnCoeffs.Location.X + widthDiff, returnCoeffs.Location.Y));

                    resultCorrelationCoefficients.Invoke(new Action<Size>((size) => resultCorrelationCoefficients.Size = size),
                        new Size(resultCorrelationCoefficients.Width + widthDiff, resultCorrelationCoefficients.Height + heightDiff));
                } 
            }
        }
    }
}
