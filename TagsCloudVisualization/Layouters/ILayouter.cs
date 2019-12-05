using System.Drawing;

namespace TagsCloudVisualization.Layouters
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}