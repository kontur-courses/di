using System.Collections.Generic;
using System.Drawing;
using TagsCloudGenerator.Infrastructure;

namespace TagsCloudGenerator.Core.Drawers
{
    public interface IRectangleCloudDrawer
    {
        Bitmap DrawCloud(IEnumerable<TagInfo> tags, bool withRectangles);
        Bitmap DrawRectangles(IEnumerable<Rectangle> rectangles);
    }
}