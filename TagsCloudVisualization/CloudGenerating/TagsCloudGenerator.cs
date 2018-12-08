using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualization.CloudGenerating
{
    public class TagsCloudGenerator : ITagsCloudGenerator
    {
        private readonly float maxFontSize = 40;
        private readonly ILayouterFactory wordRectanglesLayouterFactory;
        private readonly ImageSettings settings;

        public TagsCloudGenerator(ILayouterFactory wordRectanglesLayouterFactory, ImageSettings settings)
        {
            this.wordRectanglesLayouterFactory = wordRectanglesLayouterFactory;
            this.settings = settings;
        }

        public IEnumerable<Tag> GenerateTagsCloud(Statistics wordsStatistics)
        {
            var wordRectanglesLayouter = wordRectanglesLayouterFactory.Create();
            if (wordsStatistics.OrderedWordsStatistics.Count == 0)
                yield break;
            var maxWordCount = wordsStatistics.OrderedWordsStatistics.First().Count;

            foreach (var wordStatistics in wordsStatistics.OrderedWordsStatistics)
                yield return GenerateTagFromWordStatistics(wordStatistics, wordRectanglesLayouter, maxWordCount);
        }

        private Tag GenerateTagFromWordStatistics(
            WordStatistics wordStatistics, ILayouter wordRectanglesLayouter, int maxWordCount)
        {
            var font = new Font(settings.FontName, CalculateFontSize(wordStatistics, maxWordCount));

            var wordSize = TextRenderer.MeasureText(
                wordStatistics.Word, font);

            var locationRectangle = wordRectanglesLayouter.PutNextRectangle(wordSize);
            return new Tag(wordStatistics.Word, locationRectangle, font);
        }

        private float CalculateFontSize(WordStatistics wordStatistics, int maxWordCount)
        {
            return maxFontSize * wordStatistics.Count / maxWordCount;
        }
    }
}
