using System.Collections.Generic;

namespace TagCloud.TextHandlers.Parser
{
    public interface ITextParser
    {
        IEnumerable<string> GetWords(string path);
    }
}