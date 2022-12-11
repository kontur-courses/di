using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.CloudLayouters;
using TagCloud.TagCloudVisualizations;
using TagCloud.Tags;
using TagCloud.WordPreprocessors;

namespace TagCloud.TagCloudCreators
{
    public class WordTagCloudCreator : ITagCloudCreator
    {
        private readonly ICloudLayouter.Factory cloudLayouterFactory;
        private ICloudLayouter cloudLayouter;
        private readonly IWordPreprocessor wordPreprocessor;
        private IOrderedEnumerable<KeyValuePair<string, int>> wordsWithRate;
        private readonly ITagCloudVisualizationSettings settings;
        public TagCloud TagCloud { get; private set; }

        public delegate ITagCloudCreator Factory(
            ICloudLayouter.Factory cloudLayouterFactory,
            IWordPreprocessor wordPreprocessor,
            ITagCloudVisualizationSettings settings);

        public WordTagCloudCreator(
            ICloudLayouter.Factory cloudLayouterFactory, 
            IWordPreprocessor wordPreprocessor,
            ITagCloudVisualizationSettings settings)
        {
            if (cloudLayouterFactory == null
                || wordPreprocessor == null
                || settings == null)
            {
                throw new ArgumentNullException(
                    $"{nameof(ICloudLayouter.Factory)}, {nameof(ITagCloudVisualizationSettings)} and {nameof(IWordPreprocessor)} are required for this method");
            }

            this.cloudLayouterFactory = cloudLayouterFactory;
            this.wordPreprocessor = wordPreprocessor;
            this.settings = settings;
            GenerateTagCloud();
        }

        private void GenerateTagCloud()
        {
            cloudLayouter = cloudLayouterFactory.Invoke();
            PrepareWords();
            TagCloud = new TagCloud(cloudLayouter.Center);
            PrepareTagCloud();
        }

        private void PrepareWords()
        {
            var words = wordPreprocessor.GetPreprocessedWords();
            wordsWithRate = words.GroupBy(word => word).
                Select(group => new KeyValuePair<string, int>(group.Key, group.Count())).
                OrderByDescending(group => group.Value);
        }

        private void PrepareTagCloud()
        {
            foreach (var word in wordsWithRate)
            {
                var font = new Font(settings.FontFamilyName, GetFontSize(word.Value, settings.TextScale));
                TagCloud.Layouts.Add(new Word(word.Key, font, cloudLayouter));
            }
        }

        private int GetFontSize(int wordRate, int scale) =>
            (int)Math.Pow(wordRate, 0.5) * scale;
    }
}
