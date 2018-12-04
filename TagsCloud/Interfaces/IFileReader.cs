using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<string> ReadLines(string path);
    }
}