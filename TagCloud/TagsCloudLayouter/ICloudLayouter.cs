using System.Drawing;

namespace TagsCloudLayouter;

public interface ICloudLayouter
{
    public IReadOnlyList<Rectangle> Rectangles { get; }
    public Rectangle PutNextRectangle(Size rectangleSize);
    public IEnumerable<Rectangle> PutRectangles(IEnumerable<Size> rectangleSizes);
    public void Clear();
}