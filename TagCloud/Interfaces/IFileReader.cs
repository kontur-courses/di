using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IFileReader
    {
        string Path { get; set; }

        IEnumerable<string> Read();
    }
}