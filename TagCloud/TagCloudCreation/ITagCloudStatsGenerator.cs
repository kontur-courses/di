using System.Collections.Generic;
using System.Drawing;

namespace TagCloudCreation
{
    public interface ITagCloudStatsGenerator
    {
        List<WordInfo> GenerateStats(IEnumerable<string> words);
        Size GetSizeOfWord(WordInfo wordInfo);
    }
}
