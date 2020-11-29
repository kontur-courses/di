using System.Collections.Generic;

namespace TagCloud.FrequencyAnalyzer
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, double> GetFrequencyDictionary(string fileName);
    }
}