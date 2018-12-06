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
                .ToDictionary(w => w, w => words.Count(s => s == w));
        }

        private static string[] RemoveBoring()
        {
            return new string[0];
        }
    }
}
