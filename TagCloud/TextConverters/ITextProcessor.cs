using System.Collections.Generic;

namespace TagCloud.TextConverters
{
    internal interface ITextProcessor
    {
        public IEnumerable<string> Process(string text);
    }
}
