using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.BoringWordsRepositories;
using TagCloud.CloudLayouters;
using TagCloud.Readers;
using TagCloud.TagCloudVisualizations;
using TagCloud.Tags;
using TagCloud.WordPreprocessors;

namespace TagCloud.TagCloudCreators
{
    public class WordTagCloudCreator : ITagCloudCreator
    {
        private readonly IReader wordReader;
        private readonly IBoringWordsStorage boringWordsStorage;
        private readonly ICloudLayouter.Factory cloudLayouterFactory;
        private ICloudLayouter cloudLayouter;
        private readonly IWordPreprocessor wordPreprocessor;
        private IOrderedEnumerable<KeyValuePair<string, int>> wordsWithRate;
        
        public WordTagCloudCreator(IReader wordReader, IBoringWordsStorage boringWordsStorage, ICloudLayouter.Factory cloudLayouterFactory, IWordPreprocessor wordPreprocessor)
        {
            if (wordPreprocessor == null || cloudLayouterFactory == null)
            {
                throw new ArgumentNullException(
                    $"{nameof(IReader)}, {nameof(ICloudLayouter.Factory)} and {nameof(IWordPreprocessor)} are required for this method");
            }

            this.wordReader = wordReader;
            this.boringWordsStorage = boringWordsStorage;
            this.cloudLayouterFactory = cloudLayouterFactory;
            this.wordPreprocessor = wordPreprocessor;
        }

        public TagCloud GenerateTagCloud(ITagCloudVisualizationSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(
                    "ITagCloudVisualizationSettings is required for this method");

            cloudLayouter = cloudLayouterFactory.Invoke();
            PrepareWords(wordReader);
            var tagCloud = new TagCloud(cloudLayouter.Center);
            PrepareTagCloud(tagCloud, settings);
            return tagCloud;
        }

        private void PrepareWords(IReader wordReader)
        {
            var words = wordPreprocessor.GetPreprocessedWords(wordReader, boringWordsStorage);
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
