using System.Collections.Generic;
using System.Drawing;
using TagsCloudTextPreparation;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization
{
    public class Cloud
    {
        private readonly IEnumerable<Tag> tags;
        private readonly Style style;
        private readonly ICloudVisualizer visualizer;

        public Cloud(IEnumerable<FrequencyWord> words, Style style, ICloudVisualizer visualizer,
            ICloudLayouter layouter)
        {
            this.style = style;
            this.visualizer = visualizer;
            tags = GenerateTagsSequence(words, layouter);
        }

        public Bitmap Visualize(int width = 1000, int height = 1000) =>
            visualizer.Visualize(style, tags, width, height);

        private IEnumerable<Tag> GenerateTagsSequence(IEnumerable<FrequencyWord> words, ICloudLayouter cloudLayouter)
        {
            foreach (var word in words)
            {
                var wordSize = style.GetWordSize(word.Count);
                var tagSize = style.WordSizeCalculator.GetTagSize(style.FontProperties, wordSize, word);
                yield return cloudLayouter.PutNextTag(word, tagSize);
            }
        }
    }
}