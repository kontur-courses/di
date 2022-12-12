using System.Drawing;

namespace TagCloud.Common.Layouter;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
    IEnumerable<Rectangle> GetRectanglesLayout();
    void ClearRectanglesLayout();
}