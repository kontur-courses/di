using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;

namespace TagsCloudVisualization
{
    public static class TagsCloudVisualizer
    {
        public static Bitmap GetPictureOfRectangles(IEnumerable<Tag> tags)
        {
            var tagsList = tags.ToList();
            var pictureRectangle = CalculatePictureRectangle(tagsList.Select(tag => tag.Location).ToList());
            var picture = new Bitmap(pictureRectangle.Width, pictureRectangle.Height);
            using (var graphics = Graphics.FromImage(picture))
            {
                graphics.TranslateTransform(-pictureRectangle.X, -pictureRectangle.Y);
                graphics.Clear(Color.FromArgb(22, 44, 79));
                foreach (var tag in tagsList)
                    DrawTag(graphics, tag);
            }

            return picture;
        }

        private static void DrawTag(Graphics graphics, Tag tag)
        {
            graphics.DrawString(tag.Word, tag.Font, Brushes.Orange, tag.Location);
        }

        private static Rectangle CalculatePictureRectangle(IReadOnlyList<Rectangle> rectangles)
        {
            var minTop = rectangles.Min(r => r.Top);
            var maxBottom = rectangles.Max(r => r.Bottom);
            var minLeft = rectangles.Min(r => r.Left);
            var maxRight = rectangles.Max(r => r.Right);

            return new Rectangle(
                new Point(minLeft, minTop), 
                new Size(maxRight - minLeft, maxBottom - minTop));
        }
    }
}
