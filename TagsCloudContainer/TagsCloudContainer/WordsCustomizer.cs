using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;


namespace TagsCloudContainer
{
    internal class WordsCustomizer
    {
        private List<string> _wordsToIgnore;
        private Func<string, bool> _shouldIgnoreWord;
        private readonly string _enUsAffPath;
        private readonly string _enUsDicPath;
 
        public WordsCustomizer(List<string> wordsToIgnore = null, Func<string, bool> shouldIgnoreWord = null)
        {
            _wordsToIgnore = wordsToIgnore ?? StandardWordsToIgnorePack();
            _shouldIgnoreWord = shouldIgnoreWord ?? (x => false);

            var dirPath = AppDomain.CurrentDomain.BaseDirectory;
            _enUsAffPath = Path.Combine(dirPath, "..", "..", "EnglishDictionaries", "en_us.aff");
            _enUsDicPath = Path.Combine(dirPath, "..", "..", "EnglishDictionaries", "en_us.dic");
        }

        public string CustomizeWord(string word)
        {
            if (word == null)
                throw new NullReferenceException();

            return IgnoreWord(word) ? null : ToBaseForm(word.ToLower());
        }

        public bool IgnoreWord(string word)
        {
            return _wordsToIgnore.Contains(word.ToLower()) || _shouldIgnoreWord(word);
        }

        private string ToBaseForm(string word)
        {
            string result;
            using (var hunspell = new Hunspell(_enUsAffPath, _enUsDicPath))
            {
                result = hunspell.Stem(word).FirstOrDefault();
            }

            return result ?? word;
        }

        private static List<string> StandardWordsToIgnorePack()
        {
            return Prepositions();
        }

        public static List<string> Prepositions()
        {
            return new List<string>
            {
                "to", "into", "up", "down", "through", "out", "across", "along", "at", "by", "on", "in", "above",
                "under", "among", "between", "behind", "in front of", "next to",
                "about", "after", "at", "during", "for", "in", "on", "till", "within"
            };
        }

        public void SetIgnoreFunc(Func<string, bool> ignoreFunc)
        {
            _shouldIgnoreWord = ignoreFunc;
        }

        public void SetWordsToIgnore(IEnumerable<string> wordsToIgnore)
        {
            _wordsToIgnore = wordsToIgnore.ToList();
        }

        public void AddWordsToIgnore(IEnumerable<string> newWordsToIgnore)
        {
            _wordsToIgnore.AddRange(newWordsToIgnore);
        }

        public List<string> GetWordsToIgnore()
        {
            return _wordsToIgnore;
        }
    }
}
