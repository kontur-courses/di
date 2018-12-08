using System.Collections.Generic;

namespace TagCloudApplication
{
    public interface IWordKeeper
    {
        List<(string Word, int Freq)> GetWordIncidence(string text);
    }
}