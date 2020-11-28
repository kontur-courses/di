using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Infrastructure
{
    internal interface ISizesGenerator
    {
        public Dictionary<string, Size> GenerateSizes(Dictionary<string, int> dictionary);
    }
}