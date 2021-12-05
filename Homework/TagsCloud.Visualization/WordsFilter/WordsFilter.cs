using System.Collections.Generic;

namespace TagsCloud.Visualization.WordsFilter
{
    public class WordsFilter : IWordsFilter
    {
        // TODO Move to config file and read on init
        private readonly HashSet<string> prepositions =
            new()
            {
                "a", "and", "or", "to", "in", "into", "on", "for", "by", "during", "the", "our", "is",
                "of", "he", "she", "we", "his", "her", "that", "it", "as", "at", "but", "with", "was", "had", "has",
                "have", "which", "were", "so", "from", "been", "without", "you", "who", "me", "are", "their",
                "my", "be", "no", "not", "when", "him", "my", "said", "if", "how", "an"
            };

        public bool IsWordValid(string word) => !prepositions.Contains(word);
    }
}