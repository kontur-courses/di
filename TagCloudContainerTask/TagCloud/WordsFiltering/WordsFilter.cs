using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordsFiltering
{
    public class WordsFilter
    {
        public List<string> FilterWords(List<string> words)
        {
            return words.Where(word => word.Length > 3).ToList();
        }
    }
}