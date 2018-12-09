using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudVisualization
{
    public class TagCloudImageCreator
    {
        private readonly IEnumerable<IWordDrawer> drawers;

        public TagCloudImageCreator(IEnumerable<IWordDrawer> drawers)
        {
            this.drawers = drawers;
        }

        public virtual Bitmap CreateTagCloudImage(
            IEnumerable<(Rectangle rectangle, string word)> tagCloud,
            ImageCreatingOptions options)
        {
            tagCloud = tagCloud.ToList();
            var rectangles = tagCloud.Select(r => r.rectangle)
                                     .ToList();

            var areaSize = rectangles.GetUnitedSize();

            var width = areaSize.Width;
            var height = areaSize.Height;

            if (options.ImageSize != null)
            {
                width = options.ImageSize.Value.Width;
                height = options.ImageSize.Value.Height;
            }

            var image = new Bitmap(areaSize.Width, areaSize.Height);
            var graphics = Graphics.FromImage(image);

            foreach (var (rectangle, word) in tagCloud)
            {
                var imageCenter = new Point(width / 2, height / 2) - options.Center;
                rectangle.Offset(imageCenter);

                DrawSingleWord(graphics, options, word, rectangle);
            }

            return image;
        }

        private protected void DrawSingleWord(
            Graphics graphics,
            ImageCreatingOptions options,
            string word,
            Rectangle rectangle)
        {
            drawers.Last(d => d.Check(word))
                   .DrawWord(graphics, options, word, rectangle);
        }
    }
}
