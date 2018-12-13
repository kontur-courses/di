using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Configuration
{
    public class SimpleConfiguration : IConfiguration
    {
        public string PathToWordsFile { get; set; }

        public string BoringWordsFileName { get; set; }

        public string DirectoryToSave { get; set; }

        public string OutFileName { get; set; }

        public FontFamily FontFamily { get; set; }

        public Color Color { get; set; }

        public int MinFontSize { get; set; }

        public int MaxFontSize { get; set; }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public ImageFormat ImageFormat { get; set; }

        public int RotationAngle { get; set; }

        public int CenterX { get; set; }

        public int CenterY { get; set; }
    }
}