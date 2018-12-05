using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}