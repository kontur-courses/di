using System.Drawing;

namespace TagsCloudVisualization.CloudLayouters;

public interface ITagsCloudLayouter
{
    Point Center { get; }
    IEnumerable<Rectangle> Rectangles { get; }
    Rectangle PutNextRectangle(Size rectangleSize);
}