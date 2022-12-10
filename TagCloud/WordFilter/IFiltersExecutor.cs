using System.Collections.Generic;

namespace TagCloud.WordFilter
{
    public interface IFiltersExecutor
    {
        IReadOnlyList<string> Filter(IEnumerable<string> words);
    }
}
