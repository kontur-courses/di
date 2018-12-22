using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Generation;

namespace TagsCloudContainer.Visualization
{
    static class TagsExtension
    {
        internal static Bitmap Visualize(this IEnumerable<string> tags,
            TagsCloudSettings settings, IVisualizer visualizer) =>
                visualizer.Visualize(tags, settings);
    }
}