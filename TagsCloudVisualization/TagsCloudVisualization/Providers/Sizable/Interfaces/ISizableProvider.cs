using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Sizable.Interfaces
{
    public interface ISizableProvider
    {
        List<SizableWord> GetSizableSource(List<KeyValuePair<string, int>> frequencySource,
            DrawerSettings settings);
    }
}