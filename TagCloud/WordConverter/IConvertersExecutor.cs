using System.Collections.Generic;

namespace TagCloud
{
    public interface IConvertersExecutor
    {
        IReadOnlyList<string> Convert(IEnumerable<string> words);
    }
}
