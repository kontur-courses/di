using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Cloud
{
    public class TagCloudRenderer
    {
        public static Image GenerateImage(TagCloud tagCloud, string fontName, Brush brush)
        {
            var maxX = 0;
            var maxY = 0;
            var maxWidth = 0;
            var maxHeight = 0;

            var listOfWordTags = tagCloud.Tags.ToList();
            foreach (var wordTag in listOfWordTags)
            {
                var rectangle = wordTag.DescribedRectangle;
                maxX = Math.Max(maxX, rectangle.X);
                maxY = Math.Max(maxY, rectangle.Y);
                maxWidth = Math.Max(maxWidth, rectangle.Width);
                maxHeight = Math.Max(maxHeight, rectangle.Height);
            }

            var image = new Bitmap(maxX + maxWidth + 50, maxY + maxHeight + 50);
            var graphics = Graphics.FromImage(image);

            foreach (var wordTag in listOfWordTags)
            {
                var rectangle = wordTag.DescribedRectangle;
                Font drawFont = new Font(fontName, rectangle.Height / 2);
                graphics.DrawString(wordTag.Word, drawFont, brush, rectangle.X, rectangle.Y);
            }
            return image;
        }
    }
}
