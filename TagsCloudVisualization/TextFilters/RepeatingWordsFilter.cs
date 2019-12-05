using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.TextFilters
{
    public class RepeatingWordsFilter : ITextFilter
    {
        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Distinct();
        }
    }
}