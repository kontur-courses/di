using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Layouters
{
    public class WordCountSizeChooser : IWordSizeChooser
    {
        public Dictionary<Word, Size> GetWordSizes(AnalyzedText analyzedText, int wordHeight = 12, float letterToWidthRatio = 12f)
        {
            var sizes = new Dictionary<Word, Size>();
            foreach (var word in analyzedText.Words)
                sizes[word] = new Size((int)(word.Value.Length * letterToWidthRatio * analyzedText.GetStat(word, StatisticsType.WordCount)),
                    wordHeight * analyzedText.GetStat(word, StatisticsType.WordCount));
            return sizes;
        }
    }
}