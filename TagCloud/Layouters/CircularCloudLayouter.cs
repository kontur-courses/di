using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Layouters
{
    public class CircularCloudLayouter : IRectangleLayouter
    {
        private Point Center { get; }
        private List<Rectangle> Rectangles => rectangles.ToList();

        private List<Rectangle> rectangles;
        private Spiral spiral;
        
        public CircularCloudLayouter(Point center)
        {
            rectangles = new List<Rectangle>();
            Center = center;
            spiral = new Spiral(Center, 4, 0.005);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            do
            {
                rectangle = GetNextRectangle(rectangleSize);
            } while (IsRectangleIntersectOther(rectangle));
            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            var position = spiral.GetNextPoint();
            switch (spiral.Quadrant)
            {
                case Quadrant.Right:
                    position.Y -= rectangleSize.Height / 2;
                    break;
                case Quadrant.Bottom:
                    position.X -= rectangleSize.Width / 2;
                    position.Y -= rectangleSize.Height;
                    break;
                case Quadrant.Left:
                    position.X -= rectangleSize.Width;
                    position.Y -= rectangleSize.Height / 2;
                    break;
                case Quadrant.Top:
                    position.X -= rectangleSize.Width / 2;
                    break;
            }

            return new Rectangle(position, rectangleSize);
        }

        private bool IsRectangleIntersectOther(Rectangle rectangle)
        {
            foreach (var otherRectangle in rectangles)
                if (rectangle.IntersectsWith(otherRectangle))
                    return true;
            
            return false;
        }
    }
}