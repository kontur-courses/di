using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public class VisualInfo
{
    public Color TextColor { get; set; }
    public Font TextFont { get; set; }
    public RectangleF BoundsRectangle { get; set; }
    public bool IsRotated { get; set; }
}