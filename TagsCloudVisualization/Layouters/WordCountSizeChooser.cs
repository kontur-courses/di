using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Layouters
{
    public class WordCountSizeChooser : IWordSizeChooser
    {
        public Dictionary<Word, Size> GetWordSizes(AnalyzedText analyzedText, int wordHeight = 100,
            float letterToWidthRatio = 100f)
        {
            var sizes = new Dictionary<Word, Size>();
            foreach (var word in analyzedText.Words)
            {
                var scale = Math.Sqrt(analyzedText.GetStat(word, StatisticsType.WordCount));
                sizes[word] = new Size((int) (word.Value.Length * letterToWidthRatio * scale),
                    (int) (wordHeight * scale));
            }

            return sizes;
        }
    }
}