using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagCloudBuildingAlgorithm
    {
        IEnumerable<Tag> GetTags(Dictionary<string, int> wordsFrequencyDict);
    }
}