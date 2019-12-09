using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsFilters
{
    public class BoringWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> wordsToExclude;

        public BoringWordsFilter() =>
            wordsToExclude = new HashSet<string>
            {
                "и", "но", "если", "когда", "или",
                "я", "он", "она", "оно", "они"
            };

        public int Priority => 10;

        public string FactorialId => "BoringWordsFilter";

        public string[] Execute(string[] input)
        {
            if (input == null)
                throw new ArgumentNullException();
            return input.Where(s => !wordsToExclude.Contains(s)).ToArray();
        }
    }
}