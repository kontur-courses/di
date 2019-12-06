using System.Collections.Generic;
using System.Linq;
using TagsCloud.CloudConstruction;
using TagsCloud.Visualization.SizeDefiner;
using TagsCloud.Visualization.Tag;

namespace TagsCloud.Visualization
{
    public class Layouter : ILayouter
    {
        private readonly ICloudLayouter _cloud;
        private readonly ISizeDefiner _sizeDefiner;

        public Layouter(ICloudLayouter cloud, ISizeDefiner sizeDefiner)
        {
            this._cloud = cloud;
            this._sizeDefiner = sizeDefiner;
        }

        public IEnumerable<Tag.Tag> GetTags(Dictionary<string, int> wordFrequency)
        {
            var maxFrequency = wordFrequency.Values.Max();
            var minFrequency = wordFrequency.Values.Min();
            return (from item in wordFrequency
                let tagSize = _sizeDefiner.GetTagSize(item.Key, item.Value, minFrequency, maxFrequency)
                let locationRectangle = _cloud.PutNextRectangle(tagSize.RectangleSize)
                select new Tag.Tag(locationRectangle, item.Key, tagSize.FontSize, item.Value)).ToList();
        }
    }
}