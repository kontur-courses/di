using System.Collections.Generic;
using System.Drawing;
using TagCloud.Layouters;
using TagCloud.Painters;
using TagCloud.Util;

namespace TagCloud.Visualizers
{
    public class SimpleTagCloudVisualizer : AbstractTagCloudVisualizer
    {
        private readonly Graphics graphics;
        private readonly Bitmap bitmap;

        public SimpleTagCloudVisualizer(AbstractCloudLayouter layouter, ImageSettings imageSettings, IPainter painter)
            : base(layouter, imageSettings, painter)
        {
            bitmap = new Bitmap(imageSettings.Width, imageSettings.Height);
            graphics = Graphics.FromImage(bitmap);
        }

        public override void Render(List<TagStat> tagStats, string pathForImage)
        {
            var resTags = GetResultTags(tagStats);
            painter.SetBackgroundColorFor(graphics);
            foreach (var tag in resTags)
                graphics.DrawTag(tag);
            bitmap.Save(pathForImage, imageSettings.Format);
        }

        private (double fontSizeMultiplier, double averageRepeatsCount) GetFontSizeMultiplierAndAverageRepeatsCount(
            List<TagStat> tagStats)
        {
            var minRepeatsCount = int.MaxValue;
            var maxRepeatsCount = int.MinValue;
            foreach (var tagStat in tagStats)
            {
                if (tagStat.RepeatsCount < minRepeatsCount)
                    minRepeatsCount = tagStat.RepeatsCount;
                if (tagStat.RepeatsCount > maxRepeatsCount)
                    maxRepeatsCount = tagStat.RepeatsCount;
            }

            var fontSizeMultiplier = (double) (imageSettings.MaxFontSize - imageSettings.MinFontSize + 1) /
                                     (maxRepeatsCount - minRepeatsCount + 1);
            var averageRepeatsCount = (double) (minRepeatsCount + maxRepeatsCount) / 2;
            return (fontSizeMultiplier, averageRepeatsCount);
        }

        private List<Tag> GetResultTags(List<TagStat> tagStats)
        {
            var (fontSizeMultiplier, averageRepeatsCount) = GetFontSizeMultiplierAndAverageRepeatsCount(tagStats);
            var res = new List<Tag>();
            foreach (var tagStat in tagStats)
            {
                var tag = CreateTagFrom(tagStat, fontSizeMultiplier, averageRepeatsCount);
                res.Add(tag);
            }

            painter.PaintTags(res);
            return res;
        }

        private Tag CreateTagFrom(TagStat tagStat, double fontSizeMultiplier, double averageWordsCount)
        {
            var fontSizeDelta = (tagStat.RepeatsCount - averageWordsCount) * fontSizeMultiplier;
            var font = imageSettings.DefaultFont.WithModifiedFontSizeOf((float)fontSizeDelta);
            var stringSize = graphics.MeasureString(tagStat.Word, font);
            var tagPlace = layouter.PutNextRectangle(stringSize);
            return new Tag(tagStat, font, tagPlace);
        }
    }
}