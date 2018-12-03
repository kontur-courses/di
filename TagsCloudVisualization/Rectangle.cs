using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Rectangle
    {
        public Point Center { get; set; }
        public Size Size { get; }
        public Point Origin { get; }

        public double LeftXCoord => Center.X - Size.Width / 2;
        public double RightXCoord => Center.X + Size.Width / 2;
        public double TopYCoord => Center.Y + Size.Height / 2;
        public double BottomYCoord => Center.Y - Size.Height / 2;

        public Rectangle(Point origin, Point center, Size size)
        {
            Center = center;
            Size = size;
            Origin = origin;
        }

        public bool Intersects(Rectangle otherRectangle)
        {
            return !(LeftXCoord > otherRectangle.RightXCoord
                     || RightXCoord < otherRectangle.LeftXCoord
                     || TopYCoord < otherRectangle.BottomYCoord
                     || BottomYCoord > otherRectangle.TopYCoord);
        }

        public override string ToString()
        {
            return $"Rectangle (X: {Center.X}; Y: {Center.Y})";
        }
    }
}