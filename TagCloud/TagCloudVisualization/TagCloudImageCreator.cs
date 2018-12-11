using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagCloudVisualization
{
    public class TagCloudImageCreator
    {
        public const float MaxFontSize = 35;
        public const float DefaultFontSize = 5;
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
            tagCloud = SetRectanglesToCloud(tagCloud, options).ToList();

            var areaSize = tagCloud.Select(w => w.Rectangle)
                                   .GetUnitedSize();

            var center = options.Center;

            areaSize = new Size(areaSize.Width+center.X,areaSize.Height+center.Y);
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
                    var imageCenter = new Point(width / 2, height / 2) - center;
                    var rectangle = wordInfo.Rectangle;
                    using (var font = new Font(options.FontName, wordInfo.Scale))
                    {
                        rectangle.Offset(imageCenter);
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
                var font = new Font(options.FontName, wordInfo.Scale);
                var size = TextRenderer.MeasureText(wordInfo.Word, font);
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
