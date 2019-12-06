using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Core.TextHandler.WordHandlers
{
    class Handler
    {
        private readonly IWordHandler[] wordHandlers;
        public Handler(IWordHandler[] wordHandlers)
        {
            this.wordHandlers = wordHandlers;
        }

        public IEnumerable<string> HandleWords(IEnumerable<string> words) => words.Select(HandleWord);

        public string HandleWord(string word) =>
            wordHandlers.Aggregate(word, (current, wordHandler) => wordHandler.Handle(current));

    }
}
