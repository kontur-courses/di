using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordsPreprocessor : IWordsPreprocessor
    {
        private readonly string[] words;
        private readonly string[] excludedWords;

        public WordsPreprocessor(ISource source, TextFileReader excludedWordsSource)
        {
            words = source.GetWords();
            excludedWords = excludedWordsSource.GetWords();
        }

        public Dictionary<string, int> GetWordsFrequency()
        {
            return words.Select(word => word.ToLower())
                .Distinct()
                .Except(excludedWords, StringComparer.OrdinalIgnoreCase)
                .RemoveBoring()
                .ToDictionary(w => w, w => words
                    .Count(s => string.Equals(s, w, StringComparison.OrdinalIgnoreCase)
                    )
                );
        }
    }
}
