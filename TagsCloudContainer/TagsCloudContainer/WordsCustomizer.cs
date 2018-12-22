using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHunspell;


namespace TagsCloudContainer
{
    internal class WordsCustomizer: IWordsCustomizer
    {
        private List<string> _wordsToIgnore;
        private Func<string, bool> _shouldIgnoreWord;
        public static readonly List<string> Prepositions = Properties.Resources.listOfPrepositions
            .Split('\n', '\r').Where(w => !string.IsNullOrEmpty(w)).ToList();

        public WordsCustomizer(List<string> wordsToIgnore = null, Func<string, bool> shouldIgnoreWord = null)
        {
            _wordsToIgnore = wordsToIgnore ?? StandardWordsToIgnorePack();
            _shouldIgnoreWord = shouldIgnoreWord ?? (x => false);
        }

        public string CustomizeWord(string word)
        {
            if (word == null)
                throw new NullReferenceException();

            return IgnoreWord(word) ? null : ToBaseForm(word.ToLower());
        }

        private bool IgnoreWord(string word)
        {
            return _wordsToIgnore.Contains(word.ToLower()) || _shouldIgnoreWord(word);
        }

        private string ToBaseForm(string word)
        {
            string result;
            using (var hunspell = new Hunspell(Properties.Resources.en_us_aff,
                Encoding.ASCII.GetBytes(Properties.Resources.en_us_dic)))
            {
                result = hunspell.Stem(word).FirstOrDefault();
            }

            return result ?? word;
        }

        private List<string> StandardWordsToIgnorePack()
        {
            return new List<string>(Prepositions);
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