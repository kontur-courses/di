using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Layouters
{
    public class WordCountSizeChooser : IWordSizeChooser
    {
        public Dictionary<Word, Size> GetWordSizes(Word[] words, int wordHeight = 12, float letterToWidthRatio = 12f)
        {
            var sizes = new Dictionary<Word, Size>();
            foreach (var word in words)
                sizes[word] = new Size((int)(word.Value.Length * letterToWidthRatio * word.Amount), wordHeight * word.Amount);
            return sizes;
        }
    }
}