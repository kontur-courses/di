using System.Collections.Generic;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    public interface ISelectorFactory
    {
        IEnumerable<IWordSelector> GetSelectors(ReaderSettings readerSettings);
    }
}