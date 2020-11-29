using System.Collections.Generic;

namespace TagCloud
{
    internal interface ITextProcessor
    {
        public IEnumerable<string> Process(string text);
    }
}
