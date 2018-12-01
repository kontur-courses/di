using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TagsCloudGenerator
    {
        private readonly float maxFontSize = 40;
        private readonly Statistics wordsStatistics;
        private readonly ILayouter wordRectanglesLayouter;

        public TagsCloudGenerator(Statistics wordsStatistics, ILayouter wordRectanglesLayouter)
        {
            this.wordsStatistics = wordsStatistics;
            this.wordRectanglesLayouter = wordRectanglesLayouter;
        }

        public IEnumerable<Tag> GenerateTagsCloud()
        {
            if (wordsStatistics.OrderedWordsStatistics.Count == 0)
                yield break;
            var maxCount = wordsStatistics.OrderedWordsStatistics.First().Count;

            foreach (var wordStatistics in wordsStatistics.OrderedWordsStatistics)
            {
                var font = new Font("Arial", maxFontSize * wordStatistics.Count / maxCount);
                var wordSize = TextRenderer.MeasureText(
                    wordStatistics.Word, font);

                var locationRectangle = wordRectanglesLayouter.PutNextRectangle(wordSize);
                yield return new Tag(wordStatistics.Word, locationRectangle, font);
            }
        }
    }
}
