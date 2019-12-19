using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Core.Generators;

namespace TagsCloudContainer.Core.Layouters
{
    public class CircularCloudLayouter : IRectangleLayouter
    {
        public const string Name = "circular";
        
        private readonly Point center;
        private readonly IPointGenerator sequence;
        private readonly List<Rectangle> rectangles;

        public IEnumerable<Rectangle> Rectangles => rectangles;

        public CircularCloudLayouter(Point center, IPointGenerator sequence)
        {
            this.center = center;
            this.sequence = sequence;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var shift = new Size(
                center.X - rectangleSize.Width / 2,
                center.Y - rectangleSize.Height / 2
            );
            while (true)
            {
                var location = Point.Truncate(sequence.GetNextPoint()) + shift;
                var rectangle = new Rectangle(location, rectangleSize);
                if (IntersectsWithOthers(rectangle))
                {
                    continue;
                }

                rectangles.Add(rectangle);
                return rectangle;
            }
        }

        private bool IntersectsWithOthers(Rectangle rectangle) =>
            rectangles.Any(rectangle.IntersectsWith);
    }
}