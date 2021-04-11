using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class ScanForm : Form
    {
        private bool closed = false;

        private string scanValue;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int ToUnicode(
            uint virtualKeyCode,
            uint scanCode,
            byte[] keyboardState,
            StringBuilder receivingBuffer,
            int bufferSize,
            uint flags);

        public ScanForm(string type)
        {
            InitializeComponent();

            SetDescription(type);

            KeyDown += new KeyEventHandler(ScanFormKeyDown);
        }

        private void SetDescription(string type)
        {
            if (type == "badge")
            {
                labelText.Text = "Please scan badge ID";
            }
            else if (type == "program")
            {
                labelText.Text = "Please scan program code";
            }
        }

        private void ScanFormKeyDown(object sender, KeyEventArgs e)
        {
            scanValue = string.Concat(scanValue, GetCharsFromKeys(e.KeyCode, false));
            e.Handled = false;
        }

        private string GetCharsFromKeys(Keys keys, bool shift)
        {
            var buf = new StringBuilder(256);
            var keyboardState = new byte[256];
            if (shift)
            {
                keyboardState[(int)Keys.ShiftKey] = 0xff;
            }
            ToUnicode((uint)keys, 0, keyboardState, buf, 256, 0);
            return buf.ToString();
        }

        public string GetScanValue()
        {
            return scanValue;
        }

        public bool GetClosedValue()
        {
            return closed;
        }

        private void ButtonCancelClick(object sender, System.EventArgs e)
        {
            if (scanValue == null)
            {
                closed = true;
            }

            Close();
        }

        public ProgramEntry CheckProgram()
        {
            ProgramEntry programEntry = SqlCommunication.CheckProgram(scanValue);

            return programEntry;
        }

        public User CheckUser()
        {
            User user = SqlCommunication.CheckUserProgram(scanValue);

            return user;
        }
    }
}
