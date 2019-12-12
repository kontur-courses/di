using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Core.TextHandler.WordConverters
{
    class WordConverter
    {
        private readonly IWordConverter[] wordHandlers;

        public WordConverter(IWordConverter[] wordHandlers)
        {
            this.wordHandlers = wordHandlers;
        }

        public IEnumerable<string> ConvertWords(IEnumerable<string> words) => words.Select(ConvertWord);

        public string ConvertWord(string word) =>
            wordHandlers.Aggregate(word, (current, wordHandler) => wordHandler.Convert(current));
    }
}