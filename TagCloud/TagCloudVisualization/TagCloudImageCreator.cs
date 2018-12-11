using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagCloudVisualization
{
    public class TagCloudImageCreator
    {
        public const float MaxFontSize = 100;
        public const float DefaultFontSize = 2;
        private readonly IEnumerable<IWordDrawer> drawers;
        private readonly Func<Point, CircularCloudLayouter> layouterFactory;

        public TagCloudImageCreator(
            IEnumerable<IWordDrawer> drawers,
            Func<Point, CircularCloudLayouter> layouterFactory)
        {
            this.drawers = drawers;
            this.layouterFactory = layouterFactory;
        }

        public virtual Bitmap CreateTagCloudImage(IEnumerable<WordInfo> tagCloud, ImageCreatingOptions options)
        {
            tagCloud = SetRectanglesToCloud(tagCloud, options)
                .ToList();

            var areaSize = tagCloud.Select(w => w.Rectangle)
                                   .GetUnitedSize();
            areaSize = new Size(areaSize.Width * 2, areaSize.Height * 2);

            var width = areaSize.Width;
            var height = areaSize.Height;

            var center = new Point(areaSize.Width / 2, areaSize.Height / 2);

            if (options.ImageSize != null)
            {
                width = options.ImageSize.Value.Width;
                height = options.ImageSize.Value.Height;
            }

            var image = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(image))
                foreach (var wordInfo in tagCloud)
                {
                    var rectangle = wordInfo.Rectangle;

                    var fontSize = wordInfo.Scale;

                    using (var font = new Font(options.FontName, fontSize))
                    {
                        rectangle.Offset(center);
                        DrawSingleWord(graphics, options, wordInfo, font);
                    }
                }

            return image;
        }

        private IEnumerable<WordInfo> SetRectanglesToCloud(IEnumerable<WordInfo> tagCloud, ImageCreatingOptions options)
        {
            var layouter = layouterFactory.Invoke(options.Center);
            foreach (var wordInfo in tagCloud)
            {
                Size size;
                using (var font = new Font(options.FontName, wordInfo.Scale))
                    size = TextRenderer.MeasureText(wordInfo.Word, font);

                var rectangle = layouter.PutNextRectangle(size);
                yield return wordInfo.With(rectangle);
            }
        }

        private protected void DrawSingleWord(
            Graphics graphics,
            ImageCreatingOptions options,
            WordInfo wordInfo,
            Font font)
        {
            drawers.Last(d => d.Check(wordInfo))
                   .DrawWord(graphics, options, wordInfo, font);
        }
    }
}
