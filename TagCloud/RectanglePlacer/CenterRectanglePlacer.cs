using System.Drawing;

namespace TagCloud.RectanglePlacer
{
    public class CenterRectanglePlacer : IRectanglePlacer
    {
        public Rectangle PlaceRectangle(Size size, Point startPoint)
        {
            return new Rectangle(
                startPoint.X - size.Width / 2,
                startPoint.Y - size.Height / 2,
                size.Width,
                size.Height
            );
        }
    }
}