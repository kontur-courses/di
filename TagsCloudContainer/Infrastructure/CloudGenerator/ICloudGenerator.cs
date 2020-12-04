using System.Collections.Generic;
using TagsCloudContainer.App.CloudGenerator;

namespace TagsCloudContainer.Infrastructure.CloudGenerator
{
    internal interface ICloudGenerator
    {
        public IEnumerable<Tag> GenerateCloud(ICloudLayouter layouter, Dictionary<string, double> frequencyDictionary);
    }
}