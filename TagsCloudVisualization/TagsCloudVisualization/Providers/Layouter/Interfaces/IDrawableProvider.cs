using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Layouter.Interfaces
{
    internal interface IDrawableProvider
    {
        IEnumerable<DrawableWord> GetDrawableSource(IEnumerable<SizableWord> sizableSource,
            LayouterSettings layouterSettings);
    }
}