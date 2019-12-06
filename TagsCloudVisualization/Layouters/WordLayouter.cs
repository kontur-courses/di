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
        private readonly IWordSizeChooser sizeChooser;

        public WordLayouter(ICloudLayouter cloudLayouter, IWordSizeChooser sizeChooser) //TODO: Add resizing to fit screen size
        {
            layouter = cloudLayouter;
            this.sizeChooser = sizeChooser;
        }

        public LayoutedWord[] GetLayoutedWords(Word[] words)
        {
            var sizes = sizeChooser.GetWordSizes(words);
            var layout = new Dictionary<Word, Rectangle>();
            foreach (var wordSizePair in sizes)
                layout[wordSizePair.Key] = layouter.PutNextRectangle(wordSizePair.Value);
            return layout.Select(x => new LayoutedWord(x.Key, x.Value)).ToArray();
        }
    }
}