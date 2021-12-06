using System.Collections.Generic;

namespace TagCloud.configurations
{
    public interface IWordFilterConfiguration
    {
        IEnumerable<string> Filter(IEnumerable<string> source);
    }
}