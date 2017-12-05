using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface ITagCloudDrawer
    {
        Image DrawTagCloud(IEnumerable<TextRectangle> rectangles, DrawingSettings settings);
    }
}