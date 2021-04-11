using System;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class LogInForm : Form
    {
        private bool userValid;
        private bool userChecked;

        private User user;

        public LogInForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When Cancel button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            userChecked = false;

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
            // Get badge ID and password from textbox
            string idBadge = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (idBadge.Contains("700"))
            {
                user = new User()
                {
                    IdBadge = idBadge,
                    Rank = Rank.Operator
                };
            }
            else
            {
                // Check if user is in database
                user = SqlCommunication.CheckUser(idBadge, password);
            }

            // Check if user has been found
            if (user.IdBadge == null)
            {
                userValid = false;
            }
            else
            {
                userValid = true;
            }

            userChecked = true;

            // Close form
            Close();
        }

        private void ButtonScanClick(object sender, EventArgs e)
        {
            using (ScanForm scanForm = new ScanForm("badge"))
            {
                scanForm.ShowDialog(this);
                textBoxUsername.Text = scanForm.GetScanValue();
            }
        }

        /// <summary>
        /// Get value of checkUser
        /// </summary>
        /// <returns>Whether user has been found or not</returns>
        public bool GetUserValid()
        {
            return userValid;
        }


        public bool GetUserChecked()
        {
            return userChecked;
        }

        /// <summary>
        /// Get value of user
        /// </summary>
        /// <returns>User</returns>
        public User GetUser()
        {
            return user;
        }

    }
}
