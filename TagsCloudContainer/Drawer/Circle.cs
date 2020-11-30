using System.Drawing;

namespace TagsCloudContainer.Drawer
{
    public class Circle
    {
        public int Radius { get; }
        public Point Center { get; }

        public Circle(Point center, int radius)
        {
            Radius = radius;
            Center = center;
        }
    }
}