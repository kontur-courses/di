using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Cloud
{
    public class TagCloudRenderer
    {
        private TagCloud tagCloud;
        private string fontName;
        private Brush brush;


        public TagCloudRenderer(TagCloud tagCloud, string fontName, Brush brush)
        {
            this.tagCloud = tagCloud;
            this.fontName = fontName;
            this.brush = brush;
        }

        public Image GenerateImage()
        {
            var wordTags = tagCloud.Tags;

            var maxX = wordTags.Max(x => x.DescribedRectangle.X + x.DescribedRectangle.Width);
            var maxY = wordTags.Max(x => x.DescribedRectangle.Y + x.DescribedRectangle.Height);
            var image = new Bitmap(maxX + 50, maxY + 50);
            var graphics = Graphics.FromImage(image);

            foreach (var wordTag in wordTags)
            {
                var rectangle = wordTag.DescribedRectangle;
                Font drawFont = new Font(fontName, rectangle.Height / 2);
                graphics.DrawString(wordTag.Word, drawFont, brush, rectangle.X, rectangle.Y);
            }
            return image;
        }
    }
}
