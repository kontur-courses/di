using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public class FilterApplyer : IFilterApplyer
    {
        private readonly Dictionary<FilterType, IWordsFilter> wordsFilterResolver;

        public FilterApplyer(IWordsFilter[] wordsFilters)
        {
            wordsFilterResolver = wordsFilters.ToDictionary(x => x.FilterType);
        }

        public IWordsFilter Get(FilterType type)
        {
            if (wordsFilterResolver.ContainsKey(type))
                return wordsFilterResolver[type];
            throw new ArgumentException($"Фильтр '{type.ToString()}' не существует");
        }

        public ICollection<string> Apply(ICollection<WordInfo> words, FilterType[] filters)
        {
            return filters
                .Select(Get)
                .Aggregate(words, (current, implementation) => implementation.Filter(current))
                .Select(wordInfo => wordInfo.Lemma)
                .ToArray();
        }
    }
}