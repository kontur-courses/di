using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Layouters;
using TagCloud.Core.Painters;
using TagCloud.Core.Settings.Interfaces;
using TagCloud.Core.Util;

namespace TagCloud.Core.Visualizers
{
    public class SimpleTagCloudVisualizer : ITagCloudVisualizer
    {
        private readonly IVisualizingSettings settings;
        private readonly ICloudLayouter layouter;
        private readonly IPainter painter;

        private Bitmap bitmap;
        private Graphics graphics;

        public SimpleTagCloudVisualizer(IVisualizingSettings settings, ICloudLayouter layouter, IPainter painter)
        {
            this.settings = settings;
            this.layouter = layouter;
            this.painter = painter;
        }

        public Bitmap Render(IEnumerable<TagStat> tagStats)
        {
            bitmap = new Bitmap(settings.Width, settings.Height);
            graphics = Graphics.FromImage(bitmap);

            layouter.RefreshWith(settings.CenterPoint);
            var resTags = GetResultTags(tagStats);
            painter.SetBackgroundColorFor(graphics);
            foreach (var tag in resTags)
                graphics.DrawTag(tag);
            return bitmap;
        }

        private (double fontSizeMultiplier, double averageRepeatsCount) GetFontSizeMultiplierAndAverageRepeatsCount(
            IEnumerable<TagStat> tagStats)
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

            var fontSizeMultiplier = (double) (settings.MaxFontSize - settings.MinFontSize + 1) /
                                     (maxRepeatsCount - minRepeatsCount + 1);
            var averageRepeatsCount = (double) (minRepeatsCount + maxRepeatsCount) / 2;
            return (fontSizeMultiplier, averageRepeatsCount);
        }

        private IEnumerable<Tag> GetResultTags(IEnumerable<TagStat> tagStats)
        {
            var tagStatsList = tagStats.ToList();
            var (fontSizeMultiplier, averageRepeatsCount) = GetFontSizeMultiplierAndAverageRepeatsCount(tagStatsList);
            var res = tagStatsList
                .Select(tagStat => CreateTagFrom(tagStat, fontSizeMultiplier, averageRepeatsCount))
                .ToList();

            painter.PaintTags(res);
            return res;
        }

        private Tag CreateTagFrom(TagStat tagStat, double fontSizeMultiplier, double averageWordsCount)
        {
            var fontSizeDelta = (tagStat.RepeatsCount - averageWordsCount) * fontSizeMultiplier;
            var font = settings.DefaultFont.WithModifiedFontSizeOf((float)fontSizeDelta);
            var stringSize = graphics.MeasureString(tagStat.Word, font);
            var tagPlace = layouter.PutNextRectangle(stringSize);
            return new Tag(tagStat, font, tagPlace);
        }
    }
}