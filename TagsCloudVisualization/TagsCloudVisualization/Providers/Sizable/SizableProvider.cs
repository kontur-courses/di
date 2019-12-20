using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Sizable
{
    internal class SizableProvider : ISizableProvider
    {
        private readonly SizeSelectorFactory sizableSelector;

        public SizableProvider(SizeSelectorFactory sizableSelector)
        {
            this.sizableSelector = sizableSelector;
        }

        public List<SizableWord> GetSizableSource(List<KeyValuePair<string, int>> wordFrequency,
            DrawerSettings settings)
        {
            return wordFrequency.Select(kv =>
                    new SizableWord(kv.Key,
                        sizableSelector.GeSelector(settings.SizeSelector).GetSize(kv.Key, kv.Value, settings)))
                .Where(sizable => sizable.DrawSize.Width > 0 && sizable.DrawSize.Height > 0).ToList();
        }
    }
}