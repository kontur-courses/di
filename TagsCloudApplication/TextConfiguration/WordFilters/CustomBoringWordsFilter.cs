using System.Collections.Generic;
using System.Linq;
using TextConfiguration.TextReaders;

namespace TextConfiguration.WordFilters
{
    public class CustomBoringWordsFilter : IWordFilter
    {
        private readonly List<string> excludedWords;

        public CustomBoringWordsFilter(ITextReader reader, string filename)
        {
            excludedWords = reader.ReadText(filename).Split().ToList();
        }

        public bool ShouldExclude(string word)
        {
            return excludedWords.Contains(word.ToLower());
        }
    }
}
