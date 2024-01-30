using System.Drawing;

namespace TagsCloudContainer.Cloud
{
    public class CircularCloudLayouter
    {
        public List<Rectangle> _rectangles;
        private readonly SpiralFunction spiralFunction;
        private readonly Point _center;

        public CircularCloudLayouter(Point center)
        {
            _center = center;
            _rectangles = new List<Rectangle>();
            spiralFunction = new SpiralFunction(center, 2);
        }

        public Rectangle PutNextRectangle(Size sizeRectangle)
        {
            if (sizeRectangle.Width < 0 || sizeRectangle.Height < 0 || sizeRectangle.IsEmpty)
                throw new ArgumentException("Width and Height Size should positive");

            Rectangle rectangle;

            while(true)
            {
                rectangle = new Rectangle(spiralFunction.GetNextPoint(), sizeRectangle);
                if(rectangle.IsIntersectOthersRectangles(_rectangles))
                    break;
            }
            rectangle = MoveRectangleToCenter(rectangle);
            _rectangles.Add(rectangle);
            return rectangle;
        }
        

        private Rectangle MoveRectangleToCenter(Rectangle rectangle)
        {
            
            Rectangle newRectangle;
            newRectangle = MoveRectangleAxis(rectangle, rectangle.GetCenter().X, _center.X, 
                new Point(rectangle.GetCenter().X < _center.X ? 1 : -1, 0));
            newRectangle = MoveRectangleAxis(newRectangle, newRectangle.GetCenter().Y, _center.Y, 
                new Point(0, rectangle.GetCenter().Y < _center.Y ? 1 : -1));
            return newRectangle;
        }

        private Rectangle MoveRectangleAxis(Rectangle newRectangle, int currentPosition, int desiredPosition, Point stepPoint)
        {
            while (newRectangle.IsIntersectOthersRectangles(_rectangles)  &&  desiredPosition != currentPosition)
            {
                currentPosition += currentPosition < desiredPosition ? 1 : -1;
                newRectangle.Location = newRectangle.Location.DecreasingCoordinate(stepPoint);
            }
            
            if (!newRectangle.IsIntersectOthersRectangles(_rectangles))
            {
                newRectangle.Location = newRectangle.Location.IncreasingCoordinate(stepPoint);
            }

            return newRectangle;
        }
    }
}