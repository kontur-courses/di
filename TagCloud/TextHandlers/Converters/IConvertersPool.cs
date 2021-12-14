using System.Collections.Generic;

namespace TagCloud.TextHandlers.Converters
{
    public interface IConvertersPool
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
    }
}