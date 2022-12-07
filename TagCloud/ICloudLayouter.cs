using System.Collections.Immutable;
using System.Drawing;

namespace TagCloud;

public interface ICloudLayouter
{
    public Point Center { get; }
    public ImmutableArray<Rectangle> Rectangles { get; }
    public Rectangle PutNextRectangle(Size rectangleSize);
}