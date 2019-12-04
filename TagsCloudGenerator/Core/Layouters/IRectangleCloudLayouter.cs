using System.Drawing;

namespace TagsCloudGenerator.Core.Layouters
{
    public interface IRectangleCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}