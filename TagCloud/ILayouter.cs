using System.Drawing;

namespace TagCloud
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}