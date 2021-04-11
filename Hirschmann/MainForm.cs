using HalconDotNet;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Hirschmann
{
    public partial class MainForm : Form
    {
        private bool systemDisabled = true;
        private bool camera1Finish = false;
        private bool camera2Finish = false;
        private bool cableValidationCamera1 = false;
        private bool cableValidationCamera2 = false;

        public bool dummyFormOpen = false;
        public bool alarm1 = false;
        public bool alarm2 = false;
        public bool alarm3 = false;
        public bool alarm4 = false;
        public bool alarm5 = false;
        public bool alarm6 = false;
        public bool alarm7 = false;
        public bool alarm8 = false;
        public bool alarm9 = false;
        public bool alarm10 = false;
        public bool statusRun = false;

        private string nameCamera1 = "2676014DC93C_Basler_daA160060uc";
        private string nameCamera2 = "2676014DC93D_Basler_daA160060uc";
        private string currentDirectory = "";

        public int totalParts = 0;
        public int okParts = 0;
        public int nokParts = 0;

        public float ngRate = 0f;


        private ProgramEntry selectedProgram;

        private PlcActions plcActions = new PlcActions();

        private System.Timers.Timer alarmTimer;

        private User currentUser = new User();

        #region Halcon Variables
        // Local iconic variables 
        public HObject hoImageCamera1 = new HObject();
        private HObject hoImageCamera1Rotated = new HObject();
        public HObject hoImageCamera2 = new HObject();
        private HObject hoImageCamera2Rotated = new HObject();

        // Local control variables 
        public HTuple hvAcqHandleCamera1 = null;
        public HTuple hvAcqHandleCamera2 = null;
        #endregion

        public MainForm()
        {
            InitializeComponent();

            HalconProcedures.InitializeProcedures();

            //Start PLC connection
            Task.Factory.StartNew(() => plcActions.ContinousReadWritePlc(out alarm1, out alarm2, out alarm3, out alarm4, out alarm5, out alarm6, out alarm7, out alarm8, out alarm9, out alarm10, out statusRun));

            alarmTimer = new System.Timers.Timer();
            alarmTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            alarmTimer.Interval = 500;
            alarmTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (alarm1 || alarm2 || alarm3 || alarm4 || alarm5 || alarm6 || alarm7 || alarm8 || alarm9 || alarm10)
            {
                buttonAlarms.Enabled = true;

                if (buttonAlarms.BackColor == Color.LightSeaGreen)
                {
                    buttonAlarms.BackColor = Color.Salmon;
                }
                else if (buttonAlarms.BackColor == Color.Red)
                {
                    buttonAlarms.BackColor = Color.Salmon;
                }
                else if (buttonAlarms.BackColor == Color.Salmon)
                {
                    buttonAlarms.BackColor = Color.Red;
                }
            }
            else
            {
                buttonAlarms.Enabled = false;
                buttonAlarms.BackColor = Color.LightSeaGreen;
            }
        }

        /// <summary>
        /// Method called when form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            // Set program name to NONE
            labelSelectedProgramName.Text = "NONE";

            // Set color of program name background to yellow
            labelSelectedProgramName.BackColor = Color.Yellow;

            currentDirectory = Directory.GetCurrentDirectory();

            // Close all frame grabbers
            HOperatorSet.CloseAllFramegrabbers();

            // Start new task for camera 1 and camera 2
            ConnectFrameGrabbers();

            AddLog(new User(), $"Main Application: Open");

            ResetCounters();
        }

        /// <summary>
        /// Methdo called when the form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            AbortImageGrab();

            HOperatorSet.CloseAllFramegrabbers();

            AddLog(new User(), $"Main Application: Close");
        }

        /// <summary>
        /// Method called to reset counters
        /// </summary>
        private void ResetCounters()
        {
            // Set live stats to 0
            totalParts = 0;
            okParts = 0;
            nokParts = 0;

            ngRate = 0f;

            UpdateStats();
        }

        /// <summary>
        /// Updates live stats
        /// </summary>
        public void UpdateStats()
        {
            labelTotal.Text = $"TOTAL: {totalParts}";
            labelOkParts.Text = $"OK PARTS: {okParts}";
            labelNokParts.Text = $"NOK PARTS: {nokParts}";

            // Check values of ok and ng parts because 0/0 in impossible
            if (okParts == 0 && nokParts == 0)
            {
                labelNokRate.Text = $"NG RATE: 0%";
            }
            else
            {
                labelNokRate.Text = $"NG RATE: {string.Format("{0:0.00}", (float)nokParts / totalParts * 100)}%";
            }
        }

        /// <summary>
        /// Connect to cameras
        /// </summary>
        private void ConnectFrameGrabbers()
        {
            Task.Factory.StartNew(() => OpenFrameGrabberCamera1());
            Task.Factory.StartNew(() => OpenFrameGrabberCamera2());
        }

        /// <summary>
        /// Method called to open connection to camera 1
        /// </summary>
        private void OpenFrameGrabberCamera1()
        {
            HOperatorSet.GenEmptyObj(out hoImageCamera1);
            // Try to open connection to camera 1
            try
            {
                // Open frame grabber
                HOperatorSet.OpenFramegrabber("USB3Vision", 0, 0, 0, 0, 0, 0, "progressive",
                                    -1, "default", -1, "true", "default", nameCamera1,
                                    0, -1, out hvAcqHandleCamera1);

                HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera1, "grab_timeout", 1000);
                HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera1, "TriggerSource", "Line2");
                HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera1, "TriggerMode", "On");

                HOperatorSet.GrabImageStart(hvAcqHandleCamera1, -1);

                GrabImagesCamera1();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #5312"))
                {
                    MessageBox.Show("Cannot connect to camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Method called to open connection to camera 2
        /// </summary>
        private void OpenFrameGrabberCamera2()
        {
            HOperatorSet.GenEmptyObj(out hoImageCamera2);
            // Try to open connection to camera 2
            try
            {
                // Open frame grabber
                HOperatorSet.OpenFramegrabber("USB3Vision", 0, 0, 0, 0, 0, 0, "progressive",
                                    -1, "default", -1, "true", "default", nameCamera2,
                                    0, -1, out hvAcqHandleCamera2);

                HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera2, "grab_timeout", 1000);
                HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera2, "TriggerSource", "Line2");
                HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera2, "TriggerMode", "On");

                HOperatorSet.GrabImageStart(hvAcqHandleCamera2, -1);

                GrabImagesCamera2();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #5312"))
                {
                    MessageBox.Show("Cannot connect to camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GrabImagesCamera1()
        {
            bool camera1Connected = true;

            while (camera1Connected)
            {
                try
                {
                    cableValidationCamera1 = false;

                    camera1Connected = CheckCamera1Connection(nameCamera1);

                    hoImageCamera1.Dispose();
                    HOperatorSet.GrabImageAsync(out hoImageCamera1, hvAcqHandleCamera1, -1);
                    HOperatorSet.RotateImage(hoImageCamera1, out HObject hoImageCameraRotate1, -90, "constant");

                    hWindowControlCamera1.HalconWindow.AttachBackgroundToWindow(new HImage(hoImageCameraRotate1));

                    if (Utils.CheckIfFormIsOpen("ManageProgramsForm"))
                    {
                        ManageProgramsForm manageProgramsForm = (ManageProgramsForm)Utils.GetFormReference("ManageProgramsForm");
                        manageProgramsForm.UpdateImageCamera1(hoImageCamera1);
                        return;
                    }

                    if (Utils.CheckIfFormIsOpen("DebugForm"))
                    {
                        DebugForm debugForm = (DebugForm)Utils.GetFormReference("DebugForm");
                        debugForm.UpdateImageCamera1(hoImageCamera1);
                        return;
                    }

                    if (Utils.CheckIfFormIsOpen("DummyTestingForm"))
                    {
                        DummyTestingForm dummyTestingForm = (DummyTestingForm)Utils.GetFormReference("DummyTestingForm");
                        dummyTestingForm.UpdateImageCamera1(hoImageCamera1);
                        return;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        if (selectedProgram != null)
                        {
                            // Check if selected program has camera 1 activated for vision magic
                            if (selectedProgram.Camera1)
                            {
                                // Check the color of cable
                                cableValidationCamera1 = CheckColorCamera1();

                                if (selectedProgram.LogosCamera1)
                                {
                                    cableValidationCamera1 = CheckLogosCamera1(out HObject logo1Region, out HObject logo2Region);

                                    cableValidationCamera1 = CheckTextCamera1(logo1Region, logo2Region);
                                }
                                else if (selectedProgram.ShapesCamera1)
                                {
                                    cableValidationCamera1 = CheckShapesCamera1(out HObject shapesRegion, out HTuple checkedSahpes);

                                    cableValidationCamera1 = CheckTextCamera1(shapesRegion, shapesRegion);
                                }
                                else
                                {
                                    HOperatorSet.GenEmptyObj(out HObject emptyObject);

                                    cableValidationCamera1 = CheckTextCamera1(emptyObject, emptyObject);
                                }
                            }
                        }
                    });

                    camera1Finish = true;

                    CableValidation();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("HALCON error #5322"))
                    {
                        Console.WriteLine("OpenFrameGrabberError: " + ex.Message);
                    }
                }
            }

            if (!camera1Connected)
            {
                MessageBox.Show("Camera 1 lost connection", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HOperatorSet.CloseFramegrabber(hvAcqHandleCamera1);
                hvAcqHandleCamera1 = null;
            }
        }

        private bool CheckColorCamera1()
        {
            bool cableVerification = false;

            try
            {
                HOperatorSet.RotateImage(hoImageCamera1Rotated, out HObject hImageRotated, -90, "constant");

                string colorDetected = HalconProcedures.CheckColor(hImageRotated);

                hWindowControlCamera1.HalconWindow.SetFont("Arial-Bold-30");

                if (colorDetected == string.Empty)
                {
                    hWindowControlCamera1.HalconWindow.DispText("NO COLOR DETECTED", "image", 20, 20, "black", new HTuple(), new HTuple());
                    Console.WriteLine("No color detected");
                    return false;
                }

                if (colorDetected == selectedProgram.ColorCamera1)
                {
                    hWindowControlCamera1.HalconWindow.DispText("COLOR OK", "image", 20, 20, "green", new HTuple(), new HTuple());
                    Console.WriteLine("Camera 1 color OK");

                    cableVerification = true;
                }
                else
                {
                    hWindowControlCamera1.HalconWindow.DispText("COLOR NOK", "image", 20, 20, "red", new HTuple(), new HTuple());

                    Console.WriteLine("Camera 1 color NOK");

                    cableVerification = false;
                }

                return cableVerification;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
        }

        private bool CheckLogosCamera1(out HObject logo1RegionCheckLogos, out HObject logo2RegionCheckLogos)
        {
            HOperatorSet.GenEmptyObj(out logo1RegionCheckLogos);
            HOperatorSet.GenEmptyObj(out logo2RegionCheckLogos);

            bool cableVerification = false;
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

                    if (logo1Confidence < selectedProgram.Logo1Camera1Confidence)
                    {
                        hWindowControlCamera1.HalconWindow.DispText("LOGO1 NOK", "image", 20, 40, "red", new HTuple(), new HTuple());

                        cableVerification = false;
                    }
                    else
                    {
                        hWindowControlCamera1.HalconWindow.DispText("LOGO1 OK", "image", 20, 40, "green", new HTuple(), new HTuple());

                        cableVerification = true;
                    }
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

                    if (logo1Confidence < selectedProgram.Logo1Camera1Confidence)
                    {
                        hWindowControlCamera1.HalconWindow.DispText("LOGO1 NOK", "image", 20, 40, "red", new HTuple(), new HTuple());

                        cableVerification = false;
                    }
                    else
                    {
                        hWindowControlCamera1.HalconWindow.DispText("LOGO1 OK", "image", 20, 40, "green", new HTuple(), new HTuple());

                        cableVerification = true;
                    }

                    if (logo2Confidence < selectedProgram.Logo2Camera1Confidence)
                    {
                        hWindowControlCamera1.HalconWindow.DispText("LOGO2 NOK", "image", 20, 60, "red", new HTuple(), new HTuple());

                        cableVerification = false;
                    }
                    else
                    {
                        hWindowControlCamera1.HalconWindow.DispText("LOGO2 OK", "image", 20, 60, "green", new HTuple(), new HTuple());

                        cableVerification = true;
                    }
                }

                return cableVerification;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
        }

        private bool CheckTextCamera1(HObject logo1Region, HObject logo2Region)
        {
            string text = string.Empty;

            bool cableVerification = false;

            int textLength;

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

                if (singleCharacters.Length > selectedProgram.TextToDetectCamera1.Length)
                {
                    textLength = selectedProgram.TextToDetectCamera1.Length;
                }
                else
                {
                    textLength = singleCharacters.Length;
                }

                for (int i = 0; i < textLength; i++)
                {
                    hWindowControlCamera1.HalconWindow.SetTposition(row2[i] + 20, column1[i]);
                    hWindowControlCamera1.HalconWindow.WriteString(singleCharacters[i].S);

                    text = text + singleCharacters[i].S;
                }

                if (text != selectedProgram.TextToDetectCamera1)
                {
                    hWindowControlCamera1.HalconWindow.DispText("OCR NOK", "image", 20, 80, "red", new HTuple(), new HTuple());

                    cableVerification = false;
                }
                else
                {
                    hWindowControlCamera1.HalconWindow.DispText("OCR OK", "image", 20, 80, "green", new HTuple(), new HTuple());

                    cableVerification = true;
                }

                return cableVerification;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return false;
            }
        }

        private bool CheckShapesCamera1(out HObject shapesRegion, out HTuple checkedShapes)
        {
            HOperatorSet.GenEmptyObj(out shapesRegion);

            checkedShapes = new HTuple();

            bool shapesValidation = true;

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

                        shapesValidation = false;
                    }

                    if (!shapesValidation)
                    {
                        hWindowControlCamera1.HalconWindow.DispText("SHAPES NOK", "image", 20, 100, "red", new HTuple(), new HTuple());
                    }
                    else
                    {
                        hWindowControlCamera1.HalconWindow.DispText("SHAPES OK", "image", 20, 100, "green", new HTuple(), new HTuple());
                    }
                }

                return shapesValidation;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 1", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return false;
            }
        }

        private void GrabImagesCamera2()
        {
            bool camera2Connected = true;


            while (camera2Connected)
            {
                try
                {
                    cableValidationCamera2 = false;

                    camera2Connected = CheckCamera2Connection(nameCamera2);

                    hoImageCamera2.Dispose();
                    HOperatorSet.GrabImageAsync(out hoImageCamera2, hvAcqHandleCamera2, -1);
                    HOperatorSet.RotateImage(hoImageCamera2, out HObject hoImageCameraRotate2, 90, "constant");

                    hWindowControlCamera2.HalconWindow.AttachBackgroundToWindow(new HImage(hoImageCameraRotate2));

                    if (Utils.CheckIfFormIsOpen("ManageProgramsForm"))
                    {
                        ManageProgramsForm manageProgramsForm = (ManageProgramsForm)Utils.GetFormReference("ManageProgramsForm");
                        manageProgramsForm.UpdateImageCamera2(hoImageCamera2);
                        return;
                    }

                    if (Utils.CheckIfFormIsOpen("DebugForm"))
                    {
                        DebugForm debugForm = (DebugForm)Utils.GetFormReference("DebugForm");
                        debugForm.UpdateImageCamera2(hoImageCamera2);
                        return;
                    }

                    if (Utils.CheckIfFormIsOpen("DummyTestingForm"))
                    {
                        DummyTestingForm dummyTestingForm = (DummyTestingForm)Utils.GetFormReference("DummyTestingForm");
                        dummyTestingForm.UpdateImageCamera2(hoImageCamera2);
                        return;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        if (selectedProgram != null)
                        {
                            if (selectedProgram.Camera2)
                            {
                                cableValidationCamera2 = CheckColorCamera2();

                                if (selectedProgram.LogosCamera2)
                                {
                                    cableValidationCamera2 = CheckLogosCamera2(out HObject logo1Region, out HObject logo2Region);

                                    cableValidationCamera2 = CheckTextCamera2(logo1Region, logo2Region);
                                }
                                else if (selectedProgram.ShapesCamera2)
                                {
                                    cableValidationCamera2 = CheckShapesCamera2(out HObject shapesRegion, out HTuple checkedSahpes);

                                    cableValidationCamera2 = CheckTextCamera2(shapesRegion, shapesRegion);
                                }
                                else
                                {
                                    HOperatorSet.GenEmptyObj(out HObject emptyObject);

                                    cableValidationCamera2 = CheckTextCamera2(emptyObject, emptyObject);
                                }
                            }
                        }
                    });

                    camera2Finish = true;

                    CableValidation();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("HALCON error #5322"))
                    {
                        Console.WriteLine("OpenFrameGrabberError: " + ex.Message);
                    }
                }
            }

            if (!camera2Connected)
            {
                MessageBox.Show("Camera 2 lost connection", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HOperatorSet.CloseFramegrabber(hvAcqHandleCamera2);
                hvAcqHandleCamera2 = null;
            }
        }

        private bool CheckColorCamera2()
        {
            bool cableValidation = false;

            try
            {
                HOperatorSet.RotateImage(hoImageCamera2Rotated, out HObject hImageRotated, -90, "constant");

                string colorDetected = HalconProcedures.CheckColor(hImageRotated);

                if (colorDetected == string.Empty)
                {
                    hWindowControlCamera2.HalconWindow.DispText("NO COLOR DETECTED", "image", 20, 20, "black", new HTuple(), new HTuple());
                    Console.WriteLine("No color detected");
                    return false;
                }

                if (colorDetected == selectedProgram.ColorCamera2)
                {
                    hWindowControlCamera1.HalconWindow.DispText("COLOR OK", "image", 20, 20, "green", new HTuple(), new HTuple());
                    Console.WriteLine("Camera 1 color OK");

                    cableValidation = true;
                }
                else
                {
                    hWindowControlCamera2.HalconWindow.DispText("COLOR NOK", "image", 20, 20, "red", new HTuple(), new HTuple());

                    Console.WriteLine("Camera 1 color NOK");

                    cableValidation = false;
                }

                return cableValidation;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
        }

        private bool CheckLogosCamera2(out HObject logo1RegionCheckLogos, out HObject logo2RegionCheckLogos)
        {
            HOperatorSet.GenEmptyObj(out logo1RegionCheckLogos);
            HOperatorSet.GenEmptyObj(out logo2RegionCheckLogos);

            bool cableValidation = false;

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

                    if (logo1Confidence < selectedProgram.Logo1Camera2Confidence)
                    {
                        hWindowControlCamera2.HalconWindow.DispText("LOGO1 NOK", "image", 20, 40, "red", new HTuple(), new HTuple());

                        cableValidation = false;
                    }
                    else
                    {
                        hWindowControlCamera2.HalconWindow.DispText("LOGO1 OK", "image", 20, 40, "green", new HTuple(), new HTuple());

                        cableValidation = true;
                    }
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

                    if (logo1Confidence < selectedProgram.Logo1Camera2Confidence)
                    {
                        hWindowControlCamera2.HalconWindow.DispText("LOGO1 NOK", "image", 20, 40, "red", new HTuple(), new HTuple());

                        cableValidation = false;
                    }
                    else
                    {
                        hWindowControlCamera2.HalconWindow.DispText("LOGO1 OK", "image", 20, 40, "green", new HTuple(), new HTuple());

                        cableValidation = true;
                    }

                    if (logo2Confidence < selectedProgram.Logo2Camera2Confidence)
                    {
                        hWindowControlCamera2.HalconWindow.DispText("LOGO2 NOK", "image", 20, 60, "red", new HTuple(), new HTuple());

                        cableValidation = false;
                    }
                    else
                    {
                        hWindowControlCamera2.HalconWindow.DispText("LOGO2 OK", "image", 20, 60, "green", new HTuple(), new HTuple());

                        cableValidation = true;
                    }
                }

                return cableValidation;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("HALCON error #4056"))
                {
                    MessageBox.Show("No image camera 2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
        }

        private bool CheckTextCamera2(HObject logo1Region, HObject logo2Region)
        {
            string text = string.Empty;

            bool cableValidation = false;

            int textLength;

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

                if (singleCharacters.Length > selectedProgram.TextToDetectCamera1.Length)
                {
                    textLength = selectedProgram.TextToDetectCamera1.Length;
                }
                else
                {
                    textLength = singleCharacters.Length;
                }

                for (int i = 0; i < textLength; i++)
                {
                    hWindowControlCamera2.HalconWindow.SetTposition(row2[i] + 20, column1[i]);
                    hWindowControlCamera2.HalconWindow.WriteString(singleCharacters[i].S);
                    text = text + singleCharacters[i].S;
                }

                if (text != selectedProgram.TextToDetectCamera2)
                {
                    hWindowControlCamera1.HalconWindow.DispText("OCR NOK", "image", 20, 80, "red", new HTuple(), new HTuple());

                    cableValidation = false;
                }
                else
                {
                    hWindowControlCamera1.HalconWindow.DispText("OCR OK", "image", 20, 80, "green", new HTuple(), new HTuple());

                    cableValidation = true;
                }

                return cableValidation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return false;
            }
        }

        private bool CheckShapesCamera2(out HObject shapesRegion, out HTuple checkedShapes)
        {
            HOperatorSet.GenEmptyObj(out shapesRegion);

            checkedShapes = new HTuple();

            bool shapesValidation = true;

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

                        shapesValidation = false;
                    }

                    if (!shapesValidation)
                    {
                        hWindowControlCamera1.HalconWindow.DispText("SHAPES NOK", "image", 20, 100, "red", new HTuple(), new HTuple());
                    }
                    else
                    {
                        hWindowControlCamera1.HalconWindow.DispText("SHAPES OK", "image", 20, 100, "green", new HTuple(), new HTuple());
                    }
                }

                return shapesValidation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return false;
            }
        }

        private void CableValidation()
        {
            if (camera1Finish && camera2Finish)
            {
                if (!cableValidationCamera1 || !cableValidationCamera2)
                {
                    plcActions.WriteCableNok();
                }
                else
                {
                    plcActions.WriteCableOk();
                }

                camera1Finish = false;
                camera2Finish = false;
            }
        }

        /// <summary>
        /// Method called when User Administration button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserAdministrationToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (LogInForm logInForm = new LogInForm())
            {
                // Show login form
                logInForm.ShowDialog(this);

                // Check if user has been checked
                if (logInForm.GetUserChecked())
                {
                    // Check if user is valid
                    if (logInForm.GetUserValid())
                    {
                        // Get the user that logged in
                        User user = logInForm.GetUser();

                        // Check rank of user
                        if (user.Rank != Rank.Administrator)
                        {
                            // Show message box
                            MessageBox.Show("You do not have clearance to access this menu", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            using (UserConfigurationForm userConfigurationForm = new UserConfigurationForm(user))
                            {
                                // Show user configuration form
                                userConfigurationForm.ShowDialog(this);
                            }
                        }
                    }
                    else
                    {
                        // Show message box
                        MessageBox.Show("Badge ID or password invalid", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LogsToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (LogsForm logsForm = new LogsForm())
            {
                logsForm.ShowDialog(this);
            }
        }

        private void ButtonResetInspectionDataClick(object sender, EventArgs e)
        {
            using (LogInForm logInForm = new LogInForm())
            {
                logInForm.ShowDialog(this);

                if (logInForm.GetUserChecked())
                {
                    if (logInForm.GetUserValid())
                    {
                        User user = logInForm.GetUser();

                        if (user.Rank == Rank.Operator)
                        {
                            MessageBox.Show("You do not have clearance to access this menu", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            AddLog(user, "Main Application: Counter reset");

                            ResetCounters();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Badge ID or password invalid", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ButtonDisableSystemClick(object sender, EventArgs e)
        {
            using (LogInForm logInForm = new LogInForm())
            {
                logInForm.ShowDialog(this);

                if (logInForm.GetUserChecked())
                {
                    if (logInForm.GetUserValid())
                    {
                        User user = logInForm.GetUser();

                        if (user.Rank == Rank.Operator)
                        {
                            MessageBox.Show("You do not have clearance to access this menu", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            AddLog(user, "Main Application: System Disabled");

                            systemDisabled = true;

                            labelSelectedProgramName.Text = "Camera System Disabled";
                            labelSelectedProgramName.BackColor = Color.Crimson;

                            selectedProgram = null;

                            HOperatorSet.CloseAllFramegrabbers();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Badge ID or password invalid", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AddLog(User currentUser, string action)
        {
            if (currentUser.IdBadge == null)
            {
                SqlCommunication.InsertLog(string.Empty, action, string.Empty, string.Empty);

            }
            else
            {
                SqlCommunication.InsertLog(currentUser.IdBadge, action, string.Empty, string.Empty);
            }
        }

        private void ButtonDummyTestingClick(object sender, EventArgs e)
        {
            if (!systemDisabled)
            {
                MessageBox.Show("System is not disabled. Dummy testing unavailable", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (LogInForm logInForm = new LogInForm())
                {
                    logInForm.ShowDialog(this);

                    if (logInForm.GetUserChecked())
                    {
                        if (logInForm.GetUserValid())
                        {
                            User user = logInForm.GetUser();

                            if (user.Rank == Rank.Operator)
                            {
                                MessageBox.Show("You do not have clearance to access this menu", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                using (DummyTestingForm dummyTestForm = new DummyTestingForm(user, this))
                                {
                                    dummyTestForm.ShowDialog(this);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Badge ID or password invalid", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void ButtonSelectProgramClick(object sender, EventArgs e)
        {
            if (!systemDisabled)
            {
                MessageBox.Show("Disable the system first, then choose a program", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (ScanForm scanForm = new ScanForm("program"))
            {
                scanForm.ShowDialog(this);

                if (scanForm.GetClosedValue())
                {
                    return;
                }

                ProgramEntry scannedProgram = scanForm.CheckProgram();

                if (scannedProgram.Name == null)
                {
                    MessageBox.Show("Scanned program was not found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    using (ScanForm scanForm1 = new ScanForm("badge"))
                    {
                        scanForm1.ShowDialog(this);

                        if (scanForm1.GetClosedValue())
                        {
                            return;
                        }

                        User scannedUser = scanForm1.CheckUser();

                        if (scannedUser.IdBadge == null)
                        {
                            MessageBox.Show("Scanned badge was not found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {

                            selectedProgram = scannedProgram;

                            labelSelectedProgramName.Text = selectedProgram.Name;
                            labelSelectedProgramName.BackColor = Color.Lime;

                            systemDisabled = false;

                            Task.Factory.StartNew(() =>
                            {
                                plcActions.WriteToPlc(selectedProgram.TriggerOffset, selectedProgram.WasteOffset, selectedProgram.StartWastingOffset, selectedProgram.Camera1, selectedProgram.Camera2, selectedProgram.Machine);
                            });

                            AddLog(scannedUser, $"Main Application: System start");
                        }
                    }
                }
            }
        }

        private void DebugMenuToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (LogInForm logInForm = new LogInForm())
            {
                logInForm.ShowDialog(this);

                if (logInForm.GetUserChecked())
                {
                    if (logInForm.GetUserValid())
                    {
                        User user = logInForm.GetUser();

                        if (user.Rank == Rank.Operator)
                        {
                            MessageBox.Show("You do not have clearance to access this menu", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            using (DebugForm debugForm = new DebugForm(user, plcActions, this))
                            {
                                debugForm.ShowDialog(this);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Badge ID or password invalid", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ManageProgramsToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (LogInForm logInForm = new LogInForm())
            {
                logInForm.ShowDialog(this);

                if (logInForm.GetUserChecked())
                {
                    if (logInForm.GetUserValid())
                    {
                        User user = logInForm.GetUser();

                        if (user.Rank == Rank.Operator)
                        {
                            MessageBox.Show("You do not have clearance to access this menu", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            using (ManageProgramsForm manageProgramsForm = new ManageProgramsForm(this, user))
                            {
                                manageProgramsForm.ShowDialog(this);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Badge ID or password invalid", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void ReconnectCamera1ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (hvAcqHandleCamera1 != null)
            {
                HOperatorSet.CloseFramegrabber(hvAcqHandleCamera1);
            }
            else
            {
                hvAcqHandleCamera1 = null;
            }

            Task.Factory.StartNew(() => OpenFrameGrabberCamera1());
        }

        private void ReconnectCamera2ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (hvAcqHandleCamera2 != null)
            {
                HOperatorSet.CloseFramegrabber(hvAcqHandleCamera2);
            }
            else
            {
                hvAcqHandleCamera2 = null;
            }

            Task.Factory.StartNew(() => OpenFrameGrabberCamera2());
        }

        private bool CheckCamera1Connection(string nameCamera1)
        {
            HOperatorSet.InfoFramegrabber("USB3Vision", "info_boards", out HTuple boardsInfo, out HTuple boardsValue);

            return boardsValue.ToString().Contains(nameCamera1);
        }

        private bool CheckCamera2Connection(string nameCamera2)
        {
            HOperatorSet.InfoFramegrabber("USB3Vision", "info_boards", out HTuple boardsInfo, out HTuple boardsValue);

            return boardsValue.ToString().Contains(nameCamera2);
        }

        private void AbortImageGrab()
        {
            if (hvAcqHandleCamera1 != null && hvAcqHandleCamera1.Length != 0)
            {
                try
                {
                    HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera1, "do_abort_grab", 1);
                }
                catch (Exception)
                {
                }
            }

            if (hvAcqHandleCamera2 != null && hvAcqHandleCamera2.Length != 0)
            {
                try
                {
                    HOperatorSet.SetFramegrabberParam(hvAcqHandleCamera2, "do_abort_grab", 1);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
