using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class RectangleComposer
    {
        public Spiral Spiral { get; set; }
        public List<Rectangle> Rectangles { get; set; }
        public static readonly int StepToCenter = 5;
        public static readonly int CenterAreaRadius = 10;

        public RectangleComposer(List<Rectangle> layouterRectangles, Point spiralCenter)
        {
            Spiral = new Spiral(spiralCenter);
            Rectangles = layouterRectangles;
        }

        public Rectangle GetNextRectangleInCloud(Size newRect)
        {
            var location = new Point(Spiral.center.X, Spiral.center.Y);
            var rectangle = new Rectangle(location, newRect);
            var rectOnSpiral = FindFreePlaceOnSpiral(rectangle);
            var centeredRect = MoveToCenter(rectOnSpiral);

            Rectangles.Add(centeredRect);
            return centeredRect;
        }

        public Rectangle FindFreePlaceOnSpiral(Rectangle newRectange)
        {
            foreach (var point in Spiral.GetSpiralPoints())
            {
                var newLoc = new Point(point.X - newRectange.Width / 2, point.Y - newRectange.Height / 2);
                newRectange.Location = newLoc;

                if (!IsRectangleIntersectOther(newRectange, Rectangles))
                {
                    Spiral.ReleasePoint(point);
                    return newRectange;
                }
            }
            
            return Rectangle.Empty;
        }

        public Rectangle MoveToCenter(Rectangle rect)
        {
            var rectCenter = new Point(
                rect.X + rect.Width / 2, 
                rect.Y + rect.Height / 2);

            var delX = rectCenter.X - Spiral.center.X;
            var delY = rectCenter.Y - Spiral.center.Y;
            var angleToCenter = Math.Atan2(delY, delX);

            while (!IsRectangleInCenter(rect, Spiral.center))
            {
                Point nextPoint;

                if (delX >= 0 && delY <= 0) // 1 четверть
                {
                    nextPoint = GetNextPointToCenter(angleToCenter);
                    nextPoint = new Point(-nextPoint.X, nextPoint.Y);
                }
                else if (delX > 0 && delY > 0) // 4 четверть
                {
                    nextPoint = GetNextPointToCenter(angleToCenter);
                    nextPoint = new Point(-nextPoint.X, -nextPoint.Y);
                }
                else if (delX < 0 && delY > 0) // 3 четверть
                {
                    nextPoint = GetNextPointToCenter(Math.PI - angleToCenter);
                    nextPoint = new Point(nextPoint.X, -nextPoint.Y);
                }
                else //(delX < 0 && delY < 0) // 2 четверть
                {
                    nextPoint = GetNextPointToCenter(angleToCenter + Math.PI);
                }

                var newLoc = new Point(rect.Location.X + nextPoint.X, rect.Location.Y + nextPoint.Y);
                var newRect = new Rectangle(newLoc, rect.Size);

                if (IsRectangleIntersectOther(newRect, Rectangles))
                {
                    return rect;
                }

                rect.Location = newLoc;
            }

            return rect;
        }

        public Point GetNextPointToCenter(double angleToCenter)
        {
            var angle = Math.Abs(angleToCenter);
            var angleSin = Math.Sin(angle);
            var yStep = angleSin * StepToCenter;
            var xStep = Math.Sqrt(StepToCenter * StepToCenter - yStep * yStep);
            return new Point((int)xStep, (int)yStep);
        }

        public static bool IsRectangleIntersectOther(Rectangle rect, List<Rectangle> other)
        {
            foreach (var rec in other)
            {
                if (rec.IntersectsWith(rect))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsRectangleInCenter(Rectangle rect, Point center)
        {
            var rectCenterX = rect.X + rect.Width / 2 - center.X;
            var rectCenterY = rect.Y + rect.Height / 2 - center.Y;

            return (Math.Abs(rectCenterX) < CenterAreaRadius &&
                Math.Abs(rectCenterY) < CenterAreaRadius);
        }
    }
}