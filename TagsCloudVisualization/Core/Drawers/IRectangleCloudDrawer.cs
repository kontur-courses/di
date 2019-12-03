using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.Core.Drawers
{
    public interface IRectangleCloudDrawer
    {
        void DrawCloud(IEnumerable<TagInfo> tags, string filename, bool withRectangles);
        void DrawRectangles(IEnumerable<Rectangle> rectangles, string filename);
    }
}