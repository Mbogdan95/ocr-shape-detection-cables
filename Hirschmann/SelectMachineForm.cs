using System;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class SelectMachineForm : Form
    {
        public string selectedMachine = "";
        public SelectMachineForm()
        {
            InitializeComponent();

            comboBoxMachineList.SelectedIndex = -1;
        }

        /// <summary>
        /// When Cancel button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            // Close form
            Close();
        }

        /// <summary>
        /// When Ok button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOkClick(object sender, EventArgs e)
        {
            if (comboBoxMachineList.SelectedIndex != -1)
            {
                selectedMachine = (string)comboBoxMachineList.SelectedItem;

                // Close form
                Close();
            }
            else
            {
                MessageBox.Show("Select a machine first", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
