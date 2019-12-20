using System.Drawing;
using System.Linq;
using CloudLayouter.Spiral;

namespace CloudLayouter
{
    public class SqueezedCircularCloudLayouter : CircularCloudLayouter
    {
        public SqueezedCircularCloudLayouter(ISpiral spiral) : base(spiral)
        {
        }

        public new Rectangle PutNextRectangle(Size rectangleSize)
        {
            foreach (var point in Spiral.GetPoints(center))
            {
                var rectangle =
                    new Rectangle(new Point(point.X - rectangleSize.Width / 2, point.Y - rectangleSize.Height / 2),
                        rectangleSize);
                if (Rectangles.Any(r => r.Contains(point))) continue;
                if (HasOverlappingRectangles(rectangle, Rectangles)) continue;

                Rectangles.Add(rectangle);
                return rectangle;
            }

            // до суда потенциально не дойдет, т.к. спираль выдает бесконечно точки
            return new Rectangle();
        }
    }
}