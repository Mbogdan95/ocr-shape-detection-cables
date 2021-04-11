using System;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class AddNewUserForm : Form
    {
        private User currentUser;

        public AddNewUserForm(User currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            LoadRanks();
        }

        private void LoadRanks()
        {
            comboBoxRank.DataSource = Enum.GetValues(typeof(Rank));
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            string idBadge = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string repeatPassword = textBoxRepeatPassword.Text;

            Rank rank = (Rank)comboBoxRank.SelectedItem;

            if (idBadge == string.Empty)
            {
                MessageBox.Show("Username has not been entered", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (rank != Rank.Operator)
            {
                if (password == string.Empty)
                {
                    MessageBox.Show("Password has not been entered", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (repeatPassword == string.Empty)
                {
                    MessageBox.Show("Repeat password has not been entered", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (password != repeatPassword)
                {
                    MessageBox.Show("Passwords do not match", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            SqlCommunication.InsertUser(idBadge, password, rank.ToString());

            AddLog($"User Configuration: User {idBadge} added");

            UserConfigurationForm parent = (UserConfigurationForm)Owner;
            parent.LoadUsers();

            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ComboBoxRankIndexChanged(object sender, EventArgs e)
        {
            Rank rank = (Rank)comboBoxRank.SelectedItem;

            if (rank == Rank.Operator)
            {
                textBoxPassword.Enabled = false;
                textBoxRepeatPassword.Enabled = false;
            }
            else
            {
                textBoxPassword.Enabled = true;
                textBoxRepeatPassword.Enabled = true;
            }
        }

        private void AddLog(string action)
        {
            SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
        }
    }
}
