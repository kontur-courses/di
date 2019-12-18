using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Data;
using TagsCloudContainer.Data.Processors;
using TagsCloudContainer.Data.Readers;
using TagsCloudContainer.Visualization;
using TagsCloudContainer.Visualization.Measurers;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer.Cloud
{
    public class TagsCloudGenerator
    {
        private readonly IRectangleLayouter layouter;
        private readonly IWordsFileReader wordReader;
        private readonly IEnumerable<IWordProcessor> processors;
        private readonly IWordMeasurer wordMeasurer;
        private readonly IPainter painter;
        private readonly TagsCloudVisualizer visualizer;

        public TagsCloudGenerator(
            IWordsFileReader wordReader,
            IEnumerable<IWordProcessor> processors,
            IWordMeasurer wordMeasurer,
            IRectangleLayouter layouter,
            IPainter painter,
            TagsCloudVisualizer visualizer)
        {
            this.layouter = layouter;
            this.wordReader = wordReader;
            this.processors = processors;
            this.wordMeasurer = wordMeasurer;
            this.painter = painter;
            this.visualizer = visualizer;
        }

        public Image Create(TagsCloudSettings settings)
        {
            var words = processors.Aggregate(wordReader.ReadAllWords(settings.WordsPath),
                (current, processor) => processor.Process(current)).ToArray();
            var tags = WordCounter.Count(words)
                .Select(word =>
                {
                    var (font, size) = wordMeasurer.Measure(word);
                    return new Tag(word.Value, font, layouter.PutNextRectangle(size));
                })
                .ToArray();
            return visualizer.Visualize(painter.Colorize(tags));
        }
    }
}