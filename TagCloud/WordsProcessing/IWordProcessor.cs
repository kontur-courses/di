using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public interface IWordProcessor
    {
        IEnumerable<Word> PrepareWords(IEnumerable<string> rawWords);
    }
}
