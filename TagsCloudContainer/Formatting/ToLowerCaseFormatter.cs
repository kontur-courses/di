using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Formatting
{
    public class ToLowerCaseFormatter : IWordsFormatter
    {
        public List<string> Format(IEnumerable<string> words)
        {
            return words.Select(x => x.ToLower()).ToList();
        }
    }
}