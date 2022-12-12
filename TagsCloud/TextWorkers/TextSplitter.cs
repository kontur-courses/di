using System;
using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class TextSplitter : ITextSplitter
    {
        private readonly char[] delimeterChars = { ' ', ',', '.', ':', '\t', '\r', '\n' };

        public IEnumerable<string> SplitTextOnWords(string text)
        {
            var words = text.Split(delimeterChars, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }
    }
}
