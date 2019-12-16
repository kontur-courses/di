using System.Drawing;
using System.IO;

namespace TagCloudPainter
{
    public class PainterConfig
    {
        public int ImageWidth;
        public int ImageHeight;
        public string ImageName;
        
        public int MaxFontSize;
        public int MinFontSize;
        
        public string PathForSave;
        
        public FontFamily FontFamily;
        public Point CloudCenter;

        public Color BackgroundColor;

        public static PainterConfig Default()
        {
            return new PainterConfig()
            {
                ImageWidth = 5000,
                ImageHeight = 5000,
                MaxFontSize = 300,
                MinFontSize = 20,
                ImageName = "TestImage",
                PathForSave = Directory.GetCurrentDirectory(),
                FontFamily = FontFamily.GenericMonospace,
                CloudCenter = new Point(2500, 2500),
                BackgroundColor = Color.Black,
            };
        }
    }
}