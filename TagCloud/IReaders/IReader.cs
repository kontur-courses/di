using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.IReaders
{
    public interface IReader
    {
        public IEnumerable<string> ReadWords();
    }
}
