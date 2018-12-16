using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCloud.TextAnalyze.Words
{
    public static class WordExtensions
    {
        public static IOrderedEnumerable<T> SortByEntries<T>(this IEnumerable<T> words) where T : IWord
        {
            return words.OrderByDescending(word => word.Entries);
        }

        public static IEnumerable<IWord> CountEntries(this IEnumerable<string> terms)
        {
            return
                terms.GroupBy(
                    term => term,
                    (term, equivalentTerms) => new Word(term, equivalentTerms.Count()),
                    StringComparer.InvariantCultureIgnoreCase);
        }

        public static IEnumerable<string> Filter(this IEnumerable<string> terms, IBlackList blacklist)
        {
            return terms.Where(term => !blacklist.Countains(term));
        }
    }
}