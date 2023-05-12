using DeepParameters.Work_WIth_Files.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepParameters.Work_WIth_Files {
    internal class ExcelFileService : IFileService{
        /// <summary>
        /// Read accident information from excel file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Array of all accidents</returns>
        public List<List<string>> Open(string filePath, BackgroundWorker bgWorker) {
            // Excel connector
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = xlapp.Workbooks.Open(filePath);
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range range = sheet.UsedRange;

            List<List<string>> allRows = new List<List<string>>();
            try {
                int progress = 0;
                int step = range.Rows.Count / 100;
                int oneBarInProgress = 1;
                if (range.Rows.Count < 100) {
                    step = 1;
                    oneBarInProgress = (100 / range.Rows.Count) + 1;
                }
                bgWorker.ReportProgress(progress);

                for (int row = 1; row <= range.Rows.Count; row++) {
                    // Find progress
                    if (row % step == 0) {
                        progress += oneBarInProgress;
                        bgWorker.ReportProgress(progress);
                    }

                    // Ad values from excel sheet to List
                    List<string> rowValues = new List<string>();
                    for (int col = 1; col <= range.Columns.Count; col++) {
                        rowValues.Add(range.Cells[row, col].Text);
                    }
                    allRows.Add(rowValues);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            workbook.Close(true);
            xlapp.Quit();
            return allRows;
        }
    }
}
