using System.Drawing;

namespace TagsCloudVisualization
{
    public static class StringExtensions
    {
        public static Size GetSize(this string word, Font font, Size pictureSize)
        {
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            var image = Graphics.FromImage(bitmap);
            return image.MeasureString(word, font).ToSize();
        }
    }
}