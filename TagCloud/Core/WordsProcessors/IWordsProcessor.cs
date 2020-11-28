using System.Collections.Generic;

namespace TagCloud.Core.WordsProcessors
{
    public interface IWordsProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words);
    }
}