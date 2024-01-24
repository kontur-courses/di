using System.Drawing;

namespace TagCloud.Drawer;

public interface IPalette
{
    Color ForegroundColor { get; }
    Color BackgroudColor { get; }
}