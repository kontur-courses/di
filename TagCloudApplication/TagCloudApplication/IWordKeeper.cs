using System.Collections.Generic;

namespace TagCloudApplication
{
    public interface IWordKeeper
    {
        Dictionary<string, int> GetWordFrequency(string text);
    }
}