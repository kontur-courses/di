using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Interfaces;
using TagsCloud.WordProcessing;
using System;

namespace TagsCloud.FinalProcessing
{
    public class TagCloudGenerator : ITagCloudGenerator
    {
        private readonly ITagCloudLayouter tagCloud;

        public TagCloudGenerator(ITagCloudLayouter tagCloud)
        {
            this.tagCloud = tagCloud;
        }

        public IEnumerable<(Tag tag, Rectangle position)> GenerateTagCloud(IEnumerable<Tag> allTags)
        {
            var image = new Bitmap(1,1);
            var graph = Graphics.FromImage(image);
            var result = new List<(Tag tag, Rectangle position)>();
            foreach (var tag in allTags)
            {
                var size = graph.MeasureString(tag.word, tag.font).ToSize();
                result.Add((tag, tagCloud.PutNextRectangle(size)));
            }
            return result;
        }
    }
}
