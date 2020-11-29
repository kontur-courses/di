using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    internal class TextProcessor
    {
        private readonly IWordExcluder excluder = new WordsExcluder();
        internal IEnumerable<string> Process(string text) =>
            text
            .Split(Environment.NewLine)
            .Where(s => s != string.Empty)
            .Select(s => s.ToLower())
            .Where(s => !excluder.MustBeExclude(s));
    }
}
