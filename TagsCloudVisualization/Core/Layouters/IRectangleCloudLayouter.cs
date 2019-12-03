using System.Drawing;

namespace TagsCloudVisualization.Core.Layouters
{
    public interface IRectangleCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}