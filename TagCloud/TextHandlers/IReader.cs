using System.Collections.Generic;

namespace TagCloud.TextHandlers
{
    public interface IReader
    {
        public IEnumerable<string> Read(string filename);
    }
}