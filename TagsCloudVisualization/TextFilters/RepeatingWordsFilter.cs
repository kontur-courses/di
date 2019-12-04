using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.texts
{
    public class RepeatingWordsFilter : ITextFilter
    {
        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Distinct();
        }
    }
}