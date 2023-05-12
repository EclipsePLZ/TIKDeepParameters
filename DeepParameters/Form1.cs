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
        public MainFrom() {
            InitializeComponent();

            // Centered Main From on the screen
            this.CenterToScreen();

            // Locks all tabs except the first one
            LocksAllTabs();
            loadDataTab.Enabled = true;
        }

        /// <summary>
        /// Locks all tabs in tab collection
        /// </summary>
        private void LocksAllTabs() {
            foreach (TabPage tab in allTabs.TabPages) {
                tab.Enabled = false;
            }
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
