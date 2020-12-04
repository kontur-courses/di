using System;
using System.Collections.Generic;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordsFrequency : IWordsFrequency
    {
        private readonly IWordsFilter _wordsFilter;

        public WordsFrequency(IWordsFilter wordsFilter)
        {
            _wordsFilter = wordsFilter;
        }

        public Dictionary<string, int> GetWordsFrequency(string text)
        {
            if (text == null)
                throw new ArgumentException("String must be not null");
            var words = _wordsFilter.GetInterestingWords(text);
            var frequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequency.ContainsKey(word))
                    frequency[word]++;
                else
                    frequency[word] = 1;
            }

            return frequency;
        }
    }
}