using System.Drawing;

namespace TagsCloud.Creators.Implementation
{
    public class RectangleCreator : ICreator<Rectangle>
    {
        public Rectangle Place(Point point, Size size)
        {
            return new Rectangle
            {
                X = point.X - size.Width / 2,
                Y = point.Y - size.Height / 2,
                Width = size.Width,
                Height = size.Height
            };
        }
    }
}