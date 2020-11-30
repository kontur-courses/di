using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.TextConverters.WordExcluders;

namespace TagCloud.TextConverters.TextProcessors
{
    internal class TextProcessor : ITextProcessor
    {
        private readonly IWordExcluder excluder = new WordsExcluder();
        public IEnumerable<string> Process(string text) =>
            text
            .Split('\n')
            .Where(s => s != string.Empty)
            .Select(s => s.ToLower())
            .Where(s => !excluder.MustBeExclude(s));
    }
}
