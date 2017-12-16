using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordProcessor
    {
        Result<Dictionary<string, int>> GetFrequencyDictionary(string filePath);
    }
}