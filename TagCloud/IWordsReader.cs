using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsReader
    {
        List<string> Get();
    }
}