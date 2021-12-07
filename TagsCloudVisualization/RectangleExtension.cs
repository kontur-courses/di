using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RectangleExtension
    {
        public static Rectangle Move(this Rectangle source, Point delta)
        {
            return Move(source, delta.X, delta.Y);
        }
        
        public static Rectangle Move(this Rectangle source, int xDelta, int yDelta)
        {
            return new Rectangle(source.X + xDelta, source.Y + yDelta, source.Width, source.Height);
        }
    }
}