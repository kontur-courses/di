using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud
{
    public interface IDrawerSettings
    {
        Point Center { get; }
        HashSet<Color> Colors { get; }
        Font Font { get; }
        Size Size { get; }
        ImageFormat Format { get; }
    }
}