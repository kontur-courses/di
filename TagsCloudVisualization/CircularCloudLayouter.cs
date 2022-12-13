using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Curves;
using TagsCloudVisualization.Structures;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Rectangle maxCanvas;
        public readonly Size MaxCanvasSize = new(90000, 90000);
        private readonly Point quadTreeCenter;
        private readonly IEnumerator<Point> spiral;
        public readonly List<Point> SpiralPoints;

        public CircularCloudLayouter(Point center)
        {
            quadTreeCenter = new Point(MaxCanvasSize.Width / 2 + center.X, MaxCanvasSize.Height / 2 - center.Y);
            maxCanvas = new Rectangle(new Point(0, 0), MaxCanvasSize);
            spiral = new Spiral(quadTreeCenter, Math.PI / 360).GetEnumerator();
            SpiralPoints = new List<Point>();
            QuadTree = new QuadTree(maxCanvas);
        }

        protected QuadTree QuadTree { get; }

        private Rectangle GetRectangleAtPositionOfCenter(Point position, Size rectangleSize)
        {
            return new Rectangle(position - rectangleSize / 2, rectangleSize);
        }

        private Point CalcOriginalPos(Point offsetPosition)
        {
            var x = offsetPosition.X - MaxCanvasSize.Width / 2;
            var y = offsetPosition.Y - MaxCanvasSize.Height / 2;
            return new Point(x, y);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Given rectangle size must be positive");

            while (spiral.MoveNext())
            {
                var rectangle = GetRectangleAtPositionOfCenter(spiral.Current, rectangleSize);
                SpiralPoints.Add(CalcOriginalPos(spiral.Current));

                if (QuadTree.IntersectsWith(rectangle))
                    continue;

                CheckIfRectangleIsOutsideOfCanvas(rectangle);

                for (var i = 0; i < 4; i++)
                    rectangle = TryShiftToCenter(rectangle, i % 2 == 0);

                QuadTree.Insert(rectangle);

                rectangle.Location = CalcOriginalPos(rectangle.Location);
                return rectangle;
            }

            throw new Exception("Given rectangle couldn't be placed");
        }

        private void CheckIfRectangleIsOutsideOfCanvas(Rectangle rectangle)
        {
            var copy = rectangle;
            copy.Intersect(maxCanvas);
            if (copy != rectangle)
                throw new Exception("Rectangle was placed out side of canvas");
        }

        private Rectangle TryShiftToCenter(Rectangle rectangle, bool isVertical)
        {
            var oldDistance = rectangle.Center().GetDistanceSquareTo(quadTreeCenter);
            var oldLocation = rectangle.Location;
            while (true)
            {
                var newLocation = oldLocation;

                if (isVertical)
                    newLocation.Y += Math.Sign(quadTreeCenter.Y - rectangle.Center().Y);
                else
                    newLocation.X += Math.Sign(quadTreeCenter.X - rectangle.Center().X);


                var newRectangle = new Rectangle(newLocation, rectangle.Size);
                var newDistance = newRectangle.Center().GetDistanceSquareTo(quadTreeCenter);

                if (newDistance >= oldDistance)
                    break;

                if (!QuadTree.IntersectsWith(newRectangle))
                    rectangle = newRectangle;


                oldLocation = rectangle.Location;
                oldDistance = newDistance;
            }

            return rectangle;
        }
    }
}