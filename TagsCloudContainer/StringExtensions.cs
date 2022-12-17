using System.Drawing;

namespace TagsCloudContainer
{
    public static class StringExtensions
    {
        public static Size MeasureString(this string s, Font font)
        {
            var fakeImage = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(fakeImage);
            var sizeF = graphics.MeasureString(s, font);
            var size = Size.Ceiling(sizeF);
            return size;
        }
    }
}