using System.Drawing;

namespace TagsCloudContainer.Layouter;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
    IEnumerable<Rectangle> GetRectanglesLayout();
    void ClearRectanglesLayout();
}