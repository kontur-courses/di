using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    internal class TextProcessor : ITextProcessor
    {
        private readonly IWordExcluder excluder = new WordsExcluder();
        public IEnumerable<string> Process(string text) =>
            text
            .Split(Environment.NewLine)
            .Where(s => s != string.Empty)
            .Select(s => s.ToLower())
            .Where(s => !excluder.MustBeExclude(s));
    }
}
