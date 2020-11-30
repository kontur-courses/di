using System.Collections.Generic;

namespace TagCloud
{
    public interface IFileReader
    {
        List<string> Get();
    }
}