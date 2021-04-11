namespace Hirschmann
{
    public class ProgramEntry
    {
        public string Name { get; set; }

        public int TriggerOffset { get; set; }

        public int StartWastingOffset { get; set; }

        public int WasteOffset { get; set; }

        public string ColorCamera1 { get; set; }

        public string ColorCamera2 { get; set; }

        public bool Camera1 { get; set; }

        public bool Camera2 { get; set; }

        public bool LogosCamera1 { get; set; }

        public bool LogosCamera2 { get; set; }

        public int NumberOfLogosCamera1 { get; set; }

        public int NumberOfLogosCamera2 { get; set; }

        public string Logo1Camera1SaveLocation { get; set; }

        public string Logo2Camera1SaveLocation { get; set; }

        public string Logo1Camera2SaveLocation { get; set; }

        public string Logo2Camera2SaveLocation { get; set; }

        public double Logo1Camera1Confidence { get; set; }

        public double Logo2Camera1Confidence { get; set; }

        public double Logo1Camera2Confidence { get; set; }

        public double Logo2Camera2Confidence { get; set; }

        public bool ShapesCamera1 { get; set; }

        public bool ShapesCamera2 { get; set; }

        public string ShapeTypeCamera1 { get; set; }

        public string ShapeTypeCamera2 { get; set; }

        public bool MeasureDistanceCamera1 { get; set; }

        public bool MeasureDistanceCamera2 { get; set; }

        public int MeasureDistanceWidthCamera1 { get; set; }

        public int MeasureDistanceWidthCamera2 { get; set; }

        public int MeasureDistanceHeightCamera1 { get; set; }

        public int MeasureDistanceHeightCamera2 { get; set; }

        public int MeasureDistanceToleranceCamera1 { get; set; }

        public int MeasureDistanceToleranceCamera2 { get; set; }

        public string TextToDetectCamera1 { get; set; }

        public string TextToDetectCamera2 { get; set; }

        public string Machine { get; set; }
    }
}
