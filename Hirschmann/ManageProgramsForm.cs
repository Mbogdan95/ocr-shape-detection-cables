using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class ManageProgramsForm : Form
    {
        private MainForm mainForm;

        private List<ProgramEntry> listOfPrograms = new List<ProgramEntry>();

        private User currentUser;

        #region Halcon Variables
        // Local iconic variables 
        private HObject hoImageCamera1 = new HObject();
        private HObject hoImageCamera1Rotated = new HObject();
        private HObject hoImageCamera2 = new HObject();
        private HObject hoImageCamera2Rotated = new HObject();
        #endregion

        private double[] logo1Camera1RegionCoords = new double[] { 0, 0, 0, 0 };
        private double[] logo2Camera1RegionCoords = new double[] { 0, 0, 0, 0 };
        private double[] logo1Camera2RegionCoords = new double[] { 0, 0, 0, 0 };
        private double[] logo2Camera2RegionCoords = new double[] { 0, 0, 0, 0 };

        private int triggerOffset = 0;
        private int startWastingOffset = 0;
        private int wasteOffset = 0;

        private double logo1Tolerance = 0;
        private double logo2Tolerance = 0;

        private string machine = string.Empty;

        public ManageProgramsForm(MainForm mainForm, User currentUser)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            this.currentUser = currentUser;

            AddLog($"Manage Programs: Access");
        }

        #region Private Functions
        private void ManageProgramsFormLoad(object sender, EventArgs e)
        {
            panelColorCamera1.Enabled = false;
            panelColorCamera2.Enabled = false;
            panelLogoCamera1.Enabled = false;
            panelLogoCamera2.Enabled = false;
            panelShapeCamera1.Enabled = false;
            panelShapeCamera2.Enabled = false;
            panelOcrCamera1.Enabled = false;
            panelOcrCamera2.Enabled = false;

            numericUpDownLogosCamera1.Enabled = false;
            numericUpDownLogosCamera2.Enabled = false;

            comboBoxShapeTypeCamera1.Enabled = false;
            comboBoxShapeTypeCamera2.Enabled = false;

            textBoxMeasureDistanceHeightCamera1.Enabled = false;
            textBoxMeasureDistanceHeightCamera2.Enabled = false;
            textBoxMeasureDistanceToleranceCamera1.Enabled = false;
            textBoxMeasureDistanceToleranceCamera2.Enabled = false;
            textBoxMeasureDIstanceWidthCamera1.Enabled = false;
            textBoxMeasureDIstanceWidthCamera2.Enabled = false;

            buttonLogo1RoiCamera1.Enabled = false;
            buttonLogo1RoiCamera2.Enabled = false;
            buttonLogo2RoiCamera1.Enabled = false;
            buttonLogo2RoiCamera2.Enabled = false;

            LoadColors();
            LoadShapes();
            LoadPrograms();
        }

        private void LoadColors()
        {
            comboBoxColorsCamera1.Items.Add("YELLOW");
            comboBoxColorsCamera1.Items.Add("GREEN");
            comboBoxColorsCamera1.Items.Add("WHITE");
            comboBoxColorsCamera1.Items.Add("GRAY");
            comboBoxColorsCamera1.SelectedIndex = -1;

            comboBoxColorsCamera2.Items.Add("YELLOW");
            comboBoxColorsCamera2.Items.Add("GREEN");
            comboBoxColorsCamera2.Items.Add("WHITE");
            comboBoxColorsCamera2.Items.Add("GRAY");
            comboBoxColorsCamera2.SelectedIndex = -1;
        }

        private void LoadShapes()
        {
            comboBoxShapeTypeCamera1.Items.Add("RECTANGLE");
            comboBoxShapeTypeCamera1.Items.Add("ARROW");

            comboBoxShapeTypeCamera2.Items.Add("RECTANGLE");
            comboBoxShapeTypeCamera2.Items.Add("ARROW");
        }

        private void LoadPrograms()
        {
            listOfPrograms = SqlCommunication.GetPrograms();

            comboBoxPrograms.Items.Clear();

            foreach (var item in listOfPrograms)
            {
                comboBoxPrograms.Items.Add(item.Name);
            }

            comboBoxPrograms.SelectedIndex = -1;
        }

        private void ButtonRefreshImagesClick(object sender, EventArgs e)
        {
            if (panelLogoCamera1.Enabled)
            {
                if (numericUpDownLogosCamera1.Value == 1)
                {
                    if (!buttonLogo1RoiCamera1.Enabled)
                    {
                        buttonLogo1RoiCamera1.Enabled = true;

                        logo1Camera1RegionCoords = new double[] { 0, 0, 0, 0 };
                    }
                }
                else if (numericUpDownLogosCamera1.Value == 2)
                {
                    if (!buttonLogo1RoiCamera1.Enabled)
                    {
                        buttonLogo1RoiCamera1.Enabled = true;

                        logo1Camera1RegionCoords = new double[] { 0, 0, 0, 0 };
                    }

                    if (!buttonLogo2RoiCamera1.Enabled)
                    {
                        buttonLogo2RoiCamera1.Enabled = true;

                        logo2Camera1RegionCoords = new double[] { 0, 0, 0, 0 };
                    }
                }
            }

            if (panelLogoCamera2.Enabled)
            {
                if (numericUpDownLogosCamera2.Value == 1)
                {
                    if (!buttonLogo1RoiCamera2.Enabled)
                    {
                        buttonLogo1RoiCamera2.Enabled = true;

                        logo1Camera2RegionCoords = new double[] { 0, 0, 0, 0 };
                    }
                }
                else if (numericUpDownLogosCamera2.Value == 2)
                {
                    if (!buttonLogo1RoiCamera2.Enabled)
                    {
                        buttonLogo1RoiCamera2.Enabled = true;

                        logo1Camera2RegionCoords = new double[] { 0, 0, 0, 0 };
                    }

                    if (!buttonLogo2RoiCamera2.Enabled)
                    {
                        buttonLogo2RoiCamera2.Enabled = true;

                        logo2Camera2RegionCoords = new double[] { 0, 0, 0, 0 };
                    }
                }
            }

            if (mainForm.hvAcqHandleCamera1 != null)
            {
                try
                {
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera1, "TriggerSource", "Software");
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera1, "TriggerSoftware", 1);
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera1, "TriggerSource", "Line2");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Trigger camera 1: " + ex.Message);
                }
            }

            if (mainForm.hvAcqHandleCamera2 != null)
            {
                try
                {
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera2, "TriggerSource", "Software");
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera2, "TriggerSoftware", 1);
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera2, "TriggerSource", "Line2");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Trigger camera 2: " + ex.Message);
                }
            }
        }

        private void ButtonCreateNewProgramClick(object sender, EventArgs e)
        {
            using (CreateNewProgramForm createNewProgramForm = new CreateNewProgramForm(this))
            {
                createNewProgramForm.ShowDialog(this);
            }
        }

        private void ButtonDeleteSelectedProgramClick(object sender, EventArgs e)
        {
            if (comboBoxPrograms.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete selected program?", "Delete user request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommunication.DeleteProgram(comboBoxPrograms.SelectedItem.ToString());

                    LoadPrograms();
                }
            }
        }

        private void ButtonProductSettingsClick(object sender, EventArgs e)
        {
            using (ProductConfigurationForm productConfigurationForm = new ProductConfigurationForm())
            {
                productConfigurationForm.ShowDialog(this);

                triggerOffset = productConfigurationForm.GetTriggerOffset();
                startWastingOffset = productConfigurationForm.GetStartWastingOffset();
                wasteOffset = productConfigurationForm.GetWasteOffset();

                logo1Tolerance = productConfigurationForm.GetLogo1Tolerance();
                logo2Tolerance = productConfigurationForm.GetLogo2Tolerance();

                machine = productConfigurationForm.GetMachine();
            }
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            ProgramEntry programToCheck;

            string shapeTypeCamera1 = string.Empty;
            string shapeTypeCamera2 = string.Empty;

            if (comboBoxPrograms.SelectedIndex != -1)
            {
                programToCheck = SqlCommunication.CheckProgram(comboBoxPrograms.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("No program selected", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (startWastingOffset == 0 || startWastingOffset == 0 || wasteOffset == 0 || logo1Tolerance == 0 || logo2Tolerance == 0 || machine == string.Empty)
            {
                MessageBox.Show("Product configuration values not set", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + $"\\Programs\\{comboBoxPrograms.SelectedItem}"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + $"\\Programs\\{comboBoxPrograms.SelectedItem}");
            }

            if (!checkBoxCamera1.Checked && !checkBoxCamera2.Checked)
            {
                MessageBox.Show("At least one camera must be activated", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBoxCamera1.Checked)
            {
                if (comboBoxColorsCamera1.SelectedIndex == -1)
                {
                    MessageBox.Show("No color selected for camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxLogoCamera1.Checked)
                {
                    if (numericUpDownLogosCamera1.Value == 0)
                    {
                        MessageBox.Show("Camera 1 logos activated but no logo set", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (numericUpDownLogosCamera1.Value == 1)
                    {
                        if (logo1Camera1RegionCoords[0] == 0 && logo1Camera1RegionCoords[1] == 0 && logo1Camera1RegionCoords[2] == 0 && logo1Camera1RegionCoords[3] == 0)
                        {
                            MessageBox.Show("Logo 1 region not set for camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (numericUpDownLogosCamera1.Value == 2)
                    {
                        if (logo1Camera1RegionCoords[0] == 0 && logo1Camera1RegionCoords[1] == 0 && logo1Camera1RegionCoords[2] == 0 && logo1Camera1RegionCoords[3] == 0)
                        {
                            MessageBox.Show("Logo 1 region not set for camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (logo2Camera1RegionCoords[0] == 0 && logo2Camera1RegionCoords[1] == 0 && logo2Camera1RegionCoords[2] == 0 && logo2Camera1RegionCoords[3] == 0)
                        {
                            MessageBox.Show("Logo 2 region not set for camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera1"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera1");
                    }
                    HOperatorSet.RotateImage(hoImageCamera1Rotated, out HObject hImageRotatedCamera1, -90, "constant");

                    HalconProcedures.CreateLogos(hImageRotatedCamera1,
                        Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera1\Logo1.shm",
                        Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera1\Logo2.shm",
                        comboBoxColorsCamera1.SelectedItem.ToString().ToLower(),
                        Convert.ToInt32(numericUpDownLogosCamera1.Value),
                        logo1Camera1RegionCoords[0],
                        logo1Camera1RegionCoords[1],
                        logo1Camera1RegionCoords[2],
                        logo1Camera1RegionCoords[3],
                        logo2Camera1RegionCoords[0],
                        logo2Camera1RegionCoords[1],
                        logo2Camera1RegionCoords[2],
                        logo2Camera1RegionCoords[3]);
                }

                if (checkBoxShapeCamera1.Checked)
                {
                    if (comboBoxShapeTypeCamera1.SelectedIndex == -1)
                    {
                        MessageBox.Show("No shape type selected for camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        shapeTypeCamera1 = comboBoxShapeTypeCamera1.SelectedItem.ToString();
                    }
                }

                if (textBoxOcrCamera1.Text == string.Empty)
                {
                    MessageBox.Show("No OCR text for camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (checkBoxCamera2.Checked)
            {
                if (comboBoxColorsCamera2.SelectedIndex == -1)
                {
                    MessageBox.Show("No color selected for camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxLogoCamera2.Checked)
                {
                    if (numericUpDownLogosCamera2.Value == 0)
                    {
                        MessageBox.Show("Camera 2 logos activated but no logo set", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (numericUpDownLogosCamera2.Value == 1)
                    {
                        if (logo1Camera2RegionCoords[0] == 0 && logo1Camera2RegionCoords[1] == 0 && logo1Camera2RegionCoords[2] == 0 && logo1Camera2RegionCoords[3] == 0)
                        {
                            MessageBox.Show("Logo 1 region not set for camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (numericUpDownLogosCamera2.Value == 2)
                    {
                        if (logo1Camera2RegionCoords[0] == 0 && logo1Camera2RegionCoords[1] == 0 && logo1Camera2RegionCoords[2] == 0 && logo1Camera2RegionCoords[3] == 0)
                        {
                            MessageBox.Show("Logo 1 region not set for camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (logo2Camera2RegionCoords[0] == 0 && logo2Camera2RegionCoords[1] == 0 && logo2Camera2RegionCoords[2] == 0 && logo2Camera2RegionCoords[3] == 0)
                        {
                            MessageBox.Show("Logo 2 region not set for camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera2"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera2");
                    }

                    HOperatorSet.RotateImage(hoImageCamera2Rotated, out HObject hImageRotatedCamera2, -90, "constant");

                    HalconProcedures.CreateLogos(hImageRotatedCamera2,
                        Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera2\Logo1.shm",
                        Directory.GetCurrentDirectory() + $@"\Programs\{comboBoxPrograms.SelectedItem}\Camera2\Logo2.shm",
                        comboBoxColorsCamera2.SelectedItem.ToString().ToLower(),
                        Convert.ToInt32(numericUpDownLogosCamera2.Value),
                        logo1Camera2RegionCoords[0],
                        logo1Camera2RegionCoords[1],
                        logo1Camera2RegionCoords[2],
                        logo1Camera2RegionCoords[3],
                        logo2Camera2RegionCoords[0],
                        logo2Camera2RegionCoords[1],
                        logo2Camera2RegionCoords[2],
                        logo2Camera2RegionCoords[3]);
                }

                if (checkBoxShapeCamera2.Checked)
                {
                    if (comboBoxShapeTypeCamera2.SelectedIndex == -1)
                    {
                        MessageBox.Show("No shape type selected for camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        shapeTypeCamera2 = comboBoxShapeTypeCamera2.SelectedItem.ToString();
                    }
                }

                if (textBoxOcrCamera2.Text == string.Empty)
                {
                    MessageBox.Show("No OCR text for camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (programToCheck.Name == null)
            {
                string colorCamera1 = string.Empty;
                string colorCamera2 = string.Empty;

                if (comboBoxColorsCamera1.SelectedIndex != -1)
                {
                    colorCamera1 = comboBoxColorsCamera1.SelectedItem.ToString().ToLower();
                }

                if (comboBoxColorsCamera2.SelectedIndex != -1)
                {
                    colorCamera2 = comboBoxColorsCamera2.SelectedItem.ToString().ToLower();
                }

                SqlCommunication.InsertProgram(comboBoxPrograms.SelectedItem.ToString(),
                    triggerOffset,
                    startWastingOffset,
                    wasteOffset,
                    colorCamera1,
                    colorCamera2,
                    checkBoxCamera1.Checked,
                    checkBoxCamera2.Checked,
                    checkBoxLogoCamera1.Checked,
                    checkBoxLogoCamera2.Checked,
                    Convert.ToInt32(numericUpDownLogosCamera1.Value),
                    Convert.ToInt32(numericUpDownLogosCamera2.Value),
                    $"C:\\Hirschmann\\Hirschmann\\bin\\Debug\\Programs\\{comboBoxPrograms.SelectedItem}\\Camera1\\Logo1.shm".Replace('\\', ' '),
                    $"C:\\Hirschmann\\Hirschmann\\bin\\Debug\\Programs\\{comboBoxPrograms.SelectedItem}\\Camera1\\Logo2.shm".Replace('\\', ' '),
                    $"C:\\Hirschmann\\Hirschmann\\bin\\Debug\\Programs\\{comboBoxPrograms.SelectedItem}\\Camera2\\Logo1.shm".Replace('\\', ' '),
                    $"C:\\Hirschmann\\Hirschmann\\bin\\Debug\\Programs\\{comboBoxPrograms.SelectedItem}\\Camera2\\Logo2.shm".Replace('\\', ' '),
                    checkBoxShapeCamera1.Checked,
                    checkBoxShapeCamera2.Checked,
                    shapeTypeCamera1.ToLower(),
                    shapeTypeCamera2.ToLower(),
                    textBoxOcrCamera1.Text.Replace(" ", string.Empty),
                    textBoxOcrCamera2.Text.Replace(" ", string.Empty)
                    );

                AddLog($"Manage Programs: Program {comboBoxPrograms.SelectedItem.ToString()} created");
            }
            else
            {
                string colorCamera1 = string.Empty;
                string colorCamera2 = string.Empty;

                if (comboBoxColorsCamera1.SelectedIndex != -1)
                {
                    colorCamera1 = comboBoxColorsCamera1.SelectedItem.ToString().ToLower();
                }

                if (comboBoxColorsCamera2.SelectedIndex != -1)
                {
                    colorCamera2 = comboBoxColorsCamera2.SelectedItem.ToString().ToLower();
                }

                SqlCommunication.UpdateProgramManageProgramsForm(comboBoxPrograms.SelectedItem.ToString(),
                    triggerOffset,
                    startWastingOffset,
                    wasteOffset,
                    colorCamera1,
                    colorCamera2,
                    checkBoxCamera1.Checked,
                    checkBoxCamera2.Checked,
                    checkBoxLogoCamera1.Checked,
                    checkBoxLogoCamera2.Checked,
                    Convert.ToInt32(numericUpDownLogosCamera1.Value),
                    Convert.ToInt32(numericUpDownLogosCamera2.Value),
                    checkBoxShapeCamera1.Checked,
                    checkBoxShapeCamera2.Checked,
                    shapeTypeCamera1.ToLower(),
                    shapeTypeCamera2.ToLower(),
                    textBoxOcrCamera1.Text.Replace(" ", string.Empty),
                    textBoxOcrCamera2.Text.Replace(" ", string.Empty)
                    );

                AddLog($"Manage Programs: Program {comboBoxPrograms.SelectedItem.ToString()} updated");
            }

            Close();
        }

        private void CheckBoxCamera1CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCamera1.Checked)
            {
                panelColorCamera1.Enabled = true;
                panelLogoCamera1.Enabled = true;
                panelShapeCamera1.Enabled = true;
                panelOcrCamera1.Enabled = true;
            }
            else
            {
                panelColorCamera1.Enabled = false;
                panelLogoCamera1.Enabled = false;
                panelShapeCamera1.Enabled = false;
                panelOcrCamera1.Enabled = false;
            }
        }

        private void CheckBoxCamera2CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCamera2.Checked)
            {
                panelColorCamera2.Enabled = true;
                panelLogoCamera2.Enabled = true;
                panelShapeCamera2.Enabled = true;
                panelOcrCamera2.Enabled = true;
            }
            else
            {
                panelColorCamera2.Enabled = false;
                panelLogoCamera2.Enabled = false;
                panelShapeCamera2.Enabled = false;
                panelOcrCamera2.Enabled = false;
            }
        }

        private void ButtonDetectColorCamera1Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.RotateImage(hoImageCamera1Rotated, out HObject hImageRotated, -90, "constant");

                string colorDetected = HalconProcedures.CheckColor(hImageRotated);

                comboBoxColorsCamera1.SelectedIndex = comboBoxColorsCamera1.FindStringExact(colorDetected.ToUpper());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonDetectColorCamera2Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.RotateImage(hoImageCamera2Rotated, out HObject hImageRotated, -90, "constant");

                string colorDetected = HalconProcedures.CheckColor(hImageRotated);

                comboBoxColorsCamera2.SelectedIndex = comboBoxColorsCamera2.FindStringExact(colorDetected.ToUpper());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckBoxLogoCamera1CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLogoCamera1.Checked)
            {
                panelShapeCamera1.Enabled = false;

                numericUpDownLogosCamera1.Enabled = true;
            }
            else
            {
                panelShapeCamera1.Enabled = true;

                numericUpDownLogosCamera1.Enabled = false;
            }
        }

        private void CheckBoxLogoCamera2CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLogoCamera2.Checked)
            {
                panelShapeCamera2.Enabled = false;

                numericUpDownLogosCamera2.Enabled = true;
            }
            else
            {
                panelShapeCamera2.Enabled = true;

                numericUpDownLogosCamera2.Enabled = false;
            }
        }

        private void NumericUpDownLogosCamera1ValueChanged(object sender, EventArgs e)
        {
            logo1Camera1RegionCoords = new double[] { 0, 0, 0, 0 };
            logo2Camera1RegionCoords = new double[] { 0, 0, 0, 0 };

            if (numericUpDownLogosCamera1.Value == 0)
            {
                buttonLogo1RoiCamera1.Enabled = false;
                buttonLogo2RoiCamera1.Enabled = false;
            }
            else if (numericUpDownLogosCamera1.Value == 1)
            {
                buttonLogo1RoiCamera1.Enabled = true;
                buttonLogo2RoiCamera1.Enabled = false;
            }
            else if (numericUpDownLogosCamera1.Value == 2)
            {
                buttonLogo1RoiCamera1.Enabled = true;
                buttonLogo2RoiCamera1.Enabled = true;
            }

            try
            {
                hWindowControlCamera1.HalconWindow.ClearWindow();
                hWindowControlCamera1.HalconWindow.DispObj(hoImageCamera1Rotated);
            }
            catch (Exception)
            {
            }
        }

        private void NumericUpDownLogosCamera2ValueChanged(object sender, EventArgs e)
        {
            logo1Camera2RegionCoords = new double[] { 0, 0, 0, 0 };
            logo2Camera2RegionCoords = new double[] { 0, 0, 0, 0 };

            if (numericUpDownLogosCamera2.Value == 0)
            {
                buttonLogo1RoiCamera2.Enabled = false;
                buttonLogo2RoiCamera2.Enabled = false;
            }
            else if (numericUpDownLogosCamera2.Value == 1)
            {
                buttonLogo1RoiCamera2.Enabled = true;
                buttonLogo2RoiCamera2.Enabled = false;
            }
            else if (numericUpDownLogosCamera2.Value == 2)
            {
                buttonLogo1RoiCamera2.Enabled = true;
                buttonLogo2RoiCamera2.Enabled = true;
            }

            try
            {
                hWindowControlCamera2.HalconWindow.ClearWindow();
                hWindowControlCamera2.HalconWindow.DispObj(hoImageCamera2Rotated);
            }
            catch (Exception)
            {
            }
        }

        private void ButtonLogo1RoiCamera1Click(object sender, EventArgs e)
        {
            HWindow hWindow = hWindowControlCamera1.HalconWindow;

            hWindow.SetColor("red");
            hWindow.DrawRectangle1(out double row1, out double column1, out double row2, out double column2);

            hWindow.SetColor("green");
            hWindow.SetDraw("margin");
            hWindow.DispRectangle1(row1, column1, row2, column2);

            logo1Camera1RegionCoords = new double[] { row1, column1, row2, column2 };

            buttonLogo1RoiCamera1.Enabled = false;
        }

        private void ButtonLogo2RoiCamera1Click(object sender, EventArgs e)
        {
            HWindow hWindow = hWindowControlCamera1.HalconWindow;

            hWindow.SetColor("red");
            hWindow.DrawRectangle1(out double row1, out double column1, out double row2, out double column2);

            hWindow.SetColor("green");
            hWindow.SetDraw("margin");
            hWindow.DispRectangle1(row1, column1, row2, column2);

            logo2Camera1RegionCoords = new double[] { row1, column1, row2, column2 };

            buttonLogo2RoiCamera1.Enabled = false;
        }

        private void ButtonLogo1RoiCamera2Click(object sender, EventArgs e)
        {
            HWindow hWindow = hWindowControlCamera2.HalconWindow;

            hWindow.SetColor("red");
            hWindow.DrawRectangle1(out double row1, out double column1, out double row2, out double column2);

            hWindow.SetColor("green");
            hWindow.SetDraw("margin");
            hWindow.DispRectangle1(row1, column1, row2, column2);

            logo1Camera2RegionCoords = new double[] { row1, column1, row2, column2 };

            buttonLogo1RoiCamera2.Enabled = false;
        }

        private void ButtonLogo2RoiCamera2Click(object sender, EventArgs e)
        {
            HWindow hWindow = hWindowControlCamera2.HalconWindow;

            hWindow.SetColor("red");
            hWindow.DrawRectangle1(out double row1, out double column1, out double row2, out double column2);

            hWindow.SetColor("green");
            hWindow.SetDraw("margin");
            hWindow.DispRectangle1(row1, column1, row2, column2);

            logo2Camera2RegionCoords = new double[] { row1, column1, row2, column2 };

            buttonLogo2RoiCamera2.Enabled = false;
        }

        private void CheckBoxShapeCamera1CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShapeCamera1.Checked)
            {
                panelLogoCamera1.Enabled = false;

                comboBoxShapeTypeCamera1.Enabled = true;
            }
            else
            {
                panelLogoCamera1.Enabled = true;

                comboBoxShapeTypeCamera1.Enabled = false;
            }
        }

        private void CheckBoxShapeCamera2CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShapeCamera2.Checked)
            {
                panelLogoCamera2.Enabled = false;

                comboBoxShapeTypeCamera2.Enabled = true;
            }
            else
            {
                panelLogoCamera2.Enabled = true;

                comboBoxShapeTypeCamera2.Enabled = false;
            }
        }

        private void CheckBoxMeasureDistanceCamera1CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMeasureDistanceCamera1.Checked)
            {
                textBoxMeasureDistanceHeightCamera1.Enabled = true;
                textBoxMeasureDistanceToleranceCamera1.Enabled = true;
                textBoxMeasureDIstanceWidthCamera1.Enabled = true;
            }
            else
            {
                textBoxMeasureDistanceHeightCamera1.Enabled = false;
                textBoxMeasureDistanceToleranceCamera1.Enabled = false;
                textBoxMeasureDIstanceWidthCamera1.Enabled = false;
            }

        }

        private void CheckBoxMeasureDistanceCamera2CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMeasureDistanceCamera2.Checked)
            {
                textBoxMeasureDistanceHeightCamera2.Enabled = true;
                textBoxMeasureDistanceToleranceCamera2.Enabled = true;
                textBoxMeasureDIstanceWidthCamera2.Enabled = true;
            }
            else
            {
                textBoxMeasureDistanceHeightCamera2.Enabled = false;
                textBoxMeasureDistanceToleranceCamera2.Enabled = false;
                textBoxMeasureDIstanceWidthCamera2.Enabled = false;
            }
        }

        private void ComboBoxProgramsSelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramEntry programToCheck;

            if (comboBoxPrograms.SelectedIndex != -1)
            {
                programToCheck = SqlCommunication.CheckProgram(comboBoxPrograms.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("No program selected", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (programToCheck.Name != null)
            {
                triggerOffset = programToCheck.TriggerOffset;
                startWastingOffset = programToCheck.StartWastingOffset;
                wasteOffset = programToCheck.WasteOffset;

                logo1Tolerance = 1 - programToCheck.Logo1Camera1Confidence;
                logo2Tolerance = 1 - programToCheck.Logo1Camera2Confidence;

                machine = programToCheck.Machine;
            }
        }

        private void AddLog(string action)
        {
            SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
        }
        #endregion

        #region Public Functions
        public void AddProgramToComboBox(string programName)
        {
            comboBoxPrograms.Items.Add(programName);
            comboBoxPrograms.SelectedIndex = comboBoxPrograms.Items.Count - 1;
        }

        public void UpdateImageCamera1(HObject hoImageCamera1)
        {
            this.hoImageCamera1 = hoImageCamera1;

            HOperatorSet.RotateImage(hoImageCamera1, out hoImageCamera1Rotated, -90, "constant");

            hWindowControlCamera1.HalconWindow.DispObj(hoImageCamera1Rotated);
        }

        public void UpdateImageCamera2(HObject hoImageCamera2)
        {
            this.hoImageCamera2 = hoImageCamera2;

            HOperatorSet.RotateImage(hoImageCamera2, out hoImageCamera2Rotated, 90, "constant");

            hWindowControlCamera2.HalconWindow.DispObj(hoImageCamera2Rotated);
        }
        #endregion
    }
}