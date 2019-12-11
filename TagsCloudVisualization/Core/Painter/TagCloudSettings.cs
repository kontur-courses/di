namespace TagsCloudVisualization.Core.Painter
{
    public class TagCloudSettings
    {
        public string TextFilename { get; set; } = "exampleText.txt";
        public string FontFamily { get; set; } = "Calibri";
        public int MinFontSize { get; set; } = 10;      
        public float SpiralAlpha { get; set; } = 1;
        public float StepPhi { get; set; } = 0.05f;
    }
}