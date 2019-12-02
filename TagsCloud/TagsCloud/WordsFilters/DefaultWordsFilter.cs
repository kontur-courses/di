using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsFilters
{
    public class DefaultWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> wordsToExclude;

        public DefaultWordsFilter() =>
            wordsToExclude = new HashSet<string>
            {
                "and", "but", "if", "when", "or",
                "i", "he", "she", "it", "they"
            };

        public int Priority => 10;
        public string[] Execute(string[] input)
        {
            if (input == null)
                throw new ArgumentNullException();
            return input.Where(s => !wordsToExclude.Contains(s)).ToArray();
        }
    }
}