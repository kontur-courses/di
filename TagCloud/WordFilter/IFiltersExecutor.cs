using System.Collections.Generic;

namespace TagCloud
{
    public interface IFiltersExecutor
    {
        IReadOnlyList<string> Filter(IEnumerable<string> words);
    }
}
