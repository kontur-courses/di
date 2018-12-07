using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualization.CloudGenerating
{
    public class TagsCloudGenerator
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
            var maxCount = wordsStatistics.OrderedWordsStatistics.First().Count;

            foreach (var wordStatistics in wordsStatistics.OrderedWordsStatistics)
            {
                var font = new Font(settings.FontName, maxFontSize * wordStatistics.Count / maxCount);

                var wordSize = TextRenderer.MeasureText(
                    wordStatistics.Word, font);

                var locationRectangle = wordRectanglesLayouter.PutNextRectangle(wordSize);
                yield return new Tag(wordStatistics.Word, locationRectangle, font);
            }
        }
    }
}
