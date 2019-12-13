using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Interfaces;
using TagsCloud.TagGenerators;

namespace TagsCloud.CloudDrawers
{
    public class DefaultCloudDrawer : ICloudDrawer
    {
        public Image Paint(IEnumerable<(Tag tag, Rectangle position)> resultTagCloud, Size imageSize, Color backgroundColor, int widthOfBorder = 0)
        {
            var borderOfRectangles = GetBorderOfRectangles(resultTagCloud);
            var image = new Bitmap(borderOfRectangles.Width + widthOfBorder * 2, borderOfRectangles.Height + widthOfBorder * 2);
            using (var graph = Graphics.FromImage(image))
            {
                graph.Clear(backgroundColor);
                foreach (var tag in resultTagCloud)
                {
                    using (var brush = new SolidBrush(tag.tag.colorTag))
                    using (var font = new Font(tag.tag.font.fontName, tag.tag.font.fontSize))
                        graph.DrawString(tag.tag.word, font, brush, tag.position.Left - borderOfRectangles.X + widthOfBorder, tag.position.Top - borderOfRectangles.Y + widthOfBorder);
                }
                image = new Bitmap(image, imageSize);
            }
            return image;
        }

        private static Rectangle GetBorderOfRectangles(IEnumerable<(Tag tag, Rectangle position)> rectangles)
        {
            if (rectangles.Count() == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(rectangles));
            var maxX = rectangles.Max(rectangle => rectangle.position.Right);
            var maxY = rectangles.Max(rectangle => rectangle.position.Bottom);
            var minX = rectangles.Min(rectangle => rectangle.position.Left);
            var minY = rectangles.Min(rectangle => rectangle.position.Top);
            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }
    }
}
