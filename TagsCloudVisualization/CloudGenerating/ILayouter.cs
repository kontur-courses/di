using System.Drawing;

namespace TagsCloudVisualization.CloudGenerating
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
