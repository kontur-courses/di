using System.Drawing;

namespace TagsCloudVisualization
{
    public class Settings
    {
        public int MaxFontSize { get; init; }
        public string[] BoringWords { get; init; }
        public string FileWithWords { get; init; }
        public Point StartPoint { get; init; }
        public Color TagColor { get; init; }
        public string FontFamilyName { get; init; }
        public string Directory { get; init; }
        public string ImageName { get; init; }
    }
}