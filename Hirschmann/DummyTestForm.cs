using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class DummyTestForm : Form
    {
        private List<ProgramEntry> programs = new List<ProgramEntry>();

        public ProgramEntry selectedProgram = null;

        private User currentUser;

        private PlcActions plcActions;

        #region Halcon Variables
        // Local iconic variables 
        private HObject hoImageCamera1 = new HObject();
        private HObject hoImageCamera2 = new HObject();

        // Local control variables 
        private HTuple hvAcqHandleCamera1 = null;
        private HTuple hvAcqHandleCamera2 = null;
        #endregion

        public DummyTestForm(User currentUser, PlcActions plcActions)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            this.plcActions = plcActions;

            LoadPrograms();

            UpdateUI();

            System.Threading.Timer stateTimer = new System.Threading.Timer(new TimerCallback(TickTimer), null, 1000, 1000);

            AddLog("Dummy testing: Access");
        }

        private void LoadPrograms()
        {
            programs = SqlCommunication.GetPrograms();

            comboBoxPrograms.Items.Clear();
            comboBoxPrograms.DataSource = programs;
            comboBoxPrograms.DisplayMember = "name";
            comboBoxPrograms.SelectedIndex = -1;

            labelDescription.Text = string.Empty;
        }

        private void UpdateUI()
        {
            plcActions.ReadInputs(out bool input1,
                out bool input2,
                out bool input3,
                out bool input4,
                out bool input5,
                out bool input6,
                out bool input7,
                out bool input8,
                out bool input9,
                out bool input10,
                out bool input11,
                out bool input12,
                out bool input13,
                out bool input14);

            plcActions.ReadOutputs(out bool output1,
                out bool output2,
                out bool output3,
                out bool output4,
                out bool output5,
                out bool output6,
                out bool output7,
                out bool output8,
                out bool output9,
                out bool output10,
                out bool output11,
                out bool output12,
                out bool output13,
                out bool output14);

            #region Update Inputs
            if (input1)
            {
                labelInput1On.BackColor = Color.LimeGreen;
                labelInput1Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput1On.BackColor = Color.DimGray;
                labelInput1Off.BackColor = Color.Crimson;
            }

            if (input2)
            {
                labelInput2On.BackColor = Color.LimeGreen;
                labelInput2Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput2On.BackColor = Color.DimGray;
                labelInput2Off.BackColor = Color.Crimson;
            }

            if (input3)
            {
                labelInput3On.BackColor = Color.LimeGreen;
                labelInput3Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput3On.BackColor = Color.DimGray;
                labelInput3Off.BackColor = Color.Crimson;
            }

            if (input4)
            {
                labelInput4On.BackColor = Color.LimeGreen;
                labelInput4Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput4On.BackColor = Color.DimGray;
                labelInput4Off.BackColor = Color.Crimson;
            }

            if (input5)
            {
                labelInput5On.BackColor = Color.LimeGreen;
                labelInput5Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput5On.BackColor = Color.DimGray;
                labelInput5Off.BackColor = Color.Crimson;
            }

            if (input6)
            {
                labelInput6On.BackColor = Color.LimeGreen;
                labelInput6Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput6On.BackColor = Color.DimGray;
                labelInput6Off.BackColor = Color.Crimson;
            }

            if (input7)
            {
                labelInput7On.BackColor = Color.LimeGreen;
                labelInput7Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput7On.BackColor = Color.DimGray;
                labelInput7Off.BackColor = Color.Crimson;
            }

            if (input8)
            {
                labelInput8On.BackColor = Color.LimeGreen;
                labelInput8Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput8On.BackColor = Color.DimGray;
                labelInput8Off.BackColor = Color.Crimson;
            }

            if (input9)
            {
                labelInput9On.BackColor = Color.LimeGreen;
                labelInput9Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput9On.BackColor = Color.DimGray;
                labelInput9Off.BackColor = Color.Crimson;
            }

            if (input10)
            {
                labelInput10On.BackColor = Color.LimeGreen;
                labelInput10Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput10On.BackColor = Color.DimGray;
                labelInput10Off.BackColor = Color.Crimson;
            }

            if (input11)
            {
                labelInput11On.BackColor = Color.LimeGreen;
                labelInput11Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput11On.BackColor = Color.DimGray;
                labelInput11Off.BackColor = Color.Crimson;
            }

            if (input12)
            {
                labelInput12On.BackColor = Color.LimeGreen;
                labelInput12Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput12On.BackColor = Color.DimGray;
                labelInput12Off.BackColor = Color.Crimson;
            }

            if (input13)
            {
                labelInput13On.BackColor = Color.LimeGreen;
                labelInput13Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput13On.BackColor = Color.DimGray;
                labelInput13Off.BackColor = Color.Crimson;
            }

            if (input14)
            {
                labelInput14On.BackColor = Color.LimeGreen;
                labelInput14Off.BackColor = Color.DimGray;
            }
            else
            {
                labelInput14On.BackColor = Color.DimGray;
                labelInput14Off.BackColor = Color.Crimson;
            }
            #endregion

            #region Update Outputs
            if (output1)
            {
                buttonOutput1On.BackColor = Color.LimeGreen;
                buttonOutput1Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput1On.BackColor = Color.DimGray;
                buttonOutput1Off.BackColor = Color.Crimson;
            }

            if (output2)
            {
                buttonOutput2On.BackColor = Color.LimeGreen;
                buttonOutput2Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput2On.BackColor = Color.DimGray;
                buttonOutput2Off.BackColor = Color.Crimson;
            }

            if (output3)
            {
                buttonOutput3On.BackColor = Color.LimeGreen;
                buttonOutput3Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput3On.BackColor = Color.DimGray;
                buttonOutput3Off.BackColor = Color.Crimson;
            }

            if (output4)
            {
                buttonOutput4On.BackColor = Color.LimeGreen;
                buttonOutput4Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput4On.BackColor = Color.DimGray;
                buttonOutput4Off.BackColor = Color.Crimson;
            }

            if (output5)
            {
                buttonOutput5On.BackColor = Color.LimeGreen;
                buttonOutput5Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput5On.BackColor = Color.DimGray;
                buttonOutput5Off.BackColor = Color.Crimson;
            }

            if (output6)
            {
                buttonOutput6On.BackColor = Color.LimeGreen;
                buttonOutput6Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput6On.BackColor = Color.DimGray;
                buttonOutput6Off.BackColor = Color.Crimson;
            }

            if (output7)
            {
                buttonOutput7On.BackColor = Color.LimeGreen;
                buttonOutput7Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput7On.BackColor = Color.DimGray;
                buttonOutput7Off.BackColor = Color.Crimson;
            }

            if (output8)
            {
                buttonOutput8On.BackColor = Color.LimeGreen;
                buttonOutput8Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput8On.BackColor = Color.DimGray;
                buttonOutput8Off.BackColor = Color.Crimson;
            }

            if (output9)
            {
                buttonOutput9On.BackColor = Color.LimeGreen;
                buttonOutput9Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput9On.BackColor = Color.DimGray;
                buttonOutput9Off.BackColor = Color.Crimson;
            }

            if (output10)
            {
                buttonOutput10On.BackColor = Color.LimeGreen;
                buttonOutput10Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput10On.BackColor = Color.DimGray;
                buttonOutput10Off.BackColor = Color.Crimson;
            }

            if (output11)
            {
                buttonOutput11On.BackColor = Color.LimeGreen;
                buttonOutput11Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput11On.BackColor = Color.DimGray;
                buttonOutput11Off.BackColor = Color.Crimson;
            }

            if (output12)
            {
                buttonOutput12On.BackColor = Color.LimeGreen;
                buttonOutput12Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput12On.BackColor = Color.DimGray;
                buttonOutput12Off.BackColor = Color.Crimson;
            }

            if (output13)
            {
                buttonOutput13On.BackColor = Color.LimeGreen;
                buttonOutput13Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput13On.BackColor = Color.DimGray;
                buttonOutput13Off.BackColor = Color.Crimson;
            }

            if (output14)
            {
                buttonOutput14On.BackColor = Color.LimeGreen;
                buttonOutput14Off.BackColor = Color.DimGray;
            }
            else
            {
                buttonOutput14On.BackColor = Color.DimGray;
                buttonOutput14Off.BackColor = Color.Crimson;
            }
            #endregion
        }

        private void ComboBoxProgramsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPrograms.SelectedIndex != -1)
            {
                ProgramEntry selectedProgram = (ProgramEntry)comboBoxPrograms.SelectedItem;

                this.selectedProgram = selectedProgram;

                labelDescription.Text = selectedProgram.Description;
            }
        }

        private void AddLog(string action)
        {
            SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
        }

        private void ButtonOutput1OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(36, 6, true);

            UpdateUI();
        }

        private void ButtonOutput1OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(36, 6, false);

            UpdateUI();
        }

        private void ButtonOutput2OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(36, 7, true);

            UpdateUI();
        }

        private void ButtonOutput2OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(36, 7, false);

            UpdateUI();
        }

        private void buttonOutput3OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 0, true);

            UpdateUI();
        }

        private void buttonOutput3Off_Click(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 0, false);

            UpdateUI();
        }

        private void ButtonOutput4OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 1, true);

            UpdateUI();
        }

        private void ButtonOutput4OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 1, false);

            UpdateUI();
        }

        private void ButtonOutput5OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 2, true);

            UpdateUI();
        }

        private void ButtonOutput5OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 2, false);

            UpdateUI();
        }

        private void ButtonOutput6OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 3, true);

            UpdateUI();
        }

        private void ButtonOutput6OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 3, false);

            UpdateUI();
        }

        private void ButtonOutput7OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 4, true);

            UpdateUI();
        }

        private void ButtonOutput7OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 4, false);

            UpdateUI();
        }

        private void ButtonOutput8OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 5, true);

            UpdateUI();
        }

        private void ButtonOutput8OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 5, false);

            UpdateUI();
        }

        private void ButtonOutput9OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 6, true);

            UpdateUI();
        }

        private void ButtonOutput9OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 6, false);

            UpdateUI();
        }

        private void ButtonOutput10OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 7, true);

            UpdateUI();
        }

        private void ButtonOutput10OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(37, 7, false);

            UpdateUI();
        }

        private void ButtonOutput11OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 0, true);

            UpdateUI();
        }

        private void ButtonOutput11OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 0, false);

            UpdateUI();
        }

        private void ButtonOutput12OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 1, true);

            UpdateUI();
        }

        private void ButtonOutput12OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 1, false);

            UpdateUI();
        }

        private void ButtonOutput13OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 2, true);

            UpdateUI();
        }

        private void ButtonOutput13OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 2, false);

            UpdateUI();
        }

        private void ButtonOutput14OnClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 3, true);

            UpdateUI();
        }

        private void ButtonOutput14OffClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 3, false);

            UpdateUI();
        }

        private void TickTimer(object state)
        {
            UpdateUI();
        }

        private void ButtonExecuteClick(object sender, EventArgs e)
        {
            plcActions.WriteOutput(38, 0, true);
            plcActions.WriteOutput(38, 1, true);

            UpdateUI();
        }
    }
}
