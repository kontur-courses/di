using System.Collections.Generic;
using TagsCloudVisualization.Results;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Layouter.Interfaces
{
    internal interface IDrawableProvider
    {
        Result<List<DrawableWord>> GetDrawableSource(List<SizableWord> sizableSource,
            LayouterSettings layouterSettings);
    }
}