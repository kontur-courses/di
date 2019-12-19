using System.Collections.Generic;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    internal interface IFileReader
    {
        IEnumerable<string> ReadLines(string path);
    }
}