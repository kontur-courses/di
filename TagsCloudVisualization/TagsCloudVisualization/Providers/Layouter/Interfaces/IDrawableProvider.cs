using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Layouter.Interfaces
{
    internal interface IDrawableProvider<T>
    {
                                               IEnumerable<Drawable<T>> GetDrawableObjects(IEnumerable<Sizable<T>> sizableSource,
            LayouterSettings settings);
    }
}