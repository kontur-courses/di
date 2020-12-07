using System.Collections.Generic;

namespace TagCloud.Infrastructure.Text
{
    public interface IReader
    {
        public IEnumerable<string> ReadTokens();
    }
}