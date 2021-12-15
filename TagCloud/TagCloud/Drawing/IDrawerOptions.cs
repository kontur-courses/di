using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Drawing
{
    public interface IDrawerOptions
    {
        Point Center { get; }
        IEnumerable<Color> WordColors { get; }
        Color BackgroundColor { get; }
        FontFamily FontFamily { get; }
        float BaseFontSize { get; }
        Size Size { get; }
        ImageFormat? Format { get; }
        string Path { get; }
        string FileName { get; }
    }
}