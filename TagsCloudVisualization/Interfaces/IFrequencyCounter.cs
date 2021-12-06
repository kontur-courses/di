using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFrequencyCounter
    {
        Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words);
    }
}