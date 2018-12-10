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

        public virtual Bitmap CreateTagCloudImage(IEnumerable<WordInfo> tagCloud, ImageCreatingOptions options)
        {
            tagCloud = tagCloud.ToList();
            var rectangles = tagCloud.Select(r => r.Rectangle)
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
            using (var graphics = Graphics.FromImage(image))
                foreach (var wordInfo in tagCloud)
                {
                    var imageCenter = new Point(width / 2, height / 2) - options.Center;
                    var rectangle = wordInfo.Rectangle;
                    rectangle.Offset(imageCenter);

                    DrawSingleWord(graphics, options, wordInfo.With(rectangle));
                }

            return image;
        }

        private protected void DrawSingleWord(Graphics graphics, ImageCreatingOptions options, WordInfo wordInfo)
        {
            drawers.Last(d => d.Check(wordInfo))
                   .DrawWord(graphics, options, wordInfo);
        }
    }
}
