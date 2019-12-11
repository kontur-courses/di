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

        public IEnumerable<string> HandleWords(IEnumerable<string> words) => words.Select(HandleWord);

        public string HandleWord(string word) =>
            wordHandlers.Aggregate(word, (current, wordHandler) => wordHandler.Handle(current));

    }
}
