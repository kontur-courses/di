using System.Collections.Generic;

namespace TagCloud.WordProcessing
{
    public interface IWordProcessor
    {
        IEnumerable<string> PrepareWords(IEnumerable<string> rawWords);
    }
}
