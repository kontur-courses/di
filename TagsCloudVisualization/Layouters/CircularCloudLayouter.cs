using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ResultProject;

namespace TagsCloudVisualization.Layouters
{
    internal class CircularCloudLayouter : ILayouter<Rectangle>
    {
        private readonly IEnumerator<Point> pointSpiral;
        private readonly HashSet<Rectangle> issuedRectangles;

        public CircularCloudLayouter(IInfinityPointsEnumerable spotPoints)
        {
            issuedRectangles = new HashSet<Rectangle>();
            pointSpiral = spotPoints
                .GetPoints()
                .GetEnumerator();
        }

        public Result<Rectangle> PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                return Result.Fail<Rectangle>("Width and Height should be positive");
            
            Rectangle rectangle;
            
            do
            {
                pointSpiral.MoveNext();
                var point = pointSpiral.Current;
                rectangle = new Rectangle(
                    point.X - rectangleSize.Width / 2, 
                    point.Y - rectangleSize.Height / 2,
                    rectangleSize.Width,
                    rectangleSize.Height);
            } while (issuedRectangles.Any(x => x.IntersectsWith(rectangle)));

            issuedRectangles.Add(rectangle);
            return rectangle;
        }
    }
}