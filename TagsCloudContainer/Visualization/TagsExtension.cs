using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    static class TagsExtension
    {
        internal static Bitmap Visualize(this IEnumerable<string> tags, TagsCloudSettings settings) =>
            new TagsCloudVisualizer().Visualize(tags, settings);
    }
}