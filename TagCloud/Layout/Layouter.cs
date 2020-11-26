using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class Layouter : ILayouter
    {
        private readonly List<Rectangle> rectangles;
        private readonly ISpiral spiral;
        
        
        public Layouter(ISpiral spiral)
        {
            rectangles = new List<Rectangle>();
            this.spiral = spiral;
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var point = spiral.GetNextPoint();
                var newRectangle = GetRectangleByCenter(rectangleSize, point);
                
                if (!RectangleIntersectWithOthers(newRectangle))
                {
                    rectangles.Add(newRectangle);
                    return newRectangle;
                }
            }
        }

        private Rectangle GetRectangleByCenter(Size rectangleSize, Point rectangleCenter)
        {
            var leftTopAngle = new Point(rectangleCenter.X - rectangleSize.Width / 2,
                rectangleCenter.Y - rectangleSize.Height / 2);
            return new Rectangle(leftTopAngle, rectangleSize);
        }
        
        private bool RectangleIntersectWithOthers(Rectangle checkedRectangle)
        {
            foreach (var rectangle in rectangles)
            {
                if (rectangle.IntersectsWith(checkedRectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}