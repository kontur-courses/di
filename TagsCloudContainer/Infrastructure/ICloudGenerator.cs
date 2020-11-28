using System.Collections.Generic;
using TagsCloudContainer.App.CloudGenerator;

namespace TagsCloudContainer.Infrastructure
{
    internal interface ICloudGenerator
    {
        public IEnumerable<Tag> GenerateCloud(Dictionary<string, double> frequencyDictionary, string fontName);
    }
}