using System.Drawing;

namespace TagCloud.Visualizer.Settings
{
    public class DrawSettings : IDrawSettings
    {
        public DrawFormat DrawFormat { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }

        public DrawSettings(DrawFormat drawFormat, Font font, Brush brush)
        {
            DrawFormat = drawFormat;
            Font = font;
            Brush = brush;
        }
    }
}