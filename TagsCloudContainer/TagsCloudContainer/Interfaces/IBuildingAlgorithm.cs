using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IBuildingAlgorithm
    {
        IEnumerable<Tag> GetRectangleSizes(Dictionary<string, int> wordsFrequencyDict);
    }
}