using System.Collections.Generic;

namespace TagsCloudVisualization.Common.FileReaders
{
    public interface IFileReader
    {
        IEnumerable<string> ReadLines(string path);
    }
}