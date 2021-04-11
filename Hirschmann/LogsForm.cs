using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class LogsForm : Form
    {
        private List<LogEntry> logEntries = new List<LogEntry>();

        public LogsForm()
        {
            InitializeComponent();

            LoadLogs();
        }

        private void LoadLogs()
        {
            logEntries = SqlCommunication.GetLogs();

            ShowLogs();
        }

        private void ShowLogs()
        {
            dataGridViewLogs.Rows.Clear();
            dataGridViewLogs.DataSource = null;

            foreach (LogEntry logEntry in logEntries)
            {
                dataGridViewLogs.Rows.Add(logEntry.IdBadge, logEntry.Action, logEntry.Date);
            }
        }

        private void DataGridViewLogsSelectionChanged(object sender, EventArgs e)
        {
            string photoLocationCamera1 = logEntries.Find(x => x.Date == Convert.ToDateTime(dataGridViewLogs.CurrentRow.Cells[2].Value)).PhotoLocationCamera1;
            string photoLocationCamera2 = logEntries.Find(x => x.Date == Convert.ToDateTime(dataGridViewLogs.CurrentRow.Cells[2].Value)).PhotoLocationCamera2;

            if (photoLocationCamera1 == string.Empty)
            {
                pictureBoxCamera1.Image = null;
            }
            else
            {
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), photoLocationCamera1)))
                {
                    pictureBoxCamera1.Image = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), photoLocationCamera1));
                }
                else
                {
                    MessageBox.Show("Camera 1: Evidence does not exist", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (photoLocationCamera2 == string.Empty)
            {
                pictureBoxCamera2.Image = null;
            }
            else
            {
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), photoLocationCamera2)))
                {
                    pictureBoxCamera2.Image = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), photoLocationCamera2));
                }
                else
                {
                    MessageBox.Show("Camera 2: Evidence does not exist", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonExportClick(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridViewLogs.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to export selected log rows?", "Export request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("No logs selected. Export everything?", "Export request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                }
            }
        }
    }
}
