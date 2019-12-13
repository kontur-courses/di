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
        private readonly CloudLayouterConfiguration cloudLayouterConfiguration;
        private readonly IWordSizeChooser sizeChooser;

        public WordLayouter(CloudLayouterConfiguration cloudConfiguration, IWordSizeChooser sizeChooser)
        {
            cloudLayouterConfiguration = cloudConfiguration;
            this.sizeChooser = sizeChooser;
        }

        public AnalyzedLayoutedText GetLayoutedText(AnalyzedText analyzedText)
        {
            var layouter = cloudLayouterConfiguration.GetCloudLayouter();
            var sizes = sizeChooser.GetWordSizes(analyzedText, 20, 20);
            var layout = new Dictionary<Word, Rectangle>();
            foreach (var wordSizePair in sizes.OrderByDescending(x => x.Value.Width))
                layout[wordSizePair.Key] = layouter.PutNextRectangle(wordSizePair.Value);
            return analyzedText.ToLayoutedText(layout
                .Select(x => new LayoutedWord(x.Key, x.Value))
                .ToArray());
        }
    }
}