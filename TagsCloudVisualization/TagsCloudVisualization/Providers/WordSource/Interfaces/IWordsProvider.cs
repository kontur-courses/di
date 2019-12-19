using System.Collections.Generic;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    internal interface IWordsProvider
    {
        IEnumerable<string> GetObjectSource(ReaderSettings settings);
    }
}