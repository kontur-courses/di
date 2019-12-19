using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Sizable.Interfaces
{
    public interface ISizableProvider
    {
        IEnumerable<SizableWord> GetSizableSource(IEnumerable<KeyValuePair<string, int>> frequencySource,
            DrawerSettings settings);
    }
}