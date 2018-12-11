using System.Drawing;

namespace TagCloud.Core.Layouters
{
    public interface ICloudLayouter
    {
        RectangleF PutNextRectangle(SizeF rectangleSize);
        void RefreshWith(PointF newCenterPoint);
    }
}