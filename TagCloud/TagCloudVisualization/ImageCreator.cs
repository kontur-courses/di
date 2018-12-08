using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudVisualization
{
    public class ImageCreator : ITagCloudImageCreator
    {
        public Bitmap CreateTagCloudImage(
            IEnumerable<(Rectangle rectangle, string word)> tagCloud,
            ImageCreatingOptions options)
        {
            tagCloud = tagCloud.ToList();
            var rectangles = tagCloud.Select(r => r.rectangle)
                                     .ToList();

            var areaSize = rectangles.GetUnitedSize();

            var width = areaSize.Width;
            var height = areaSize.Height;

            var image = new Bitmap(areaSize.Width, areaSize.Height);
            var graphics = Graphics.FromImage(image);

            foreach (var (rectangle, word) in tagCloud)
            {
                var imageCenter = new Point(width / 2, height / 2) - options.Center;
                rectangle.Offset(imageCenter);
                graphics.DrawString(word, options.Font, options.Brush, rectangle);
            }

            return image;
        }
    }
}
