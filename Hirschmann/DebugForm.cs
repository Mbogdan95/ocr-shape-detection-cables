using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class DebugForm : Form
    {
        private List<ProgramEntry> programs = new List<ProgramEntry>();

        private ProgramEntry selectedProgram;

        private User currentUser;

        private MainForm mainForm;

        private PlcActions plcActions;

        #region Halcon Variables
        // Local iconic variables 
        private HObject hoImageCamera1 = new HObject();
        private HObject hoImageCamera1Rotated = new HObject();
        private HObject hoImageCamera2 = new HObject();
        private HObject hoImageCamera2Rotated = new HObject();
        #endregion

        public DebugForm(User currentUser, PlcActions plcActions, MainForm mainForm)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            this.mainForm = mainForm;

            this.plcActions = plcActions;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            LoadPrograms();

            AddLog("Debug menu: Access");
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region Read inputs
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

            #region Read outputs
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

        private void LoadPrograms()
        {
            programs = SqlCommunication.GetPrograms();

            comboBoxPrograms.Items.Clear();
            comboBoxPrograms.DataSource = programs;
            comboBoxPrograms.DisplayMember = "name";
            comboBoxPrograms.SelectedIndex = -1;
        }

        private void ButtonExecuteClick(object sender, EventArgs e)
        {
            if (comboBoxPrograms.SelectedIndex == -1)
            {
                MessageBox.Show("No program selected", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                selectedProgram = (ProgramEntry)comboBoxPrograms.SelectedItem;
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

            // Start new task to do vision maginc
            Task.Factory.StartNew(() =>
            {
                if (selectedProgram != null)
                {
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
            });
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

            Task.Factory.StartNew(() =>
            {
                if (selectedProgram != null)
                {
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
            });
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

        private void AddLog(string action)
        {
            SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
        }

        private void ButtonOutput1OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(36, 6, true));
        }

        private void ButtonOutput1OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(36, 6, false));
        }

        private void ButtonOutput2OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(36, 7, true));
        }

        private void ButtonOutput2OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(36, 7, false));
        }

        private void ButtonOutput3OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 0, true));
        }

        private void ButtonOutput3OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 0, false));
        }

        private void ButtonOutput4OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 1, true));
        }

        private void ButtonOutput4OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 1, false));
        }

        private void ButtonOutput5OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 2, true));
        }

        private void ButtonOutput5OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 2, false));
        }

        private void ButtonOutput6OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 3, true));
        }

        private void ButtonOutput6OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 3, false));
        }

        private void ButtonOutput7OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 4, true));
        }

        private void ButtonOutput7OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 4, false));
        }

        private void ButtonOutput8OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 5, true));
        }

        private void ButtonOutput8OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 5, false));
        }

        private void ButtonOutput9OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 6, true));
        }

        private void ButtonOutput9OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 6, false));
        }

        private void ButtonOutput10OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 7, true));
        }

        private void ButtonOutput10OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(37, 7, false));
        }

        private void ButtonOutput11OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 0, true));
        }

        private void ButtonOutput11OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 0, false));
        }

        private void ButtonOutput12OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 1, true));
        }

        private void ButtonOutput12OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 1, false));
        }

        private void ButtonOutput13OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 2, true));
        }

        private void buttonOutput13Off_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 2, false));
        }

        private void ButtonOutput14OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 3, true));
        }

        private void ButtonOutput14OffClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => plcActions.WriteOutput(38, 3, false));
        }
    }
}
