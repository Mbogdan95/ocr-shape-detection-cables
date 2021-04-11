using HalconDotNet;
using System;
using System.IO;
using System.Windows.Forms;

namespace Hirschmann
{
    class HalconProcedures
    {
        // Procedure variables
        private static HDevEngine hDevEngine = new HDevEngine();
        private static HDevProcedureCall hDevprocedureCallCheckColor;
        private static HDevProcedureCall hDevprocedureCallCreateLogos;
        private static HDevProcedureCall hDevprocedureCallCheckLogos;
        private static HDevProcedureCall hDevProcedureCallCheckText;
        private static HDevProcedureCall hDevProcedureCallCheckShapes;

        /// <summary>
        /// Initialize halcon procedures
        /// </summary>
        public static void InitializeProcedures()
        {
            hDevEngine.SetEngineAttribute("execute_procedures_jit_compiled", "true");
            hDevEngine.SetProcedurePath(Directory.GetCurrentDirectory());

            HDevProcedure hDevProcedureCheckColor = new HDevProcedure("CheckColor");
            hDevprocedureCallCheckColor = new HDevProcedureCall(hDevProcedureCheckColor);

            HDevProcedure hDevProcedureCreateLogos = new HDevProcedure("CreateLogos");
            hDevprocedureCallCreateLogos = new HDevProcedureCall(hDevProcedureCreateLogos);

            HDevProcedure hDevProcedureCheckLogos = new HDevProcedure("CheckLogos");
            hDevprocedureCallCheckLogos = new HDevProcedureCall(hDevProcedureCheckLogos);

            HDevProcedure hDevProcedureCheckText = new HDevProcedure("CheckText");
            hDevProcedureCallCheckText = new HDevProcedureCall(hDevProcedureCheckText);

            HDevProcedure hDevProcedureCheckShapes = new HDevProcedure("CheckShapes");
            hDevProcedureCallCheckShapes = new HDevProcedureCall(hDevProcedureCheckShapes);
        }

        /// <summary>
        /// Checks the color on the cables
        /// </summary>
        /// <param name="hoImage">Image from camera</param>
        /// <returns>What color was detected</returns>
        public static string CheckColor(HObject hoImage)
        {
            hDevprocedureCallCheckColor.SetInputIconicParamObject("Image", hoImage);
            hDevprocedureCallCheckColor.Execute();
            HTuple colorDetected = hDevprocedureCallCheckColor.GetOutputCtrlParamTuple("ColorDetected");

            if (colorDetected.Length != 0)
            {
                return colorDetected;
            }
            else
            {
                return "";
            }
        }

        public static void CreateLogos(HObject hoImage, string logo1SaveLocation, string logo2SaveLocation, string color, int numberOfLogos, double row1Logo1, double column1Logo1, double row2Logo1, double column2Logo1, double row1Logo2, double column1Logo2, double row2Logo2, double column2Logo2)
        {
            try
            {
                hDevprocedureCallCreateLogos.SetInputIconicParamObject("Image", hoImage);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Logo1SaveLocation", logo1SaveLocation);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Logo2SaveLocation", logo2SaveLocation);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Color", color);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("NumberOfLogos", numberOfLogos);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Row1Logo1", row1Logo1);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Column1Logo1", column1Logo1);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Row2Logo1", row2Logo1);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Column2Logo1", column2Logo1);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Row1Logo2", row1Logo2);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Column1Logo2", column1Logo2);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Row2Logo2", row2Logo2);
                hDevprocedureCallCreateLogos.SetInputCtrlParamTuple("Column2Logo2", column2Logo2);
                hDevprocedureCallCreateLogos.Execute();
            }
            catch (Exception)
            {
                MessageBox.Show("Eroare creare logo");
            }

        }

        public static void CheckText(HObject hoImage, string color, int numberOfLogos, HObject logo1Region, HObject logo2Region, out HObject textLines, out HTuple singleCharacters)
        {
            hDevProcedureCallCheckText.SetInputIconicParamObject("Image", hoImage);
            hDevProcedureCallCheckText.SetInputIconicParamObject("Logo1Region", logo1Region);
            hDevProcedureCallCheckText.SetInputIconicParamObject("Logo2Region", logo2Region);
            hDevProcedureCallCheckText.SetInputCtrlParamTuple("Color", color);
            hDevProcedureCallCheckText.SetInputCtrlParamTuple("NumberOfLogos", numberOfLogos);
            hDevProcedureCallCheckText.Execute();

            textLines = hDevProcedureCallCheckText.GetOutputIconicParamObject("TextLines");
            singleCharacters = hDevProcedureCallCheckText.GetOutputCtrlParamTuple("SingleCharacters");
        }

        public static void CheckLogos(HObject hoImage, int numberOfLogos, string color, string logo1SaveLocation, string logo2SaveLocation, out HObject logo1Region, out HObject logo2Region, out HObject logo1Model, out HObject logo2Model, out double logo1Confidence, out double logo2Confidence)
        {
            hDevprocedureCallCheckLogos.SetInputIconicParamObject("Image", hoImage);
            hDevprocedureCallCheckLogos.SetInputCtrlParamTuple("Color", color);
            hDevprocedureCallCheckLogos.SetInputCtrlParamTuple("NumberOfLogos", numberOfLogos);
            hDevprocedureCallCheckLogos.SetInputCtrlParamTuple("Logo1SaveLocation", logo1SaveLocation);
            hDevprocedureCallCheckLogos.SetInputCtrlParamTuple("Logo2SaveLocation", logo2SaveLocation);
            hDevprocedureCallCheckLogos.Execute();

            logo1Region = hDevprocedureCallCheckLogos.GetOutputIconicParamObject("Logo1Region");
            logo2Region = hDevprocedureCallCheckLogos.GetOutputIconicParamObject("Logo2Region");
            logo1Model = hDevprocedureCallCheckLogos.GetOutputIconicParamObject("Logo1Model");
            logo2Model = hDevprocedureCallCheckLogos.GetOutputIconicParamObject("Logo2Model");
            logo1Confidence = hDevprocedureCallCheckLogos.GetOutputCtrlParamTuple("Logo1Confidence");
            logo2Confidence = hDevprocedureCallCheckLogos.GetOutputCtrlParamTuple("Logo2Confidence");
        }

        public static void CheckShapes(HObject hoImage, string color, string shapeType, out HObject selectedShapes, out HTuple checkShapes)
        {
            hDevProcedureCallCheckShapes.SetInputIconicParamObject("Image", hoImage);
            hDevProcedureCallCheckShapes.SetInputCtrlParamTuple("Color", color);
            hDevProcedureCallCheckShapes.SetInputCtrlParamTuple("ShapeType", shapeType);
            hDevProcedureCallCheckShapes.Execute();

            selectedShapes = hDevProcedureCallCheckShapes.GetOutputIconicParamObject("SelectedRegions");
            checkShapes = hDevProcedureCallCheckShapes.GetOutputCtrlParamTuple("CheckShapes");
        }
    }
}
