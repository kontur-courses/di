using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.Extensions
{
    static class RectangleExtension
    {
        public static void Move(this ref Rectangle rect, int deltaX, int deltaY)
        {
            rect.Location = new Point(rect.X + deltaX, rect.Y + deltaY);
        }

        public static void MoveToPosition(this ref Rectangle rect, Point newPosition)
        {
            rect.Location = newPosition;
        }
    }
}
