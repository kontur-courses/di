using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CloudLayouters
{
    public abstract class BaseCloudLayouter
    {
        protected readonly List<Point> FreePoints = new List<Point>();
        protected readonly List<Rectangle> Rectangles = new List<Rectangle>();
        public string? Name { get; protected set; }
        public virtual Point Center { get; set; }

        public abstract Rectangle PutNextRectangle(Size rectangleSize);

        public Rectangle[] GetAllRectangles()
        {
            return Rectangles.ToArray();
        }

        protected bool CouldPutRectangle(Rectangle rectangle)
        {
            return !(Rectangles.Count > 0 && Rectangles.Any(rect =>
                rect.IntersectsWith(rectangle)));
        }

        protected static Rectangle GetRectangleWithCenterInPoint(Point point, Size rectangleSize)
        {
            point.X -= rectangleSize.Width / 2;
            point.Y -= rectangleSize.Height / 2;
            return new Rectangle(point, rectangleSize);
        }

        public virtual void ClearLayout()
        {
            ClearContainer();
        }


        protected void AddRectangle(Rectangle rectangle)
        {
            Rectangles.Add(rectangle);
            FreePoints.RemoveAll(rectangle.Contains);
        }

        protected void AddFreePoint(Point point)
        {
            if (!Rectangles.Any(rectangle => rectangle.Contains(point)))
                FreePoints.Add(point);
        }

        protected void ClearContainer()
        {
            FreePoints.Clear();
            Rectangles.Clear();
        }
    }
}