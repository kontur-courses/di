using System;
using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public class TextParser : ITextParser
    {
        private readonly ISourceReader sourceReader;
        private readonly Dictionary<string, int> wordsCounts;
        private readonly List<Func<string, string>> handlers;
        private readonly Action<string, Dictionary<string, int>> grouper;

        public TextParser(ISourceReader sourceReader, 
            List<Func<string, string>> handlers,
            Action<string, Dictionary<string, int>> grouper)
        {
            this.sourceReader = sourceReader;
            this.handlers = handlers;
            this.grouper = grouper;
            wordsCounts = HandleWords();
        }

        public IReadOnlyDictionary<string, int> GetWordsCounts()
            => wordsCounts;

        private Dictionary<string, int> HandleWords()
        {
            var groupedWords = new Dictionary<string, int>();
            foreach (var word in sourceReader.GetNextWord())
            {
                var handledWord = HandleWord(word);
                grouper(handledWord, groupedWords);
            }

            return groupedWords;
        }

        private string HandleWord(string word)
        {
            foreach (var handler in handlers)
                word = handler(word);
            return word;
        }
    }
}
