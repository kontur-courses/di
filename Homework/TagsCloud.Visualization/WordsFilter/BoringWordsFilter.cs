using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Visualization.WordsReaders;

namespace TagsCloud.Visualization.WordsFilter
{
    public class BoringWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> baseBoringWords =
            new()
            {
                "a", "and", "or", "to", "in", "into", "on", "for", "by", "during", "the", "our", "is",
                "of", "he", "she", "we", "his", "her", "that", "it", "as", "at", "but", "with", "was", "had", "has",
                "have", "which", "were", "so", "from", "been", "without", "you", "who", "me", "are", "their",
                "my", "be", "no", "not", "when", "him", "my", "said", "if", "how", "an"
            };

        private readonly HashSet<string> boringWords;

        public BoringWordsFilter() : this(null)
        {
        }

        public BoringWordsFilter(IWordsReadService wordsReadService)
        {
            if (wordsReadService == null)
                boringWords = baseBoringWords;
            else
                boringWords = wordsReadService.Read()
                    .Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
                    .ToHashSet();
        }

        public bool IsWordValid(string word) => !boringWords.Contains(word);
    }
}