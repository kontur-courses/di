using System;
using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public static class RectangleExtension
    {
        public static Point GetCenter(this Rectangle rectangle) =>
            new Point(
                rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Height / 2
            );

        public static Rectangle ShiftByAxis(this Rectangle rectangle,
            int offset, Axis axis)
        {
            var shiftedRectangle = new Rectangle(rectangle.Location, rectangle.Size);

            switch (axis)
            {
                case Axis.OX:
                    shiftedRectangle.X += offset;
                    break;
                case Axis.OY:
                    shiftedRectangle.Y += offset;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return shiftedRectangle;
        }
    }
}
