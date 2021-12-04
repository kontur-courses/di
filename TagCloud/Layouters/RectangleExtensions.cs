using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Layouters
{
    internal static class RectangleExtensions
    {
        internal static Point GetCenter(this Rectangle rectangle)
        {
            var x = rectangle.X + rectangle.Width / 2;
            var y = rectangle.Y + rectangle.Height / 2;
            return new Point(x, y);
        }
    }
}
