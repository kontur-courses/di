using System.Collections.Generic;
using TagsCloudVisualization.Results;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.WordSource.Interfaces
{
    internal interface IWordsProvider
    {
        Result<List<string>> GetObjectSource(ReaderSettings settings);
    }
}