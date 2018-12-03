using System.Collections.Generic;
using TagCloud.Data;

namespace TagCloud.Parser
{
    public interface IWordsParser
    {
        IEnumerable<WordInfo> Parse(IEnumerable<string> words, HashSet<string> boringWords);
    }
}