using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WordCloudImageGenerator.LayoutCraetion.Layouters;
using WordCloudImageGenerator.Layouting.Cloud;

namespace WordCloudImageGenerator.Layouting.Layouters.Circular
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Point center;
        private readonly SpiralGenerator spiralGenerator;
        public IRectangleCloud RectangleCloud;

        public CircularCloudLayouter(Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Coordinates of the center must be positive numbers");
            this.center = center;
            spiralGenerator = new SpiralGenerator(this.center);
            RectangleCloud = new RectangleCloud();
        }

        public CircularCloudLayouter(IRectangleCloud rectangleCloud)
        {
            if (!rectangleCloud.Rectangles.Any())
                throw new ArgumentException("Tags cloud could not be empty");

            var firstRectangle = rectangleCloud.Rectangles.First();

            var firstRectangleCenter = firstRectangle.GetRectangleCenter();

            if (firstRectangleCenter.X < 0 || firstRectangleCenter.Y < 0)
                throw new ArgumentException("Coordinates of the center must be positive numbers");
            this.center = firstRectangleCenter;
            this.RectangleCloud = rectangleCloud;
            spiralGenerator = new SpiralGenerator(this.center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var nextRectangle = new Rectangle(spiralGenerator.GetNextPositionOnSpiral(), rectangleSize);
            while (nextRectangle.IntersectsWithRectangles(this.RectangleCloud.Rectangles))
                nextRectangle = new Rectangle(spiralGenerator.GetNextPositionOnSpiral(), rectangleSize);
            nextRectangle = MoveToCenter(nextRectangle);
            this.RectangleCloud.Rectangles.Add(nextRectangle);
            return nextRectangle;
        }

        public void Reset() => RectangleCloud = new RectangleCloud(new List<Rectangle>());

        private Rectangle MoveToCenter(Rectangle rectangle)
        {
            while (true)
            {
                var direction = center - new Size(rectangle.GetRectangleCenter());
                var offsetRectangle = MoveRectangleByOnePoint(rectangle, new Point(Math.Sign(direction.X), 0));
                if (offsetRectangle == rectangle)
                    break;

                offsetRectangle = MoveRectangleByOnePoint(offsetRectangle, new Point(0, Math.Sign(direction.Y)));
                if (offsetRectangle == rectangle)
                    break;
                rectangle = offsetRectangle;
            }

            return rectangle;
        }

        private Rectangle MoveRectangleByOnePoint(Rectangle rectangle, Point offset)
        {
            var offsetRectangle = new Rectangle(rectangle.Location + new Size(offset), rectangle.Size);
            return offsetRectangle.IntersectsWithRectangles(RectangleCloud.Rectangles) ? rectangle : offsetRectangle;
        }
    }
}