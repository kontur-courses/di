using System.Drawing;
using System.IO;

namespace TagCloudPainter
{
    public class PainterConfig
    {
        public int ImageWidth;
        public int ImageHeight;
        public int MaxFontSize;
        public int MinFontSize;
        public string ImageName;
        public string PathForSave;
        public FontFamily FontFamily;
        public Point CloudCenter;

        public static PainterConfig Default()
        {
            return new PainterConfig()
            {
                ImageWidth = 1000,
                ImageHeight = 1000,
                MaxFontSize = 70,
                MinFontSize = 20,
                ImageName = "TestImage",
                PathForSave = Directory.GetCurrentDirectory(),
                FontFamily = FontFamily.GenericMonospace,
                CloudCenter = new Point(500, 500),
            };
        }
    }
}