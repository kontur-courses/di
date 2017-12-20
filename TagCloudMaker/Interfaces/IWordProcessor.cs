using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordProcessor
    {
        IDictionary<string, int> GetFrequencyDictionary(string filePath);
    }
}