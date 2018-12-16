using System.Drawing;

namespace TagCloud
{
    public class DrawingSettings
    {
        public Size Size { get; set; }
        public string FontType { get; set; }
        public Brush FontBrush { get; set; }
        public Brush BackgroundBrush { get; set; }

        public static DrawingSettings Default() =>
            new DrawingSettings()
            {
                Size = new Size(3000, 3000),
                FontType = "NewTimesRoman",
                FontBrush = Brushes.Black,
                BackgroundBrush = Brushes.White
            };
    }
}