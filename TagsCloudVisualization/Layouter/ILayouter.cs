using System.Drawing;

namespace TagsCloudVisualization.Layouter
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}