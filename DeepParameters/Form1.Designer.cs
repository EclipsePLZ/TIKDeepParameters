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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.selectAccident = new System.Windows.Forms.ComboBox();
            this.choosenAccidentLabel = new System.Windows.Forms.Label();
            this.acceptFaultButton = new System.Windows.Forms.Button();
            this.accidentsData = new System.Windows.Forms.DataGridView();
            this.progressBarDataLoad = new System.Windows.Forms.ProgressBar();
            this.menuStrip.SuspendLayout();
            this.allTabs.SuspendLayout();
            this.loadDataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accidentsData)).BeginInit();
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
            this.OpenFile.Size = new System.Drawing.Size(180, 22);
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
            this.allTabs.Controls.Add(this.tabPage2);
            this.allTabs.Location = new System.Drawing.Point(12, 27);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.Size = new System.Drawing.Size(824, 416);
            this.allTabs.TabIndex = 2;
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(816, 390);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // choosenAccidentLabel
            // 
            this.choosenAccidentLabel.AutoSize = true;
            this.choosenAccidentLabel.Location = new System.Drawing.Point(655, 91);
            this.choosenAccidentLabel.Name = "choosenAccidentLabel";
            this.choosenAccidentLabel.Size = new System.Drawing.Size(103, 13);
            this.choosenAccidentLabel.TabIndex = 5;
            this.choosenAccidentLabel.Text = "Выбранная авария";
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
            // progressBarDataLoad
            // 
            this.progressBarDataLoad.Location = new System.Drawing.Point(3, 364);
            this.progressBarDataLoad.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarDataLoad.Name = "progressBarDataLoad";
            this.progressBarDataLoad.Size = new System.Drawing.Size(632, 19);
            this.progressBarDataLoad.TabIndex = 9;
            this.progressBarDataLoad.Visible = false;
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
            this.Resize += new System.EventHandler(this.MainFrom_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.allTabs.ResumeLayout(false);
            this.loadDataTab.ResumeLayout(false);
            this.loadDataTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accidentsData)).EndInit();
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button acceptFaultButton;
        private System.Windows.Forms.Label choosenAccidentLabel;
        private System.Windows.Forms.ComboBox selectAccident;
        private System.Windows.Forms.ProgressBar progressBarDataLoad;
        private System.Windows.Forms.DataGridView accidentsData;
    }
}

