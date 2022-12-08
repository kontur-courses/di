using System.Collections.Generic;

namespace TagCloud.Readers
{
    public interface IReader
    {
        public IEnumerable<string> ReadWords();
    }
}
