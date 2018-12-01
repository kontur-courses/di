using System.Drawing;

namespace TagCloudLayouter
{
    public class Config
    {
        public Config(Point center, string inputFile, int count, Font font, string fileName, string outPath, Color color, Color backgroundColor)
        {
            Center = center;
            InputFile = inputFile;
            Count = count;
            Font = font;
            FileName = fileName;
            OutPath = outPath;
            Color = color;
            BackgroundColor = backgroundColor;
        }

        public Point Center { get; }
        public string InputFile { get; }
        public int Count { get; }
        public Font Font { get; }
        public string FileName { get; }
        public string OutPath { get; }
        public Color Color { get; }
        public Color BackgroundColor { get; }
    }
}
