using System.Drawing;

namespace TagsCloudLayouter;

public interface ICloudLayouter
{
    public IReadOnlyList<Rectangle> Rectangles { get; }
    public Rectangle PutNextRectangle(Size rectangleSize);
}