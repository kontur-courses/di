using System.Collections.Generic;
using System.Drawing;

namespace Extensions
{
    public static class RectangleExtensions
    {
        public static Point Center(this Rectangle rectangle)=>
            rectangle.Location + rectangle.Size.Divide(2);
        
        public static IEnumerable<Point> Points(this Rectangle rect)
        {
            yield return rect.Location;
            yield return rect.Location.AddWidth(rect.Size.Width);
            yield return rect.Location.AddHeight(rect.Size.Height);
            yield return rect.Location + rect.Size;
        }

    }
}