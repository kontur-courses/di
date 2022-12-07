using System.Collections.Immutable;
using System.Drawing;

namespace TagCloud.Abstractions;

public interface ICloudLayouter
{
    public Point Center { get; }
    public ImmutableArray<Rectangle> Rectangles { get; }
    public Rectangle PutNextRectangle(Size rectangleSize);
}