using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class CloudGenerator : ICloudGenerator
    {
        private readonly ICloudLayouter layouter;
        private readonly ISizesGenerator sizesGenerator;

        public CloudGenerator(ISizesGenerator sizesGenerator, ICloudLayouter layouter)
        {
            this.sizesGenerator = sizesGenerator;
            this.layouter = layouter;
        }

        public Dictionary<string, Rectangle> GenerateCloud(Dictionary<string, int> dictionary)
        {
            var cloud = new Dictionary<string, Rectangle>();
            foreach (var pair in sizesGenerator.GenerateSizes(dictionary))
            {
                var word = pair.Key;
                var size = pair.Value;
                cloud[word] = layouter.PutNextRectangle(size);
            }

            return cloud;
        }
    }
}