using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud
{
    public class DefaultDrawerSettings : IDrawerSettings
    {
        public Point Center => Point.Empty;
        public HashSet<Color> Colors { get; }
        public Font Font => new Font(FontFamily.GenericSerif, 20);
        public Size Size { get; }
        public ImageFormat Format => ImageFormat.Png;
    }
}