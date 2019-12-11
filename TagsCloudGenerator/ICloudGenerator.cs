using System.Collections.Generic;
using System.Drawing;
using TagsCloudGenerator.CloudLayouter;

namespace TagsCloudGenerator
{
    public interface ICloudGenerator
    {
        Cloud Generate(Dictionary<string, int> wordsToCount, Font font);
    }
}