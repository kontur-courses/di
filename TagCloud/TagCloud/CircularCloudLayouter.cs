using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class CircularCloudLayouter : TagCloud
    {
        private readonly ICurve curve;

        public CircularCloudLayouter(ICurve curve)
        {
            this.curve = curve;
        }

        public override Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var currentPoint = curve.CurrentPoint;
                var possibleRectangle = new Rectangle(currentPoint, rectangleSize);
                var canFit = Rectangles.All(rect => !rect.IntersectsWith(possibleRectangle));
                curve.Next();
                if (!canFit) continue;
                Rectangles.Add(possibleRectangle);
                return possibleRectangle;
            }
        }
    }
}