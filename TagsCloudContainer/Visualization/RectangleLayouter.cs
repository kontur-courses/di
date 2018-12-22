using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Infrastructure.PointTracks;

namespace TagsCloudContainer.Visualization
{
    public class RectangleLayouter
    {
        private readonly Point center;
        private readonly List<Rectangle> pastRectangles = new List<Rectangle>();
        private readonly IPointsTrack layoutTrack;

        public RectangleLayouter(Point center, IPointsTrack layoutTrack)
        {
            this.center = center;
            this.layoutTrack = layoutTrack;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("rectangle size should be positive");

            var nextRectangle = PutNextRectangleByTrack(rectangleSize);
            nextRectangle = PullToCenter(nextRectangle);
            pastRectangles.Add(nextRectangle);
            return nextRectangle;
        }

        private Rectangle PullToCenter(Rectangle rectangle)
        {
            rectangle = ShiftRectangleAboutAxisBeforeIntersection(rectangle, Axis.OY);
            rectangle = ShiftRectangleAboutAxisBeforeIntersection(rectangle, Axis.OX);

            return rectangle;
        }

        private Rectangle ShiftRectangleAboutAxisBeforeIntersection(Rectangle rectangle,
            Axis axis)
        {
            var offset = GetRectangleOffsetAboutAxis(rectangle, axis);
            var shiftedRectangle = rectangle.ShiftByAxis(Math.Sign(offset), axis);

            while (offset != 0 && NotIntersectWithPastRectangles(shiftedRectangle))
            {
                rectangle.Location = shiftedRectangle.Location;
                shiftedRectangle = rectangle.ShiftByAxis(Math.Sign(offset), axis);
                offset = GetRectangleOffsetAboutAxis(rectangle, axis);
            }

            return rectangle;
        }

        private int GetRectangleOffsetAboutAxis(Rectangle rectangle, Axis axis)
        {
            var rectangleCenter = rectangle.GetCenter();

            switch (axis)
            {
                case Axis.OX:
                    return center.X - rectangleCenter.X;
                case Axis.OY:
                    return center.Y - rectangleCenter.Y;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool NotIntersectWithPastRectangles(Rectangle rectangle) =>
            !pastRectangles.Select(r => r.IntersectsWith(rectangle)).Any(e => e);    
        
        private Rectangle PutNextRectangleByTrack(Size rectangleSize)
        {
            while (true)
            {
                var point = layoutTrack.GetNextPoint();

                var location = new Point(
                    point.X - rectangleSize.Width / 2,
                    point.Y - rectangleSize.Height / 2);

                var rectangle = new Rectangle(location.X,
                    location.Y, rectangleSize.Width, rectangleSize.Height);
                if (NotIntersectWithPastRectangles(rectangle))
                    return new Rectangle(location.X,
                        location.Y, rectangleSize.Width, rectangleSize.Height);
            }
        }
     }
}