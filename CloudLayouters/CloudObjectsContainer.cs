using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CloudLayouters
{
    public class CloudObjectsContainer
    {
        public readonly List<Point> FreePoints = new List<Point>();
        public readonly List<Rectangle> Rectangles = new List<Rectangle>();

        public void AddRectangle(Rectangle rectangle)
        {
            Rectangles.Add(rectangle);
            FreePoints.RemoveAll(rectangle.Contains);
        }

        public void AddFreePoint(Point point)
        {
            if (!Rectangles.Any(rectangle => rectangle.Contains(point)))
                FreePoints.Add(point);
        }

        public void Clear()
        {
            FreePoints.Clear();
            Rectangles.Clear();
        }
    }
}