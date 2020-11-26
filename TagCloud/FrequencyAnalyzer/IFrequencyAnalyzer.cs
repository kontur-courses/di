using System.Collections.Generic;

namespace TagCloud
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, double> GetFrequencyDictionary();
    }
}