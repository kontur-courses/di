using System.Collections.Generic;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    internal interface IObjectProvider<T>
    {
        IEnumerable<T> GetObjectSource(ReaderSettings settings);
    }
}