using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.Util;

namespace TagCloud.Core.Painters
{
    public interface IPainter
    {
        void PaintTags(IEnumerable<Tag> tags);
        void SetBackgroundColorFor(Graphics graphics);
    }
}