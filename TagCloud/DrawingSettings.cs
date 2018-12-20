using System.Drawing;

namespace TagCloud
{
    public class DrawingSettings
    {
        public Size Size { get; set; }
        public string FontType { get; set; }
        public Brush FontBrush { get; set; }
        public Brush BackgroundBrush { get; set; }

        private DrawingSettings(Size size, string fontType, Brush fontBrush,
            Brush backgroundBrush)
        {
            Size = size;
            FontType = "fontType";
            FontBrush = fontBrush;
            BackgroundBrush = backgroundBrush;
        }

        public static DrawingSettings Default() =>
            new DrawingSettings(
                new Size(3000, 3000),
                "NewTimesRoman",
                Brushes.Black,
                Brushes.White
            );

        public static DrawingSettings BuildValid((int x, int y) size, string fontType, string fontColor,
            string backgroundColor) =>
            new DrawingSettings(
                size.x > 0 && size.y > 0 ? new Size(size.x, size.y) : new Size(3000, 3000),
                fontType ?? "NewTimesRoman",
                fontColor != null
                    ? new SolidBrush(Color.FromName(fontColor))
                    : Brushes.Black,
                backgroundColor != null
                    ? new SolidBrush(Color.FromName(backgroundColor))
                    : Brushes.White
            );
    }
}