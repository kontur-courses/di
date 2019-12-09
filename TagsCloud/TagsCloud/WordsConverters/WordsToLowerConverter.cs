using System;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsConverters
{
    public class WordsToLowerConverter : IWordsConverter
    {
        public int Priority => 10;

        public string FactorialId => "WordsToLowerConverter";

        public string[] Execute(string[] input)
        {
            if (input == null)
                throw new ArgumentNullException();
            return input.Select(w => w.ToLower()).ToArray();
        }
    }
}