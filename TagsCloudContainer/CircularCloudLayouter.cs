using System;
using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter
    {
        private Spiral _spiral;
        private List<Rectangle> _rectangles;
        private Point _center;
        
        public CircularCloudLayouter(Point center)
        {
            _center = center;
            _spiral = new Spiral(center, 2);
            _rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize == Size.Empty || rectangleSize.Height < 0 || rectangleSize.Width < 0)
                throw new ArgumentException("The size must not be equal to or less than 0");
            
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(_spiral.NextPoint(), rectangleSize);
            } while (rectangle.IsIntersects(_rectangles));
            
            if(_rectangles.Count != 0)
                rectangle = MoveRectangleToCenter(rectangle);
            
            _rectangles.Add(rectangle);
            return rectangle;
        }
        
        private Rectangle MoveRectangleToCenter(Rectangle newRectangle)
        {
            var shiftX = newRectangle.GetCenter().X < _center.X ? 1 : -1;
            var shiftY = newRectangle.GetCenter().Y < _center.Y ? 1 : -1;
            var isIntersectsByX = false;
            var isIntersectsByY = false;
            while (!isIntersectsByX && !isIntersectsByY)
            {
                shiftX = newRectangle.GetCenter().X < _center.X ? 1 : -1;
                newRectangle = TryMoveRectangleX(newRectangle, shiftX, ref isIntersectsByX);
                shiftY = newRectangle.GetCenter().Y < _center.Y ? 1 : -1;
                newRectangle = TryMoveRectangleY(newRectangle, shiftY, ref isIntersectsByY);
            };
            
            return newRectangle;
        }

        private Rectangle TryMoveRectangleX(Rectangle newRectangle, int x, ref bool isIntersectsByX)
        {
            var shift = new Size(x, 0);
            newRectangle.Location += shift;
            if (newRectangle.IsIntersects(_rectangles))
            {
                isIntersectsByX = true;
                newRectangle.Location -= shift;
            }
            return newRectangle;
        }
        
        private Rectangle TryMoveRectangleY(Rectangle newRectangle, int y, ref bool isIntersectsByY)
        {
            var shift = new Size(0, y);
            newRectangle.Location += shift;
            if (newRectangle.IsIntersects(_rectangles))
            {
                isIntersectsByY = true;
                newRectangle.Location -= shift;
            }
            return newRectangle;
        }
    }
}