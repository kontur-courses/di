using System.Drawing;

namespace TagCloud
{
    public interface ICircularCloudLayouter
    {
        RectangleF PutNextRectangle(SizeF rectangleSize, Point center);
        void Clear();
    }
}