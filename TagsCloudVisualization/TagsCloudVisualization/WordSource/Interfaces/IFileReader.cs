using System.Collections.Generic;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    internal interface IFileReader<T>
    {
        IEnumerable<T> ReadLines(string path);
    }
}