using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly IPoints Points;

        public CircularCloudLayouter(IPoints points) =>
            Points = points;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var halfRectangleSize = new Size(rectangleSize.Width / 2, rectangleSize.Height / 2);
            foreach (var point in Points.GetPoints())
            {
                var rectangle = new Rectangle(point - halfRectangleSize, rectangleSize);
                if (rectangles.Any(x => x.IntersectsWith(rectangle)))
                    continue;

                rectangles.Add(rectangle);
                return rectangle;
            }

            return new Rectangle();
        }
    }
}