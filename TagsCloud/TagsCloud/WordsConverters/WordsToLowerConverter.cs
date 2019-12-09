using System;
using System.Linq;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsConverters
{
    [Priority(10)]
    [Factorial("WordsToLowerConverter")]
    public class WordsToLowerConverter : IWordsConverter
    {
        public string[] Execute(string[] input)
        {
            if (input == null)
                throw new ArgumentNullException();
            return input.Select(w => w.ToLower()).ToArray();
        }
    }
}