using System.Drawing;

namespace TagsCloud.App
{
    public static class ImageSizeExtensions
    {
        public static Point GetCenter(this ImageSize size)
        {
            return new Point(size.Width / 2, size.Height / 2);
        }
    }
}