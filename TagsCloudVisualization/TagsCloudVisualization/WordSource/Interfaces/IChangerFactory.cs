using System.Collections.Generic;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    public interface IChangerFactory<T>
    {
        IEnumerable<IChanger<T>> GetChangers(ReaderSettings settings);
    }
}