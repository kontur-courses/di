using System.Collections.Generic;

namespace TagCloud.Sources
{
    public interface ISource
    {
        IEnumerable<string> Words();
    }
}
