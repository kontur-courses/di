using System;
using System.Drawing;
using System.Linq;

namespace CloudLayouters
{
    public abstract class BaseCloudLayouter
    {
        protected readonly CloudObjectsContainer Container = new CloudObjectsContainer();
        public string? Name { get; protected set; }
        public virtual Point Center { get; set; }

        public virtual Rectangle PutNextRectangle(Size rectangleSize)
        {
            throw new NotImplementedException();
        }

        public Rectangle[] GetAllRectangles()
        {
            return Container.GetRectangles().ToArray();
        }

        protected bool CouldPutRectangle(Rectangle rectangle)
        {
            return !(Container.GetRectangles().Count > 0 && Container.GetRectangles().Any(rect =>
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
            Container.Clear();
        }
    }
}