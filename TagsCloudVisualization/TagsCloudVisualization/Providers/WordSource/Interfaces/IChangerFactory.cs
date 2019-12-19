using System.Collections.Generic;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    public interface IChangerFactory
    {
        IEnumerable<IWordChanger> GetChangers(ReaderSettings settings);
    }
}