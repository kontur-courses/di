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
        private readonly ICloudLayouter cloudLayouter;
        private readonly IWordPreprocessor wordPreprocessor;
        private IOrderedEnumerable<KeyValuePair<string, int>> wordsWithRate;
        
        public WordTagCloudCreator(ICloudLayouter cloudLayouter, IWordPreprocessor wordPreprocessor)
        {
            if (wordPreprocessor == null || cloudLayouter == null)
            {
                throw new ArgumentNullException(
                    "ICloudLayouter and IWordPreprocessor are required for this method");
            }

            this.cloudLayouter = cloudLayouter;
            this.wordPreprocessor = wordPreprocessor;
            PrepareWords();
        }

        public TagCloud GenerateTagCloud(ITagCloudVisualizationSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(
                    "ITagCloudVisualizationSettings is required for this method");

            var tagCloud = new TagCloud(cloudLayouter.Center);
            PrepareTagCloud(tagCloud, settings);
            return tagCloud;
        }

        private void PrepareWords()
        {
            var words = wordPreprocessor.GetPreprocessedWords();
            wordsWithRate = words.GroupBy(word => word).
                Select(group => new KeyValuePair<string, int>(group.Key, group.Count())).
                OrderByDescending(group => group.Value);
        }

        private void PrepareTagCloud(TagCloud tagCloud, ITagCloudVisualizationSettings settings)
        {
            foreach (var word in wordsWithRate)
            {
                var font = new Font(settings.FontFamilyName, word.Value * 20);
                tagCloud.Layouts.Add(new Word(word.Key, font, cloudLayouter));
            }
        }
    }
}
