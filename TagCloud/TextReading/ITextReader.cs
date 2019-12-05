using System.Collections.Generic;

namespace TagCloud
{
    public interface ITextReader
    {
        IEnumerable<string> ReadWords();
    }
}
