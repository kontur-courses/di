using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Sizable
{
    internal class SizableProvider : ISizableProvider<string>
    {
        private readonly ISizableSelector<string, int> selector;

        public SizableProvider(ISizableSelector<string, int> selector)
        {
            this.selector = selector;
        }

        public IEnumerable<Sizable<string>> GetSizableObjects(IEnumerable<KeyValuePair<string, int>> wordFrequency,
             DrawerSettings settings)
        {
            return wordFrequency.Select(kv =>
                    new Sizable<string>(kv.Key, selector.GetSize(kv.Key, kv.Value, settings)))
                .Where(sizable => sizable.DrawSize.Width > 0 && sizable.DrawSize.Height > 0);
        }
    }
}