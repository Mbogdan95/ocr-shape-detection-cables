using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class UserConfigurationForm : Form
    {
        public List<User> users = new List<User>();

        private User currentUser;

        public UserConfigurationForm(User currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            LoadUsers();

            AddLog("User Configuration: Access");
        }

        public void LoadUsers()
        {
            users = SqlCommunication.GetUsers();

            ShowUsers(users);
        }

        private void ShowUsers(List<User> users)
        {
            dataGridViewUsers.Rows.Clear();
            dataGridViewUsers.DataSource = null;

            foreach (User user in users)
            {
                dataGridViewUsers.Rows.Add(user.IdBadge, user.Rank.ToString());
            }
        }

        private void ButtonAddNewUserClick(object sender, EventArgs e)
        {
            using (AddNewUserForm addNewUserForm = new AddNewUserForm(currentUser))
            {
                addNewUserForm.ShowDialog(this);
            }
        }

        private void ButtonDeleteSelectedUserClick(object sender, EventArgs e)
        {
            if (dataGridViewUsers.CurrentRow != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete selected user?", "Delete user request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommunication.DeleteUser(dataGridViewUsers.CurrentRow.Cells[0].Value.ToString());

                    AddLog($"User Configuration: User {dataGridViewUsers.CurrentRow.Cells[0].Value.ToString()} deleted");

                    LoadUsers();
                }
            }
        }

        private void AddLog(string action)
        {
            SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
        }
    }
}
