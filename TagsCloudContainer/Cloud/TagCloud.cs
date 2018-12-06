using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Words;
using TagsCloudVisualization;

namespace TagsCloudContainer.Cloud
{
    public class TagCloud
    {
        public WordTag[] Tags;
        public int Cof = 20;
        public TagCloud(ICloudLayouter cloudLayouter, WordAnalizer wordAnalizer)
        {
            var packs = wordAnalizer.WordPacks.ToList();
            //sortPacks.Sort((p1, p2) => p2.Count - p1.Count); // todo impliments
            var tags = new List<WordTag>();
            foreach (var pack in packs)
            {
                var word = pack.Key;
                var size = new Size(word.Length * Cof * pack.Count, Cof * 2 * pack.Count);
                var rect = cloudLayouter.PutNextRectangle(size);
                tags.Add(new WordTag(rect, word));
            }

            Tags = tags.ToArray();
        }
    }
}
