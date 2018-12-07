using System.Drawing;
using TagCloud.Visualizer.Settings.Colorizer;

namespace TagCloud.Visualizer.Settings
{
    public class DrawSettings : IDrawSettings
    {
        public DrawFormat DrawFormat { get; set; }
        public Font Font { get; set; }
        public Color Color { get; set; }
        public IColorizer Colorizer { get; set; }

        public DrawSettings(DrawFormat drawFormat, Font font, Color color, IColorizer colorizer)
        {
            DrawFormat = drawFormat;
            Font = font;
            Color = color;
            Colorizer = colorizer;
        }
    }
}