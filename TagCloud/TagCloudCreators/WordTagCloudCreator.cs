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
        private readonly IWordPreprocessor wordPreprocessor;
        private readonly ITagCloudVisualizationSettings settings;

        private ICloudLayouter cloudLayouter;
        private IOrderedEnumerable<KeyValuePair<string, int>> wordsWithRate;
        private ITagCloud tagCloud;

        public WordTagCloudCreator(
            ICloudLayouter.Factory cloudLayouterFactory, 
            IWordPreprocessor wordPreprocessor,
            ITagCloudVisualizationSettings settings)
        {
            this.cloudLayouterFactory = cloudLayouterFactory;
            this.wordPreprocessor = wordPreprocessor;
            this.settings = settings;
        }

        public ITagCloud GenerateTagCloud()
        {
            PrepareWords();
            return PrepareTagCloud();
        }

        private void PrepareWords()
        {
            cloudLayouter = cloudLayouterFactory.Invoke();
            var words = wordPreprocessor.GetPreprocessedWords();
            wordsWithRate = words.GroupBy(word => word).
                Select(group => new KeyValuePair<string, int>(group.Key, group.Count())).
                OrderByDescending(group => group.Value);
        }

        private ITagCloud PrepareTagCloud()
        {
            tagCloud = new TagCloud(cloudLayouter.Center);
            foreach (var word in wordsWithRate)
            {
                var font = new Font(settings.FontFamilyName, GetFontSize(word.Value, settings.TextScale));
                tagCloud.Layouts.Add(new Word(word.Key, font, cloudLayouter));
            }
            return tagCloud;
        }

        private int GetFontSize(int wordRate, int scale) =>
            (int)Math.Pow(wordRate, 0.5) * scale;
    }
}
