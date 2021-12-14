using System.Drawing;

namespace TagsCloudVisualization
{
    public struct Config
    {
        public Size Size { get; set; }
        public int WordCountToStatistic { get; set; }
        public Point Center { get; set; }
        public double Density { get; set; }
        public byte MinWordToStatisticLength { get; set; }
        public string Font { get; set; }
        public Color? Color { get; set; }
        
        public string TextFilePath { get; set; }
        public string? CustomIgnoreFilePath { get; set; }
        public string DefaultIgnoreFilePath => "ignore.txt";
        
        public TagCloudResultActions TagCloudResultActions { get; set; }
        public SourceTextInterpretationMode  SourceTextInterpretationMode { get; set; }
    }
}