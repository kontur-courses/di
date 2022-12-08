using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TagCloud.CloudLayouters;
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
            if (wordPreprocessor == null || cloudLayouter == null)// || settings == null)
            {
                throw new ArgumentNullException(
                    "IWordPreprocessor and TagCloudSettings are required for this method");
            }

            this.cloudLayouter = cloudLayouter;
            this.wordPreprocessor = wordPreprocessor;
        }

        public TagCloud GenerateTagCloud()
        {
            var tagCloud = new TagCloud(cloudLayouter.Center);
            PrepareWords(wordPreprocessor);
            PrepareTagCloud(tagCloud);
            return tagCloud;
        }

        private void PrepareWords(IWordPreprocessor wordPreprocessor)
        {
            var words = wordPreprocessor.GetPreprocessedWords();
            wordsWithRate = words.GroupBy(word => word).
                Select(group => new KeyValuePair<string, int>(group.Key, group.Count())).
                OrderByDescending(group => group.Value);
        }

        private void PrepareTagCloud(TagCloud tagCloud)
        {
            foreach (var word in wordsWithRate)
            {
                var font = new Font("Arial", word.Value);
                tagCloud.Rectangles.Add(new Word(word.Key, font, cloudLayouter));
            }
        }
    }
}
