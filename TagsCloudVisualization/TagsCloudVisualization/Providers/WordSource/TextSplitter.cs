using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Providers.WordSource.Interfaces;

namespace TagsCloudVisualization.Providers.WordSource
{
    internal class TextSplitter : IWordReader
    {
        public IEnumerable<string> SplitByPunctuation(IEnumerable<string> lineSource)
        {
            return lineSource.SelectMany(line =>
            {
                var punctuation = line.Where(char.IsPunctuation).Distinct().ToArray();
                return line.Split().Select(x => x.Trim(punctuation));
            });
        }
    }
}