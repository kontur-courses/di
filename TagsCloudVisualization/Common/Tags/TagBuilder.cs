using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Common.Settings;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Common.Tags
{
    public class TagBuilder : ITagBuilder
    {
        private readonly ITagStyleSettings tagStyleSettings;
        private int colorIndex;
        private int fontIndex;

        public TagBuilder(ITagStyleSettings tagStyleSettings = null)
        {
            this.tagStyleSettings = tagStyleSettings;
            colorIndex = -1;
            fontIndex = -1;
        }

        public IEnumerable<Tag> GetTags(IList<WordStatistic> wordStatistics)
        {
            var allWordsCount = wordStatistics.Sum(stat => stat.Count);
            return wordStatistics.Select(wordStatistic => new Tag(wordStatistic.Text,
                GetNewTagStyle((float) wordStatistic.Count / allWordsCount)));
        }

        private TagStyle GetNewTagStyle(float weight)
        {
            return tagStyleSettings == null ? new TagStyle() : new TagStyle(GetTagColor(), GetTagFont(weight));
        }

        private Color GetTagColor()
        {
            colorIndex = (colorIndex + 1) % tagStyleSettings.ForegroundColors.Length;
            return tagStyleSettings.ForegroundColors[colorIndex];
        }

        private Font GetTagFont(float weight)
        {
            fontIndex = (fontIndex + 1) % tagStyleSettings.FontFamilies.Length;
            return new Font(tagStyleSettings.FontFamilies[fontIndex],
                tagStyleSettings.Size - tagStyleSettings.SizeScatter + tagStyleSettings.SizeScatter * 2 * weight * 10);
        }
    }
}