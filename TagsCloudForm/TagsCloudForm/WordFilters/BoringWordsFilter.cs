using System.Collections.Generic;
using System.Linq;

namespace TagsCloudForm.WordFilters
{
    public class BoringWordsFilter
    {
        public IEnumerable<string>  Filter(HashSet<string> boringWords, IEnumerable<string> words)
        {
            return words.Where(x=>!boringWords.Contains(x));
        }
    }
}
