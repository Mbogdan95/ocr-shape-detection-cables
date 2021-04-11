using System;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class CreateNewProgramForm : Form
    {
        private ManageProgramsForm manageProgramsForm;

        public CreateNewProgramForm(ManageProgramsForm manageProgramsForm)
        {
            InitializeComponent();

            this.manageProgramsForm = manageProgramsForm;
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            string programName = textBoxProgramName.Text.Replace(' ', '_');

            if (programName == string.Empty)
            {
                MessageBox.Show("Program name has not been entered", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProgramEntry programToCheck = SqlCommunication.CheckProgram(programName);

            if (programToCheck.Name == null)
            {
                manageProgramsForm.AddProgramToComboBox(programName);

                Close();
            }
            else
            {
                MessageBox.Show("Program already exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
