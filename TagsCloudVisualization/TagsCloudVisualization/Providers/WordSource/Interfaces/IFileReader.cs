using System.Collections.Generic;
using TagsCloudVisualization.Results;

namespace TagsCloudVisualization.Providers.WordSource.Interfaces
{
    internal interface IFileReader
    {
        Result<List<string>> ReadLines(string path);
    }
}