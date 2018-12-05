using System.Collections.Generic;
using System.Drawing;
using TagCloud.Util;

namespace TagCloud.Painters
{
    public interface IPainter
    {
        Color BackgroundColor { get; }
        void PaintTags(List<Tag> tags);
        void SetBackgroundColorFor(Graphics graphics);
    }
}