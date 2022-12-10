using System.Collections.Generic;

namespace TagCloud.WordConverter
{
    public interface IConvertersExecutor
    {
        IReadOnlyList<string> Convert(IEnumerable<string> words);
    }
}
