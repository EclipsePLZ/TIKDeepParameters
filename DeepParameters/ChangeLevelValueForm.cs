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
    public partial class ChangeLevelValueForm : Form {
        public int NumberOfValuesForLevel { get; set; }

        public int Level { get; set; }

        public ChangeLevelValueForm(int level, int numberOfValues) {
            InitializeComponent();

            this.CenterToScreen();

            Level = level;
            NumberOfValuesForLevel = numberOfValues;

            numberOfMaxDeepLevel.Maximum = Decimal.MaxValue;
            levelDepth.Text = Level.ToString();
            numberOfMaxDeepLevel.Value = NumberOfValuesForLevel;
        }

        /// <summary>
        /// Set new value for deep level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acceptNewValueForLevel_Click(object sender, EventArgs e) {
            NumberOfValuesForLevel = (int)numberOfMaxDeepLevel.Value;
            this.Close();
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
    }
}
