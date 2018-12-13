using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.CloudStructure;

namespace TagsCloud.TagsCloudVisualization
{
    public class TagCloudLayouter: ITagCloudLayouter
    {
        private readonly ICloudLayouter cloud;
        private readonly ISizeDefiner sizeDefiner;

        public TagCloudLayouter(ICloudLayouter cloud, ISizeDefiner sizeDefiner)
        {
            this.cloud = cloud;
            this.sizeDefiner = sizeDefiner;
        }

        public List<Tag> GetTags(Dictionary<string, int> wordFrequency)
        {
            var tags = new List<Tag>();
            var maxFrequency = wordFrequency.Values.Max();
            var minFrequency = wordFrequency.Values.Min();
            foreach (var item in wordFrequency)
            {
                var sizeAndFont = sizeDefiner.GetSizeAndFont(item.Key, item.Value, minFrequency, maxFrequency);
                var posRectangle = cloud.PutNextRectangle(sizeAndFont.Item1);
                tags.Add(new Tag(posRectangle, item.Key, sizeAndFont.Item2, item.Value));
            }
            return tags;
        }
    }
}
