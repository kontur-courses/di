using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Sizable.Interfaces
{
    public interface ISizableProvider<T>
    {
        IEnumerable<Sizable<T>> GetSizableObjects(IEnumerable<KeyValuePair<T, int>> objects,
            DrawerSettings settings);
    }
}