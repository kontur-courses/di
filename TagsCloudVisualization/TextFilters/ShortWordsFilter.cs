using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.TextFilters
{
    public class ShortWordsFilter : ITextFilter
    {
        private readonly int minLength;
        
        public ShortWordsFilter(int minLength = 3)
        {
            this.minLength = minLength;
        }
        
        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => word.Length > minLength);
        }
    }
}