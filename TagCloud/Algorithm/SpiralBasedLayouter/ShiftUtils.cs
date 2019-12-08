using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class ShiftUtils
    {
        public static Point GetShiftToTheFirstQuadrant(Point center, List<Rectangle> rectangles)
        {
            var dx = center.X - rectangles.Select(r => r.Left).Min();
            var dy = center.Y - rectangles.Select(r => r.Top).Min();
            return new Point(dx < 0 ? center.X : dx, dy < 0 ? center.Y : dy);
        }

        public static Point ShiftPoint(Point original, Point shift, int increaseParameter)
        {
            return new Point(
                (original.X + shift.X) * increaseParameter, 
                (original.Y + shift.Y) * increaseParameter);
        }

        public static Size IncreaseSize(Size original, int increaseParameter)
        {
            return new Size(
                original.Width * increaseParameter,
                original.Height * increaseParameter
                );
        }

        public static Rectangle GetShiftedAndResizedRectangle(Rectangle rectangle, Point shift, int increaseParameter)
        {
            return new Rectangle(
                ShiftPoint(rectangle.Location, shift, increaseParameter),
                IncreaseSize(rectangle.Size, increaseParameter)
            );
        }
    }
}
