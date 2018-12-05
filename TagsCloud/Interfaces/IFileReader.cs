using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFileReader
    {
        string Path { get; set; }

        IEnumerable<string> ReadLines();
    }
}