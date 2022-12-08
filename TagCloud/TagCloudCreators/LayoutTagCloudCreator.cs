using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.CloudLayouters;
using TagCloud.TagCloudVisualizations;
using TagCloud.Tags;

namespace TagCloud.TagCloudCreators
{
    public class LayoutTagCloudCreator : ITagCloudCreator
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IEnumerable<Size> sizes;
        public LayoutTagCloudCreator(ICloudLayouter cloudLayouter, IEnumerable<Size> sizes)
        {
            if (sizes == null || cloudLayouter == null)
            {
                throw new ArgumentNullException(
                    "ICloudLayouter and IEnumerable<Size> are required for this method");
            }
            this.cloudLayouter = cloudLayouter;
            this.sizes = sizes;
        }

        public TagCloud GenerateTagCloud(ITagCloudVisualizationSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(
                    "ITagCloudVisualizationSettings is required for this method");

            var tagCloud = new TagCloud(cloudLayouter.Center);
            foreach (var layoutSize in sizes)
                tagCloud.Layouts.Add(new Layout(
                    cloudLayouter.PutNextRectangle(layoutSize)));

            return tagCloud;
        }
    }
}
