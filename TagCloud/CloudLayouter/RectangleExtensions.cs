using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            var centerPoint = rectangle.Location.MoveOn(rectangle.Width / 2, rectangle.Height / 2);

            return centerPoint;
        }

        public static Rectangle MoveOn(this Rectangle rectangle, int deltaX, int deltaY)
        {
            var movedLocation = rectangle.Location.MoveOn(deltaX, deltaY);

            return new Rectangle(movedLocation, rectangle.Size);
        }
    }
}