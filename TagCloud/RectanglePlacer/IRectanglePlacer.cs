using System.Drawing;

namespace TagCloud.RectanglePlacer
{
    public interface IRectanglePlacer
    {
        Rectangle PlaceRectangle(Size size, Point startPoint);
    }
}