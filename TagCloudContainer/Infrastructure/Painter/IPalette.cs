using System.Drawing;

namespace TagCloudContainer.Infrastructure.Painter;

public interface IPalette
{
    Color MainColor { get; }
    Color BackgroundColor { get; }
}