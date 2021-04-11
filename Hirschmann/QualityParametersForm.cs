using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class QualityParametersForm : Form
    {
        private List<ProgramEntry> programs = new List<ProgramEntry>();

        private User currentUser;

        public QualityParametersForm(User currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            LoadPrograms();

            AddLog("Quality Parameters: Access");
        }

        public void LoadPrograms()
        {
            programs = SqlCommunication.GetPrograms();

            comboBoxPrograms.Items.Clear();
            comboBoxPrograms.DataSource = programs;
            comboBoxPrograms.DisplayMember = "name";
            comboBoxPrograms.SelectedIndex = -1;

            labelDescription.Text = string.Empty;
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void AddLog(string action)
        {
            SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
        }
    }
}
