using System.Collections.Generic;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.WordSource.Interfaces
{
    public interface ISelectorFactory
    {
        IEnumerable<IWordSelector> GetSelectors(ReaderSettings readerSettings);
    }
}