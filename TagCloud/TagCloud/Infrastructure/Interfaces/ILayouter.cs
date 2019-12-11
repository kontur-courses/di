using System.Drawing;

namespace TagCloud
{
    public interface ILayouter
    {
        RectangleF PutNextRectangle(SizeF rectangleSize);
        void Reset();
    }
}
