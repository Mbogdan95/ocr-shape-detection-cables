using System;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class ProductConfigurationForm : Form
    {
        private int triggerOffset = 0;
        private int startWastingOffset = 0;
        private int wasteOffset = 0;

        private double logo1Tolerance = 0;
        private double logo2Tolerance = 0;

        private string machine = string.Empty;

        public ProductConfigurationForm()
        {
            InitializeComponent();

            comboBoxMachineList.Items.Add("KOMAX");
            comboBoxMachineList.Items.Add("METZNER");
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            triggerOffset = Convert.ToInt32(numericUpDownTriggerOffset.Value);
            startWastingOffset = Convert.ToInt32(numericUpDownStartWastingOffset.Value);
            wasteOffset = Convert.ToInt32(numericUpDownWasteOffset.Value);

            logo1Tolerance = Convert.ToDouble(numericUpDownLogo1Tolerance.Value / 100);
            logo2Tolerance = Convert.ToDouble(numericUpDownLogo2Tolerance.Value / 100);

            if (comboBoxMachineList.SelectedIndex != -1)
            {
                machine = comboBoxMachineList.SelectedItem.ToString();
            }

            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        public int GetTriggerOffset()
        {
            return triggerOffset;
        }

        public int GetStartWastingOffset()
        {
            return startWastingOffset;
        }

        public int GetWasteOffset()
        {
            return wasteOffset;
        }

        public string GetMachine()
        {
            return machine;
        }

        public double GetLogo1Tolerance()
        {
            return logo1Tolerance;
        }

        public double GetLogo2Tolerance()
        {
            return logo2Tolerance;
        }
    }
}
