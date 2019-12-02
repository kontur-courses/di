using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.Visualization
{
    public interface IRectangleCloudDrawer
    {
        void DrawCloud(IEnumerable<TagInfo> tags, string filename);
        void DrawRectangles(IEnumerable<Rectangle> rectangles, string filename);
    }
}