using System.Collections.Generic;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    public interface ISelectorFactory<T>
    {
        IEnumerable<ISelector<T>> GetSelectors(ReaderSettings readerSettings);
    }
}