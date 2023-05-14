namespace DeepParameters {
    partial class MainFrom {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrom));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.WorkFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAllSteps = new System.Windows.Forms.ToolStripMenuItem();
            this.allTabs = new System.Windows.Forms.TabControl();
            this.loadDataTab = new System.Windows.Forms.TabPage();
            this.progressBarDataLoad = new System.Windows.Forms.ProgressBar();
            this.accidentsData = new System.Windows.Forms.DataGridView();
            this.acceptFaultButton = new System.Windows.Forms.Button();
            this.choosenAccidentLabel = new System.Windows.Forms.Label();
            this.selectAccident = new System.Windows.Forms.ComboBox();
            this.findReliabIntervalTab = new System.Windows.Forms.TabPage();
            this.labelNumberOfValuesInAcc = new System.Windows.Forms.Label();
            this.numberOfValuesInAcc = new System.Windows.Forms.TextBox();
            this.labelIndexWithMaxValue = new System.Windows.Forms.Label();
            this.indexOfMaxValue = new System.Windows.Forms.TextBox();
            this.labelNumberOfValuesForNormLevel = new System.Windows.Forms.Label();
            this.numberOfValuesForNormLevel = new System.Windows.Forms.NumericUpDown();
            this.labelNumberOfStdForMaxLevel = new System.Windows.Forms.Label();
            this.numberOfStdForMaxLevel = new System.Windows.Forms.NumericUpDown();
            this.buttonCalcReliabilityInterval = new System.Windows.Forms.Button();
            this.dataSignalReliability = new System.Windows.Forms.DataGridView();
            this.progressBarReliability = new System.Windows.Forms.ProgressBar();
            this.researchParametersTab = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip.SuspendLayout();
            this.allTabs.SuspendLayout();
            this.loadDataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accidentsData)).BeginInit();
            this.findReliabIntervalTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfValuesForNormLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfStdForMaxLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSignalReliability)).BeginInit();
            this.researchParametersTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WorkFileMenuItem,
            this.ExitApp,
            this.helpAllSteps});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(836, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // WorkFileMenuItem
            // 
            this.WorkFileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile});
            this.WorkFileMenuItem.Name = "WorkFileMenuItem";
            this.WorkFileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.WorkFileMenuItem.Text = "Файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(121, 22);
            this.OpenFile.Text = "Открыть";
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // ExitApp
            // 
            this.ExitApp.Name = "ExitApp";
            this.ExitApp.Size = new System.Drawing.Size(54, 20);
            this.ExitApp.Text = "Выход";
            this.ExitApp.Click += new System.EventHandler(this.ExitApp_Click);
            // 
            // helpAllSteps
            // 
            this.helpAllSteps.Name = "helpAllSteps";
            this.helpAllSteps.Size = new System.Drawing.Size(68, 20);
            this.helpAllSteps.Text = "Помощь";
            // 
            // allTabs
            // 
            this.allTabs.Controls.Add(this.loadDataTab);
            this.allTabs.Controls.Add(this.findReliabIntervalTab);
            this.allTabs.Controls.Add(this.researchParametersTab);
            this.allTabs.Location = new System.Drawing.Point(12, 27);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.Size = new System.Drawing.Size(824, 416);
            this.allTabs.TabIndex = 2;
            this.allTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.allTabs_Selected);
            // 
            // loadDataTab
            // 
            this.loadDataTab.Controls.Add(this.progressBarDataLoad);
            this.loadDataTab.Controls.Add(this.accidentsData);
            this.loadDataTab.Controls.Add(this.acceptFaultButton);
            this.loadDataTab.Controls.Add(this.choosenAccidentLabel);
            this.loadDataTab.Controls.Add(this.selectAccident);
            this.loadDataTab.Location = new System.Drawing.Point(4, 22);
            this.loadDataTab.Name = "loadDataTab";
            this.loadDataTab.Padding = new System.Windows.Forms.Padding(3);
            this.loadDataTab.Size = new System.Drawing.Size(816, 390);
            this.loadDataTab.TabIndex = 0;
            this.loadDataTab.Text = "Загрузка данных";
            this.loadDataTab.UseVisualStyleBackColor = true;
            // 
            // progressBarDataLoad
            // 
            this.progressBarDataLoad.Location = new System.Drawing.Point(3, 364);
            this.progressBarDataLoad.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarDataLoad.Name = "progressBarDataLoad";
            this.progressBarDataLoad.Size = new System.Drawing.Size(632, 19);
            this.progressBarDataLoad.TabIndex = 9;
            this.progressBarDataLoad.Visible = false;
            // 
            // accidentsData
            // 
            this.accidentsData.AllowUserToAddRows = false;
            this.accidentsData.AllowUserToDeleteRows = false;
            this.accidentsData.AllowUserToResizeRows = false;
            this.accidentsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accidentsData.Location = new System.Drawing.Point(3, 3);
            this.accidentsData.Name = "accidentsData";
            this.accidentsData.ReadOnly = true;
            this.accidentsData.RowHeadersWidth = 51;
            this.accidentsData.Size = new System.Drawing.Size(632, 381);
            this.accidentsData.TabIndex = 8;
            // 
            // acceptFaultButton
            // 
            this.acceptFaultButton.Enabled = false;
            this.acceptFaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptFaultButton.Location = new System.Drawing.Point(680, 255);
            this.acceptFaultButton.Name = "acceptFaultButton";
            this.acceptFaultButton.Size = new System.Drawing.Size(110, 48);
            this.acceptFaultButton.TabIndex = 7;
            this.acceptFaultButton.Text = "Подтвердить";
            this.acceptFaultButton.UseVisualStyleBackColor = true;
            this.acceptFaultButton.Click += new System.EventHandler(this.acceptFaultButton_Click);
            // 
            // choosenAccidentLabel
            // 
            this.choosenAccidentLabel.AutoSize = true;
            this.choosenAccidentLabel.Location = new System.Drawing.Point(655, 91);
            this.choosenAccidentLabel.Name = "choosenAccidentLabel";
            this.choosenAccidentLabel.Size = new System.Drawing.Size(103, 13);
            this.choosenAccidentLabel.TabIndex = 5;
            this.choosenAccidentLabel.Text = "Выбранная авария";
            // 
            // selectAccident
            // 
            this.selectAccident.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectAccident.FormattingEnabled = true;
            this.selectAccident.Location = new System.Drawing.Point(658, 107);
            this.selectAccident.Name = "selectAccident";
            this.selectAccident.Size = new System.Drawing.Size(150, 21);
            this.selectAccident.TabIndex = 3;
            // 
            // findReliabIntervalTab
            // 
            this.findReliabIntervalTab.Controls.Add(this.progressBarReliability);
            this.findReliabIntervalTab.Controls.Add(this.dataSignalReliability);
            this.findReliabIntervalTab.Controls.Add(this.buttonCalcReliabilityInterval);
            this.findReliabIntervalTab.Controls.Add(this.numberOfStdForMaxLevel);
            this.findReliabIntervalTab.Controls.Add(this.labelNumberOfStdForMaxLevel);
            this.findReliabIntervalTab.Controls.Add(this.numberOfValuesForNormLevel);
            this.findReliabIntervalTab.Controls.Add(this.labelNumberOfValuesForNormLevel);
            this.findReliabIntervalTab.Controls.Add(this.indexOfMaxValue);
            this.findReliabIntervalTab.Controls.Add(this.labelIndexWithMaxValue);
            this.findReliabIntervalTab.Controls.Add(this.numberOfValuesInAcc);
            this.findReliabIntervalTab.Controls.Add(this.labelNumberOfValuesInAcc);
            this.findReliabIntervalTab.Location = new System.Drawing.Point(4, 22);
            this.findReliabIntervalTab.Name = "findReliabIntervalTab";
            this.findReliabIntervalTab.Padding = new System.Windows.Forms.Padding(3);
            this.findReliabIntervalTab.Size = new System.Drawing.Size(816, 390);
            this.findReliabIntervalTab.TabIndex = 1;
            this.findReliabIntervalTab.Text = "Расчет интервала надежности";
            this.findReliabIntervalTab.UseVisualStyleBackColor = true;
            // 
            // labelNumberOfValuesInAcc
            // 
            this.labelNumberOfValuesInAcc.AutoSize = true;
            this.labelNumberOfValuesInAcc.Location = new System.Drawing.Point(21, 23);
            this.labelNumberOfValuesInAcc.Name = "labelNumberOfValuesInAcc";
            this.labelNumberOfValuesInAcc.Size = new System.Drawing.Size(140, 26);
            this.labelNumberOfValuesInAcc.TabIndex = 19;
            this.labelNumberOfValuesInAcc.Text = "Количество наблюдений в\r\nвыбранной аварии:";
            // 
            // numberOfValuesInAcc
            // 
            this.numberOfValuesInAcc.Location = new System.Drawing.Point(175, 29);
            this.numberOfValuesInAcc.Name = "numberOfValuesInAcc";
            this.numberOfValuesInAcc.ReadOnly = true;
            this.numberOfValuesInAcc.Size = new System.Drawing.Size(100, 20);
            this.numberOfValuesInAcc.TabIndex = 20;
            // 
            // labelIndexWithMaxValue
            // 
            this.labelIndexWithMaxValue.AutoSize = true;
            this.labelIndexWithMaxValue.Location = new System.Drawing.Point(21, 79);
            this.labelIndexWithMaxValue.Name = "labelIndexWithMaxValue";
            this.labelIndexWithMaxValue.Size = new System.Drawing.Size(148, 26);
            this.labelIndexWithMaxValue.TabIndex = 21;
            this.labelIndexWithMaxValue.Text = "Индекс наблюдения с \r\nмаксимальным значением:";
            // 
            // indexOfMaxValue
            // 
            this.indexOfMaxValue.Location = new System.Drawing.Point(175, 85);
            this.indexOfMaxValue.Name = "indexOfMaxValue";
            this.indexOfMaxValue.ReadOnly = true;
            this.indexOfMaxValue.Size = new System.Drawing.Size(100, 20);
            this.indexOfMaxValue.TabIndex = 22;
            // 
            // labelNumberOfValuesForNormLevel
            // 
            this.labelNumberOfValuesForNormLevel.AutoSize = true;
            this.labelNumberOfValuesForNormLevel.Location = new System.Drawing.Point(21, 166);
            this.labelNumberOfValuesForNormLevel.Name = "labelNumberOfValuesForNormLevel";
            this.labelNumberOfValuesForNormLevel.Size = new System.Drawing.Size(199, 26);
            this.labelNumberOfValuesForNormLevel.TabIndex = 23;
            this.labelNumberOfValuesForNormLevel.Text = "Количество наблюдений для\r\nподсчета уровня нормальной работы:";
            // 
            // numberOfValuesForNormLevel
            // 
            this.numberOfValuesForNormLevel.Location = new System.Drawing.Point(273, 172);
            this.numberOfValuesForNormLevel.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfValuesForNormLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfValuesForNormLevel.Name = "numberOfValuesForNormLevel";
            this.numberOfValuesForNormLevel.Size = new System.Drawing.Size(120, 20);
            this.numberOfValuesForNormLevel.TabIndex = 24;
            this.numberOfValuesForNormLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfValuesForNormLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPressedOnlyNums);
            // 
            // labelNumberOfStdForMaxLevel
            // 
            this.labelNumberOfStdForMaxLevel.AutoSize = true;
            this.labelNumberOfStdForMaxLevel.Location = new System.Drawing.Point(21, 215);
            this.labelNumberOfStdForMaxLevel.Name = "labelNumberOfStdForMaxLevel";
            this.labelNumberOfStdForMaxLevel.Size = new System.Drawing.Size(246, 26);
            this.labelNumberOfStdForMaxLevel.TabIndex = 25;
            this.labelNumberOfStdForMaxLevel.Text = "Количество стандартных отклонения для\r\nподсчета максимального допустимого уровня:" +
    "";
            // 
            // numberOfStdForMaxLevel
            // 
            this.numberOfStdForMaxLevel.DecimalPlaces = 1;
            this.numberOfStdForMaxLevel.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numberOfStdForMaxLevel.Location = new System.Drawing.Point(273, 221);
            this.numberOfStdForMaxLevel.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfStdForMaxLevel.Name = "numberOfStdForMaxLevel";
            this.numberOfStdForMaxLevel.Size = new System.Drawing.Size(120, 20);
            this.numberOfStdForMaxLevel.TabIndex = 26;
            this.numberOfStdForMaxLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonCalcReliabilityInterval
            // 
            this.buttonCalcReliabilityInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalcReliabilityInterval.Location = new System.Drawing.Point(97, 310);
            this.buttonCalcReliabilityInterval.Name = "buttonCalcReliabilityInterval";
            this.buttonCalcReliabilityInterval.Size = new System.Drawing.Size(111, 44);
            this.buttonCalcReliabilityInterval.TabIndex = 27;
            this.buttonCalcReliabilityInterval.Text = "Рассчитать";
            this.buttonCalcReliabilityInterval.UseVisualStyleBackColor = true;
            this.buttonCalcReliabilityInterval.Click += new System.EventHandler(this.buttonCalcReliabilityInterval_Click);
            // 
            // dataSignalReliability
            // 
            this.dataSignalReliability.AllowUserToAddRows = false;
            this.dataSignalReliability.AllowUserToDeleteRows = false;
            this.dataSignalReliability.AllowUserToResizeRows = false;
            this.dataSignalReliability.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataSignalReliability.Location = new System.Drawing.Point(457, 23);
            this.dataSignalReliability.Name = "dataSignalReliability";
            this.dataSignalReliability.ReadOnly = true;
            this.dataSignalReliability.RowHeadersWidth = 51;
            this.dataSignalReliability.Size = new System.Drawing.Size(341, 353);
            this.dataSignalReliability.TabIndex = 28;
            // 
            // progressBarReliability
            // 
            this.progressBarReliability.Location = new System.Drawing.Point(457, 357);
            this.progressBarReliability.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarReliability.Name = "progressBarReliability";
            this.progressBarReliability.Size = new System.Drawing.Size(341, 19);
            this.progressBarReliability.TabIndex = 29;
            this.progressBarReliability.Visible = false;
            // 
            // researchParametersTab
            // 
            this.researchParametersTab.Controls.Add(this.listBox1);
            this.researchParametersTab.Location = new System.Drawing.Point(4, 22);
            this.researchParametersTab.Name = "researchParametersTab";
            this.researchParametersTab.Size = new System.Drawing.Size(816, 390);
            this.researchParametersTab.TabIndex = 2;
            this.researchParametersTab.Text = "Параметры исследования";
            this.researchParametersTab.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(27, 35);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(164, 251);
            this.listBox1.TabIndex = 0;
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 444);
            this.Controls.Add(this.allTabs);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(852, 483);
            this.Name = "MainFrom";
            this.Text = "DeepStatistic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrom_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.MainFrom_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainFrom_ResizeEnd);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.allTabs.ResumeLayout(false);
            this.loadDataTab.ResumeLayout(false);
            this.loadDataTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accidentsData)).EndInit();
            this.findReliabIntervalTab.ResumeLayout(false);
            this.findReliabIntervalTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfValuesForNormLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfStdForMaxLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSignalReliability)).EndInit();
            this.researchParametersTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem WorkFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripMenuItem ExitApp;
        private System.Windows.Forms.ToolStripMenuItem helpAllSteps;
        private System.Windows.Forms.TabControl allTabs;
        private System.Windows.Forms.TabPage loadDataTab;
        private System.Windows.Forms.TabPage findReliabIntervalTab;
        private System.Windows.Forms.Button acceptFaultButton;
        private System.Windows.Forms.Label choosenAccidentLabel;
        private System.Windows.Forms.ComboBox selectAccident;
        private System.Windows.Forms.ProgressBar progressBarDataLoad;
        private System.Windows.Forms.DataGridView accidentsData;
        private System.Windows.Forms.ProgressBar progressBarReliability;
        private System.Windows.Forms.DataGridView dataSignalReliability;
        private System.Windows.Forms.Button buttonCalcReliabilityInterval;
        private System.Windows.Forms.NumericUpDown numberOfStdForMaxLevel;
        private System.Windows.Forms.Label labelNumberOfStdForMaxLevel;
        private System.Windows.Forms.NumericUpDown numberOfValuesForNormLevel;
        private System.Windows.Forms.Label labelNumberOfValuesForNormLevel;
        private System.Windows.Forms.TextBox indexOfMaxValue;
        private System.Windows.Forms.Label labelIndexWithMaxValue;
        private System.Windows.Forms.TextBox numberOfValuesInAcc;
        private System.Windows.Forms.Label labelNumberOfValuesInAcc;
        private System.Windows.Forms.TabPage researchParametersTab;
        private System.Windows.Forms.ListBox listBox1;
    }
}

