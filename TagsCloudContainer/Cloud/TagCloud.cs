using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Words;
using TagsCloudVisualization;

namespace TagsCloudContainer.Cloud
{
    public class TagCloud
    {
        public WordTag[] Tags { get; }
        private const int letterWidthInPixels = 20;
        public TagCloud(ICloudLayouter cloudLayouter, WordAnalizer wordAnalizer)
        {
            var tags = new List<WordTag>();
            foreach (var pack in wordAnalizer.WordPacks)
            {
                var word = pack.Key;
                var size = new Size(word.Length * letterWidthInPixels * pack.Count, letterWidthInPixels * 2 * pack.Count);
                var rect = cloudLayouter.PutNextRectangle(size);
                tags.Add(new WordTag(rect, word));
            }

            Tags = tags.ToArray();
        }
    }
}
