using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class WordsFilter : IWordsFilter
    {
        private readonly List<Func<string, bool>> filters = new List<Func<string, bool>>();

        private readonly HashSet<string> prepositions = new HashSet<string>
            {"in", "of", "but", "at", "until", "to", "for", "on", "by"};

        private readonly List<Func<string, string>> transformations = new List<Func<string, string>>();

        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (!filters.All(filter => filter(word))) continue;
                var res = word;
                foreach (var transformation in transformations) res = transformation(res);

                yield return res;
            }
        }

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
    }
}