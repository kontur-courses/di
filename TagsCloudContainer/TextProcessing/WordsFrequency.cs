using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordsFrequency : IWordsFrequency
    {
        private readonly IEnumerable<IWordsFilter> _wordsFilter;

        public WordsFrequency(IEnumerable<IWordsFilter> wordsFilter)
        {
            _wordsFilter = wordsFilter;
        }

        public Dictionary<string, int> GetWordsFrequency(string text)
        {
            if (text == null)
                throw new ArgumentException("String must be not null");

            return GetInterestingWordsIntersection(_wordsFilter.Select(filter => filter.GetInterestingWords(text))
                    .ToList())
                .GroupBy(word => word)
                .ToDictionary(wordsGroup => wordsGroup.Key,
                    wordsGroup => wordsGroup.Count());
        }

        private static IEnumerable<string> GetInterestingWordsIntersection(List<string[]> sequencesWords)
        {
            if (sequencesWords.Count == 0)
                return new List<string>();
            var intersection = sequencesWords[0].ToList();
            for (var i = 1; i < sequencesWords.Count; i++)
                intersection = intersection.Intersect(sequencesWords[i]).ToList();

            return intersection;
        }
    }
}