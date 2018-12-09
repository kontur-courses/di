using System;
using System.Collections.Generic;
using System.Linq;
using TagCloudApplication.Readers;

namespace TagCloudApplication.WordKeepers
{
    public class StandartWordKeeper : SimpleWordKeeper
    {
        private Func<string, bool> removingUnnecessaryWordsRule = p => false;
        private Func<string, string> gramFormRule = p => p;
        private Func<string, bool> wordClassRule = w => true;

        public StandartWordKeeper(string[] delimiters, IReader reader) : base(delimiters, reader)
        {
        }

        public StandartWordKeeper RemoveUnnecessaryWordsBy(Func<string, bool> removingRule)
        {
            removingUnnecessaryWordsRule = removingRule;
            return this;
        }

        public StandartWordKeeper ChangeWordGrammaticalFormBy(Func<string, string> gramFormRule)
        {
            this.gramFormRule = gramFormRule;
            return this;
        }

        public StandartWordKeeper TakeOnlyClassFormBy(Func<string, bool> wordClassRule)
        {
            this.wordClassRule = wordClassRule;
            return this;
        }

        public new List<(string Word, int Freq)> GetWordIncidenceInPercent(string fileName, int minPossibleWordFrequency = 5)
        {
            var words = ComplicatedPreprocessWords(reader.GetText(fileName));
            return GetWordsFrequencyInPercent(words, minPossibleWordFrequency);
        }

        private string[] ComplicatedPreprocessWords(string text)
        {
            return base.PreprocessWords(text)
                .Select(w => w.ToCharArray().Where(char.IsLetter).ToString())
                .Select(w => gramFormRule(w))
                .Where(w => !removingUnnecessaryWordsRule(w))
                .Where(w => wordClassRule(w))
                .ToArray();
        }
    }
}
