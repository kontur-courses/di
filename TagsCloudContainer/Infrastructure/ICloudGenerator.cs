using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Infrastructure
{
    internal interface ICloudGenerator
    {
        public Dictionary<string, Rectangle> GenerateCloud(Dictionary<string, int> dictionary);
    }
}