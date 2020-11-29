using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CloudLayouters
{
    public class CloudObjectsContainer
    {
        private readonly List<Point> freePoints = new List<Point>();
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public void AddRectangle(Rectangle rectangle)
        {
            rectangles.Add(rectangle);
            freePoints.RemoveAll(rectangle.Contains);
        }

        public void AddFreePoint(Point point)
        {
            if (!rectangles.Any(rectangle => rectangle.Contains(point)))
                freePoints.Add(point);
        }

        public List<Point> GetFreePoints()
        {
            return freePoints.ToList();
        }

        public List<Rectangle> GetRectangles()
        {
            return rectangles.ToList();
        }

        public void Clear()
        {
            freePoints.Clear();
            rectangles.Clear();
        }
    }
}