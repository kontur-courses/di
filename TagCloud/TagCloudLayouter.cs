using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class TagCloudLayouter : ITagCloudLayouter
    {
        private readonly ICloudLayouter layouter;

        public TagCloudLayouter(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public IReadOnlyCollection<Tag> GetLayout(ICollection<KeyValuePair<string, double>> words)
        {
            var result = new List<Tag>();
            foreach (var keyValuePair in words)
            {
                var width = (int) Math.Round(keyValuePair.Key.Length * keyValuePair.Value);
                var height = (int) Math.Round(keyValuePair.Value);
                var size = new Size(width, height);
                var tag = new Tag(keyValuePair.Key, layouter.PutNextRectangle(size));
                result.Add(tag);
            }

            return result;
        }
    }
}