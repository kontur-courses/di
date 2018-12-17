using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    internal class WordsCustomizer
    {
        private readonly List<string> _wordsToIgnore;
        private readonly Func<string, bool> _shouldIgnoreWord;
 
        public WordsCustomizer(List<string> wordsToIgnore = null, Func<string, bool> shouldIgnoreWord = null)
        {
            _wordsToIgnore = wordsToIgnore ?? StandardWordsToIgnorePack();
            _shouldIgnoreWord = shouldIgnoreWord ?? (x => false);
        }

        public string CustomizeWord(string word)
        {
            return IgnoreWord(word) ? null : ToBaseForm(word.ToLower());
        }

        public bool IgnoreWord(string word)
        {
            return _wordsToIgnore.Contains(word.ToLower()) || _shouldIgnoreWord(word);
        }

        private string ToBaseForm(string word)
        {
            // Место для потенциального приведения слов к начальной форме
            return word;
        }

        private static List<string> StandardWordsToIgnorePack()
        {
            return Prepositions();
        }

        private static List<string> Prepositions()
        {
            return new List<string>
            {
                "to", "into", "up", "down", "through", "out", "across", "along", "at", "by", "on", "in", "above",
                "under", "among", "between", "behind", "in front of", "next to",
                "about", "after", "at", "during", "for", "in", "on", "till", "within"
            };
        }
    }
}
