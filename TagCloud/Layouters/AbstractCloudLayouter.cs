using System.Drawing;

namespace TagCloud.Layouters
{
    public abstract class AbstractCloudLayouter
    {
        protected readonly PointF center;

        protected AbstractCloudLayouter(PointF center)
        {
            this.center = center;
        }

        public abstract RectangleF PutNextRectangle(SizeF rectangleSize);
    }
}