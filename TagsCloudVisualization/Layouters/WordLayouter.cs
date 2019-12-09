using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters.CloudLayouters;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Layouters
{
    public class WordLayouter
    {
        private readonly ICloudLayouter layouter;
        private readonly IWordSizeChooser sizeChooser;

        public WordLayouter(ICloudLayouter cloudLayouter, IWordSizeChooser sizeChooser)
        {
            layouter = cloudLayouter;
            this.sizeChooser = sizeChooser;
        }

        public AnalyzedLayoutedText GetLayoutedText(AnalyzedText analyzedText)
        {
            var sizes = sizeChooser.GetWordSizes(analyzedText);
            var layout = new Dictionary<Word, Rectangle>();
            foreach (var wordSizePair in sizes)
                layout[wordSizePair.Key] = layouter.PutNextRectangle(wordSizePair.Value);
            return analyzedText.ToLayoutedText(layout
                .Select(x => new LayoutedWord(x.Key, x.Value))
                .ToArray());
        }
    }
}