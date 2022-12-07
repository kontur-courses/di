using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.Readers
{
    public interface IReader
    {
        public IEnumerable<string> ReadWords();
    }
}
