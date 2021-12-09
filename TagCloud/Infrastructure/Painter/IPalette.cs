using System.Drawing;

namespace TagCloud.Infrastructure.Painter;

public interface IPalette
{
    Color MainColor { get; }
    Color BackgroundColor { get; }
}