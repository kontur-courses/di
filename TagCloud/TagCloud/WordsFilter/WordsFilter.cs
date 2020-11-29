using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class WordsFilter : IWordsFilter
    {
        private HashSet<string> prepositions = new HashSet<string>
            {"in", "of", "but", "at", "until", "to", "for", "on", "by"};

        private List<Func<string, bool>> filters = new List<Func<string, bool>>();
        private List<Func<string, string>> transformations = new List<Func<string, string>>();

        public WordsFilter RemovePrepositions()
        {
            filters.Add(word => !prepositions.Contains(word.ToLower()));
            return this;
        }

        public WordsFilter Normalize()
        {
            transformations.Add(word => word.ToLower());
            return this;
        }

        public WordsFilter And(Func<string, bool> filter)
        {
            filters.Add(filter);
            return this;
        }

        public WordsFilter And(Func<string, string> transfromation)
        {
            transformations.Add(transfromation);
            return this;
        }

        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            return filters
                .Aggregate(words, (current, filter) => current.Where(filter))
                .Select(word =>
                    transformations
                        .Aggregate(word, (current, transformation) => transformation(current)));
        }
    }
}