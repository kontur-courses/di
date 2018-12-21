using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;

namespace TagCloud
{
    public class SimpleWordsCounter: IWordsCounter
    {
        private readonly Dictionary<string, int> countedWords = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> CountedWords => countedWords;

        public Result<None> UpdateWith(string text) => 
            Result.OfAction(()=>Split(text).Foreach(IncOrAddToCounter));

        private string[] Split(string text)=>
            text.ToLower()
                .Split(" \t\n\r.,?!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        private void IncOrAddToCounter(string word) =>
            Result.Of(() => countedWords.TryGetValue(word, out var count) ? count : 0)
                .Then(i => countedWords[word] = i + 1);
    }
}