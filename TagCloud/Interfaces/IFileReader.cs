using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<string> Read(string path);
    }
}