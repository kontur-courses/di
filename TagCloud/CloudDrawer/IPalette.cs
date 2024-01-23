using System.Drawing;

namespace TagCloud.CloudDrawer;

public interface IPalette
{
    Color ForegroundColor { get; }
    Color BackgroudColor { get; }
}