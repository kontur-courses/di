using System.Collections.Generic;

namespace TagCloud.Reader
{
    public interface IWordsFileReader
    {
        IEnumerable<string> Read(string fileName);
    }
}