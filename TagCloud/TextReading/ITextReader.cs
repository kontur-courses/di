using System.Collections.Generic;

namespace TagCloud.TextReading
{
    public interface ITextReader
    {
        IEnumerable<string> ReadWords();
    }
}
