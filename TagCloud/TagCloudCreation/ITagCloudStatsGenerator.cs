using System.Collections.Generic;
using System.Drawing;
using TagCloudVisualization;

namespace TagCloudCreation
{
    public interface ITagCloudStatsGenerator
    {
        List<WordInfo> GenerateStats(IEnumerable<string> words);
        Size GetSizeOfWord(WordInfo wordInfo);
    }
}
