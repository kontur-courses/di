using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagCloud.CloudLayouters;
using TagCloud.Tags;

namespace TagCloud.TagCloudCreators
{
    public class LayoutTagCloudCreator : ITagCloudCreator
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IEnumerable<Size> sizes;
        public LayoutTagCloudCreator(ICloudLayouter cloudLayouter, IEnumerable<Size> sizes)
        {
            this.cloudLayouter = cloudLayouter;
            this.sizes = sizes;
        }

        public TagCloud GenerateTagCloud()
        {
            var tagCloud = new TagCloud(cloudLayouter.Center);
            foreach (var layoutSize in sizes)
                tagCloud.Rectangles.Add(new Layout(
                    cloudLayouter.PutNextRectangle(layoutSize)));

            return tagCloud;
        }
    }
}
