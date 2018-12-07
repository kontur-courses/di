using System.Collections.Generic;

namespace TagCloud.Processor
{
    public interface IWordsProcessor
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}