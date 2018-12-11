using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Formatters
{
    public class ToLowerCaseFormatter : IWordsFormatter
    {
        public List<string> Format(List<string> words)
        {
            return words.Select(x => x.ToLower()).ToList();
        }
    }
}