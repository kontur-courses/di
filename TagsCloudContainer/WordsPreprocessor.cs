using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordsPreprocessor : IWordsPreprocessor
    {
        private readonly string[] words;

        public WordsPreprocessor(ISource source)
        {
            words = source.Parse();
        }

        public Dictionary<string, int> Prepare()
        {
            return words.Select(word => word.ToLower())
                .Distinct()
                .RemoveBoring()
                .ToDictionary(w => w, w => words
                    .Count(s => string.Equals(s, w, StringComparison.OrdinalIgnoreCase)
                    )
                );
        }
    }
}
