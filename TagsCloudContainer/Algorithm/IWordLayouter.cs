using System.Drawing;

namespace TagsCloudContainer.Algorithm
{
    public interface IWordLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}