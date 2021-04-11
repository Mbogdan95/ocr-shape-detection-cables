using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class DummyTestingForm : Form
    {
        private List<ProgramEntry> programs = new List<ProgramEntry>();

        private ProgramEntry selectedProgram;

        private User currentUser;

        private MainForm mainForm;

        #region Halcon Variables
        // Local iconic variables 
        private HObject hoImageCamera1 = new HObject();
        private HObject hoImageCamera1Rotated = new HObject();
        private HObject hoImageCamera2 = new HObject();
        private HObject hoImageCamera2Rotated = new HObject();
        #endregion

        public DummyTestingForm(User currentUser, MainForm mainForm)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            this.mainForm = mainForm;

            LoadPrograms();

            AddLog("Dummy testing menu: Access");
        }

        /// <summary>
        /// Loads programs for DB and adds to combo box
        /// </summary>
        private void LoadPrograms()
        {
            // Gets list of programs from DB
            programs = SqlCommunication.GetPrograms();

            // Clear combo box
            comboBoxPrograms.Items.Clear();

            // Set data source of combo box
            comboBoxPrograms.DataSource = programs;

            // What it should display
            comboBoxPrograms.DisplayMember = "name";

            // Deselect everything
            comboBoxPrograms.SelectedIndex = -1;
        }

        /// <summary>
        /// Adds new log entry to DB
        /// </summary>
        /// <param name="action"></param>
        private void AddLog(string action)
        {
            // Adds log to DB
            SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
        }

        /// <summary>
        /// Called when Execute button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExecuteClick(object sender, EventArgs e)
        {
            // Checks if any program has been selected
            if (comboBoxPrograms.SelectedIndex == -1)
            {
                // Show no program selected error
                MessageBox.Show("No program selected", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // Get reference of selected program
                selectedProgram = (ProgramEntry)comboBoxPrograms.SelectedItem;
            }

            // Check if camera 1 is connected
            if (mainForm.hvAcqHandleCamera1 != null)
            {
                try
                {
                    // Change trigger source to software
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera1, "TriggerSource", "Software");

                    // Do a software trigger
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera1, "TriggerSoftware", 1);

                    // Change back trigger source to line 2
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera1, "TriggerSource", "Line2");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Trigger camera 1: " + ex.Message);
                }
            }

            // Check if camera 2 is connected
            if (mainForm.hvAcqHandleCamera2 != null)
            {
                try
                {
                    // Change trigger source to software
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera2, "TriggerSource", "Software");

                    // Do a software trigger
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera2, "TriggerSoftware", 1);

                    // Change back trigger source to line 2
                    HOperatorSet.SetFramegrabberParam(mainForm.hvAcqHandleCamera2, "TriggerSource", "Line2");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Trigger camera 2: " + ex.Message);
                }
            }
        }

        #region Camera 1 actions
        /// <summary>
        /// Updates the image
        /// </summary>
        /// <param name="hoImageCamera1">New image from camera</param>
        public void UpdateImageCamera1(HObject hoImageCamera1)
        {
            // Get reference of image from camera 1
            this.hoImageCamera1 = hoImageCamera1;

            // Rotate image
            HOperatorSet.RotateImage(hoImageCamera1, out hoImageCamera1Rotated, -90, "constant");

            // Display image
            hWindowControlCamera1.HalconWindow.DispObj(hoImageCamera1Rotated);

            // Check if selected program has camera 1 activated for vision magic
            if (selectedProgram.Camera1)
            {
                // Check the color of cable
                CheckColorCamera1();

                if (selectedProgram.LogosCamera1)
                {
                    CheckLogosCamera1(out HObject logo1Region, out HObject logo2Region);

                    CheckTextCamera1(logo1Region, logo2Region);
                }
                else if (selectedProgram.ShapesCamera1)
                {
                    CheckShapesCamera1(out HObject shapesRegion, out HTuple checkedSahpes);

                    CheckTextCamera1(shapesRegion, shapesRegion);
                }
                else
                {
                    HOperatorSet.GenEmptyObj(out HObject emptyObject);

                    CheckTextCamera1(emptyObject, emptyObject);
                }
            }
        }

        private void CheckColorCamera1()
        {
            try
            {
                HOperatorSet.RotateImage(hoImageCamera1Rotated, out HObject hImageRotated, -90, "constant");

                string colorDetected = HalconProcedures.CheckColor(hImageRotated);

                hWindowControlCamera1.HalconWindow.SetFont("Arial-Bold-30");

                if (colorDetected == string.Empty)
                {
                    hWindowControlCamera1.HalconWindow.DispText("NO COLOR DETECTED", "image", 20, 20, "black", new HTuple(), new HTuple());
                    Console.WriteLine("No color detected");
                    return;
                }

                if (colorDetected == selectedProgram.ColorCamera1)
                {
                    hWindowControlCamera1.HalconWindow.DispText("COLOR OK", "image", 20, 20, "green", new HTuple(), new HTuple());
                    Console.WriteLine("Camera 1 color OK");
                }
                else
                {
                    hWindowControlCamera1.HalconWindow.DispText("COLOR NOK", "image", 20, 20, "red", new HTuple(), new HTuple());

                    Console.WriteLine("Camera 1 color NOK");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckLogosCamera1(out HObject logo1RegionCheckLogos, out HObject logo2RegionCheckLogos)
        {
            HOperatorSet.GenEmptyObj(out logo1RegionCheckLogos);
            HOperatorSet.GenEmptyObj(out logo2RegionCheckLogos);

            try
            {
                HOperatorSet.RotateImage(hoImageCamera1Rotated, out HObject hImageRotated, -90, "constant");

                HalconProcedures.CheckLogos(hImageRotated,
                    selectedProgram.NumberOfLogosCamera1,
                    selectedProgram.ColorCamera1,
                    selectedProgram.Logo1Camera1SaveLocation.Replace(' ', '\\'),
                    selectedProgram.Logo2Camera1SaveLocation.Replace(' ', '\\'),
                    out HObject logo1Region,
                    out HObject logo2Region,
                    out HObject logo1Model,
                    out HObject logo2Model,
                    out double logo1Confidence,
                    out double logo2Confidence);

                logo1RegionCheckLogos = logo1Region;
                logo2RegionCheckLogos = logo2Region;

                hWindowControlCamera1.HalconWindow.SetColor("red");
                hWindowControlCamera1.HalconWindow.SetLineWidth(2);
                hWindowControlCamera1.HalconWindow.SetFont("Arial-Bold-18");

                if (selectedProgram.NumberOfLogosCamera1 == 1)
                {
                    HOperatorSet.AreaCenter(logo1Region, out HTuple areaLogo1, out HTuple rowLogo1, out HTuple columnLogo1);

                    hWindowControlCamera1.HalconWindow.DispObj(logo1Model);

                    HOperatorSet.SmallestRectangle1(logo1Region, out HTuple row1Logo1, out HTuple column1Logo1, out HTuple row2Logo1, out HTuple column2Logo1);

                    hWindowControlCamera1.HalconWindow.SetTposition(row2Logo1 + 20, column1Logo1);
                    hWindowControlCamera1.HalconWindow.WriteString(logo1Confidence);
                }
                else if (selectedProgram.NumberOfLogosCamera1 == 2)
                {
                    HOperatorSet.AreaCenter(logo1Region, out HTuple areaLogo1, out HTuple rowLogo1, out HTuple columnLogo1);
                    HOperatorSet.AreaCenter(logo2Region, out HTuple areaLogo2, out HTuple rowLogo2, out HTuple columnLogo2);

                    hWindowControlCamera1.HalconWindow.DispObj(logo1Model);
                    hWindowControlCamera1.HalconWindow.DispObj(logo2Model);

                    HOperatorSet.SmallestRectangle1(logo1Region, out HTuple row1Logo1, out HTuple column1Logo1, out HTuple row2Logo1, out HTuple column2Logo1);

                    hWindowControlCamera1.HalconWindow.SetTposition(row2Logo1 + 50, column1Logo1);
                    hWindowControlCamera1.HalconWindow.WriteString(logo1Confidence);

                    HOperatorSet.SmallestRectangle1(logo2Region, out HTuple row1Logo2, out HTuple column1Logo2, out HTuple row2Logo2, out HTuple column2Logo2);

                    hWindowControlCamera1.HalconWindow.SetTposition(row2Logo2 + 50, column1Logo2);
                    hWindowControlCamera1.HalconWindow.WriteString(logo2Confidence);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckTextCamera1(HObject logo1Region, HObject logo2Region)
        {
            try
            {
                HOperatorSet.RotateImage(hoImageCamera1Rotated, out HObject hImageRotated, -90, "constant");

                HalconProcedures.CheckText(hImageRotated,
                    selectedProgram.ColorCamera1,
                    selectedProgram.NumberOfLogosCamera1,
                    logo1Region,
                    logo2Region,
                    out HObject textLines,
                    out HTuple singleCharacters);

                hWindowControlCamera1.HalconWindow.SetColor("blue");
                hWindowControlCamera1.HalconWindow.DispObj(textLines);

                HOperatorSet.SmallestRectangle1(textLines, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);

                hWindowControlCamera1.HalconWindow.SetColor("green");
                hWindowControlCamera1.HalconWindow.SetFont("Arial-Bold-18");

                for (int i = 0; i < singleCharacters.Length; i++)
                {
                    hWindowControlCamera1.HalconWindow.SetTposition(row2[i] + 20, column1[i]);
                    hWindowControlCamera1.HalconWindow.WriteString(singleCharacters[i].S);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CheckShapesCamera1(out HObject shapesRegion, out HTuple checkedShapes)
        {
            HOperatorSet.GenEmptyObj(out shapesRegion);

            checkedShapes = new HTuple();

            try
            {
                HOperatorSet.RotateImage(hoImageCamera1Rotated, out HObject hImageRotated, -90, "constant");

                HalconProcedures.CheckShapes(hImageRotated,
                    selectedProgram.ColorCamera1,
                    selectedProgram.ShapeTypeCamera1,
                    out HObject selectedShapes,
                    out HTuple checkShapes);

                shapesRegion = selectedShapes;
                checkedShapes = checkShapes;

                hWindowControlCamera1.HalconWindow.SetColor("yellow");
                hWindowControlCamera1.HalconWindow.DispObj(selectedShapes);

                HOperatorSet.CountObj(selectedShapes, out HTuple numberOfShapes);

                hWindowControlCamera1.HalconWindow.SetColor("coral");
                hWindowControlCamera1.HalconWindow.SetFont("Arial-Bold-18");

                for (int i = 1; i <= numberOfShapes; i++)
                {
                    HOperatorSet.SelectObj(selectedShapes, out HObject selectedShape, i);
                    HOperatorSet.SmallestRectangle1(selectedShape, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);

                    hWindowControlCamera1.HalconWindow.SetTposition(row2 + 20, column1 + 30);

                    if (checkedShapes[i - 1] == 1)
                    {
                        hWindowControlCamera1.HalconWindow.WriteString("OK");
                    }
                    else
                    {
                        hWindowControlCamera1.HalconWindow.WriteString("NOK");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion

        #region Camera 2 actions
        public void UpdateImageCamera2(HObject hoImageCamera2)
        {
            this.hoImageCamera2 = hoImageCamera2;

            HOperatorSet.RotateImage(hoImageCamera2, out hoImageCamera2Rotated, 90, "constant");

            hWindowControlCamera2.HalconWindow.DispObj(hoImageCamera2Rotated);

            if (selectedProgram.Camera2)
            {
                CheckColorCamera2();

                if (selectedProgram.LogosCamera2)
                {
                    CheckLogosCamera2(out HObject logo1Region, out HObject logo2Region);

                    CheckTextCamera2(logo1Region, logo2Region);
                }
                else if (selectedProgram.ShapesCamera2)
                {
                    CheckShapesCamera2(out HObject shapesRegion, out HTuple checkedSahpes);

                    CheckTextCamera2(shapesRegion, shapesRegion);
                }
                else
                {
                    HOperatorSet.GenEmptyObj(out HObject emptyObject);

                    CheckTextCamera2(emptyObject, emptyObject);
                }
            }
        }

        private void CheckColorCamera2()
        {
            try
            {
                HOperatorSet.RotateImage(hoImageCamera2Rotated, out HObject hImageRotated, -90, "constant");

                string colorDetected = HalconProcedures.CheckColor(hImageRotated);

                if (colorDetected == string.Empty)
                {
                    Console.WriteLine("No color detected");
                    return;
                }

                if (colorDetected == selectedProgram.ColorCamera2)
                {
                    Console.WriteLine("Camera 2 color OK");
                }
                else
                {
                    Console.WriteLine("Camera 2 color NOK");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckLogosCamera2(out HObject logo1RegionCheckLogos, out HObject logo2RegionCheckLogos)
        {
            HOperatorSet.GenEmptyObj(out logo1RegionCheckLogos);
            HOperatorSet.GenEmptyObj(out logo2RegionCheckLogos);

            try
            {
                HOperatorSet.RotateImage(hoImageCamera2Rotated, out HObject hImageRotated, -90, "constant");

                HalconProcedures.CheckLogos(hImageRotated,
                    selectedProgram.NumberOfLogosCamera2,
                    selectedProgram.ColorCamera2,
                    selectedProgram.Logo1Camera2SaveLocation.Replace(' ', '\\'),
                    selectedProgram.Logo2Camera2SaveLocation.Replace(' ', '\\'),
                    out HObject logo1Region,
                    out HObject logo2Region,
                    out HObject logo1Model,
                    out HObject logo2Model,
                    out double logo1Confidence,
                    out double logo2Confidence);

                logo1RegionCheckLogos = logo1Region;
                logo2RegionCheckLogos = logo2Region;

                hWindowControlCamera2.HalconWindow.SetColor("red");
                hWindowControlCamera2.HalconWindow.SetLineWidth(2);

                if (selectedProgram.NumberOfLogosCamera2 == 1)
                {
                    HOperatorSet.AreaCenter(logo1Region, out HTuple areaLogo1, out HTuple rowLogo1, out HTuple columnLogo1);

                    hWindowControlCamera2.HalconWindow.DispObj(logo1Model);

                    HOperatorSet.SmallestRectangle1(logo1Region, out HTuple row1Logo1, out HTuple column1Logo1, out HTuple row2Logo1, out HTuple column2Logo1);

                    hWindowControlCamera2.HalconWindow.SetTposition(row2Logo1 + 20, column1Logo1);
                    hWindowControlCamera2.HalconWindow.WriteString(logo1Confidence);
                }
                else if (selectedProgram.NumberOfLogosCamera2 == 2)
                {
                    HOperatorSet.AreaCenter(logo1Region, out HTuple areaLogo1, out HTuple rowLogo1, out HTuple columnLogo1);
                    HOperatorSet.AreaCenter(logo2Region, out HTuple areaLogo2, out HTuple rowLogo2, out HTuple columnLogo2);

                    hWindowControlCamera2.HalconWindow.DispObj(logo1Model);
                    hWindowControlCamera2.HalconWindow.DispObj(logo2Model);

                    HOperatorSet.SmallestRectangle1(logo1Region, out HTuple row1Logo1, out HTuple column1Logo1, out HTuple row2Logo1, out HTuple column2Logo1);

                    hWindowControlCamera2.HalconWindow.SetTposition(row2Logo1 + 50, column1Logo1);
                    hWindowControlCamera2.HalconWindow.WriteString(logo1Confidence);

                    HOperatorSet.SmallestRectangle1(logo2Region, out HTuple row1Logo2, out HTuple column1Logo2, out HTuple row2Logo2, out HTuple column2Logo2);

                    hWindowControlCamera2.HalconWindow.SetTposition(row2Logo2 + 50, column1Logo2);
                    hWindowControlCamera2.HalconWindow.WriteString(logo2Confidence);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckTextCamera2(HObject logo1Region, HObject logo2Region)
        {
            try
            {
                HOperatorSet.RotateImage(hoImageCamera2Rotated, out HObject hImageRotated, -90, "constant");

                HalconProcedures.CheckText(hImageRotated,
                    selectedProgram.ColorCamera2,
                    selectedProgram.NumberOfLogosCamera2,
                    logo1Region,
                    logo2Region,
                    out HObject textLines,
                    out HTuple singleCharacters);

                hWindowControlCamera2.HalconWindow.SetColor("blue");
                hWindowControlCamera2.HalconWindow.DispObj(textLines);

                HOperatorSet.SmallestRectangle1(textLines, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);

                hWindowControlCamera2.HalconWindow.SetColor("green");
                hWindowControlCamera2.HalconWindow.SetFont("Arial-Bold-18");

                for (int i = 0; i < singleCharacters.Length; i++)
                {
                    hWindowControlCamera2.HalconWindow.SetTposition(row2[i] + 20, column1[i]);
                    hWindowControlCamera2.HalconWindow.WriteString(singleCharacters[i].S);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CheckShapesCamera2(out HObject shapesRegion, out HTuple checkedShapes)
        {
            HOperatorSet.GenEmptyObj(out shapesRegion);

            checkedShapes = new HTuple();

            try
            {
                HOperatorSet.RotateImage(hoImageCamera2Rotated, out HObject hImageRotated, -90, "constant");

                HalconProcedures.CheckShapes(hImageRotated,
                    selectedProgram.ColorCamera2,
                    selectedProgram.ShapeTypeCamera2,
                    out HObject selectedShapes,
                    out HTuple checkShapes);

                shapesRegion = selectedShapes;
                checkedShapes = checkShapes;

                hWindowControlCamera2.HalconWindow.SetColor("yellow");
                hWindowControlCamera2.HalconWindow.DispObj(selectedShapes);

                HOperatorSet.CountObj(selectedShapes, out HTuple numberOfShapes);

                hWindowControlCamera2.HalconWindow.SetColor("coral");
                hWindowControlCamera2.HalconWindow.SetFont("Arial-Bold-18");

                for (int i = 1; i <= numberOfShapes; i++)
                {
                    HOperatorSet.SelectObj(selectedShapes, out HObject selectedShape, i);
                    HOperatorSet.SmallestRectangle1(selectedShape, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);

                    hWindowControlCamera2.HalconWindow.SetTposition(row2 + 20, column1 + 30);

                    if (checkedShapes[i - 1] == 1)
                    {
                        hWindowControlCamera2.HalconWindow.WriteString("OK");
                    }
                    else
                    {
                        hWindowControlCamera2.HalconWindow.WriteString("NOK");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion
    }
}
