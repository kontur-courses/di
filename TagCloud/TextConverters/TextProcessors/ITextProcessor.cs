using System.Collections.Generic;

namespace TagCloud.TextConverters.TextProcessors
{
    internal interface ITextProcessor
    {
        public IEnumerable<string> Process(string text);
    }
}
