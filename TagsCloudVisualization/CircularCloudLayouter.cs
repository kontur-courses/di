using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.PointDistributors;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private readonly Cloud cloud;
        private readonly IPointDistributor distributor;

        public CircularCloudLayouter(IPointDistributor type)
        {        
            distributor = type;
            cloud = new Cloud(distributor.GetCenter());
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException();

            if (cloud.Rectangles.Count == 0)
                return AddToCenterPosition(rectangleSize);

            var rectangle = new Rectangle(distributor.GetPosition(), rectangleSize);

            while (HaveIntersection(rectangle))
            {
                rectangle.Location = distributor.GetPosition();
            }

            cloud.Rectangles.Add(rectangle);

            return rectangle;
        }

        private Rectangle AddToCenterPosition(Size rectangleSize)
        {
            var newRectangle = new Rectangle(new Point(cloud.Center.X - rectangleSize.Width / 2,
                cloud.Center.Y - rectangleSize.Height / 2), rectangleSize);

            cloud.Rectangles.Add(newRectangle);

            return newRectangle;
        }

        private bool HaveIntersection(Rectangle newRectangle) =>
            cloud.Rectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle));
    }
}