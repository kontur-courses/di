using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudVisualization;

namespace TagCloudCreation
{
    public class TagCloudStatsGenerator : ITagCloudStatsGenerator
    {
        public List<WordInfo> GenerateStats(IEnumerable<string> words)
        {
            return words.GroupBy(w => w, StringComparer.InvariantCulture)
                        .Select(g => new WordInfo(g.Key, g.Count()))
                        .ToList();
        }

        //TODO: add font size relation
        public Size GetSizeOfWord(WordInfo wordInfo) =>
            new Size(wordInfo.Word.Length * 10*wordInfo.Count, 20* wordInfo.Count);
    }
}
