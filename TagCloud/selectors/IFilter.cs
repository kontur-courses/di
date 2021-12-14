using System.Collections.Generic;

namespace TagCloud.selectors
{
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> source);
    }
}