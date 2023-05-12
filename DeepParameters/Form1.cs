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
        }

        private void MainFrom_Resize(object sender, EventArgs e) {
            int newWidth = this.Width > 28 ? this.Width - 28 : 0;
            int newHeight = this.Height > 67 ? this.Height - 67 : 0;
            allTabs.Size = new Size(newWidth, newHeight);

            // tab1

        }
    }
}
