using System.Drawing;

namespace TagsCloudVisualization
{
    public class Config
    {
        public Size Size { get; set; }
        public bool Open { get; set; }
        public uint WordCountToStatistic { get; set; }
        public uint Density { get; set; }
        public byte MinWordToStatisticLength { get; set; }
        public string Font { get; set; }
        public string SavePath { get; set; }
        public string SaveFileName { get; set; }
        public Color? Color { get; set; }
        
        public string TextFilePath { get; set; }
        public string? IgnoreFilePath { get; set; }
        
        public TagCloudResultActions TagCloudResultActions { get; set; }
        public SourceTextInterpretationMode  SourceTextInterpretationMode { get; set; }
    }
}