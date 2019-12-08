using System.Drawing;
using System.IO;

namespace TagCloud
{
    public class DefaultPainterConfig : IPainterConfig
    {
        public int ImageWidth => 1000;
        public int ImageHeight => 1000;
        public int MaxFontSize => 70;
        public int MinFontSize => 20;
        public string ImageName => "DefaultImage";
        public string PathForSave => Directory.GetCurrentDirectory();
        public FontFamily FontFamily => FontFamily.GenericMonospace;
        public Point CloudCenter => new Point(500, 500);
    }
}