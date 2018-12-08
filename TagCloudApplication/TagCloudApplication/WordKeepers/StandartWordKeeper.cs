using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloudApplication.WordKeepers
{
    public class StandartWordKeeper : IWordKeeper
    {
        private readonly string[] delimiters;
        private Func<(string Word, int Frequency), bool> removingUnnecessaryWordsRule = p => false;
        private Func<string, string> gramFormRule = p => p;
        private WordClass currentWordClass = WordClass.All;

        public StandartWordKeeper(string[] delimiters)
        {
            this.delimiters = delimiters;
        }

        public StandartWordKeeper RemoveUnnecessaryWordsBy(Func<(string Word, int Frequency), bool> removingRule)
        {
            removingUnnecessaryWordsRule = removingRule;
            return this;
        }

        public StandartWordKeeper ChangeWordGrammaticalFormBy(Func<string, string> gramFormRule)
        {
            this.gramFormRule = gramFormRule;
            return this;
        }

        public StandartWordKeeper TakeOnly(WordClass wordClass)
        {
            currentWordClass = wordClass;
            return this;
        }

        public List<(string Word, int Freq)> GetWordIncidence(string fileName, IReader reader)
        {
            var text = reader.GetText(fileName);
            return GetWordIncidence(text);
        }

        public List<(string Word, int Freq)> GetWordIncidence(string text)
        {
            var resDick = new Dictionary<string, int>();
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var prepWords = words.Select(w => gramFormRule(w.ToLower())).Where(w => IsCurrentWordClass(w)).ToList();
            foreach (var word in prepWords)
            {
                if (resDick.ContainsKey(word))
                    resDick[word]++;
                else
                    resDick.Add(word, 1);
            }
            return resDick.Select(p => (Word: p.Key, Freq: (int)Math.Round((double)(p.Value * 100 / prepWords.Count))))
                .Where(p => !removingUnnecessaryWordsRule(p))
                .ToList();
        }

        private static bool IsCurrentWordClass(string word)
        {
            //доделать
            return true;
        }
    }

    public enum WordClass
    {
        Noun,
        Verb,
        Adjective, 
        All
    }
}
