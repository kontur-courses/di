using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Configurations;
using TagsCloudVisualization.Helpers;

namespace TagsCloudVisualization
{
    public class TagCloudCreator : ICloudCreator
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly CloudConfiguration cloudConfiguration;

        public TagCloudCreator(ICloudLayouter cloudLayouter, CloudConfiguration cloudConfiguration)
        {
            this.cloudLayouter = cloudLayouter;
            this.cloudConfiguration = cloudConfiguration;
        }

        public IEnumerable<Bitmap> Create(IEnumerable<string> words, int amountWords = -1, int amountClouds = 1)
        {
            var tags = TagCloudHelper.CreateTagsFromWords(words, amount: amountWords);
            var sizes = TagCloudHelper.GenerateRectangleSizes(tags);
            var bitmaps = new List<Bitmap>();
            
            for (var i = 0; i < amountClouds; i++)
            {
                TagCloudHelper.ShuffleTags(tags, sizes);
                
                var rectangles = cloudLayouter.GenerateCloud(cloudConfiguration.Center, sizes);

                var tagsByRectangles = tags.Zip(rectangles, (k, v) => new { k, v })
                    .ToDictionary(x => x.k, x => x.v);

                bitmaps.Add(DrawingHelper.DrawTagCloud(tagsByRectangles, cloudConfiguration));
            }

            return bitmaps;
        }
    }
}