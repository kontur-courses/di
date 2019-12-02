using System.Drawing;

namespace TagsCloudVisualization.Core
{
    public interface IRectangleCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}