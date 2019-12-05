using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters.CloudLayouters;

namespace TagsCloudVisualization.Layouters
{
    public class WordLayouter
    {
        private readonly ICloudLayouter layouter;
        private readonly Dictionary<Word, Size> wordSizes;

        public WordLayouter(Word[] words, ICloudLayouter cloudLayouter, int wordHeight = 12, float letterToWidthRatio = 12f) //TODO: Add resizing to fit screen size
        {
            layouter = cloudLayouter;
            wordSizes = DefineWordSizes(words, wordHeight, letterToWidthRatio);
        }

        private static Dictionary<Word, Size> DefineWordSizes(Word[] words, int wordHeight, float letterToWidthRatio)
        {
            var sizes = new Dictionary<Word, Size>();
            foreach (var word in words)
                sizes[word] = new Size((int) (word.Value.Length * letterToWidthRatio * word.Amount), wordHeight * word.Amount);
            return sizes;
        }

        public LayoutedWord[] GetLayoutedWords()
        {
            var layout = new Dictionary<Word, Rectangle>();
            foreach (var wordSizePair in wordSizes)
                layout[wordSizePair.Key] = layouter.PutNextRectangle(wordSizePair.Value);
            return layout.Select(x => new LayoutedWord(x.Key, x.Value)).ToArray();
        }
    }
}