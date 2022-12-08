using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.Tags
{
    public interface ITag
    {
        public Rectangle Frame { get; }
        public Size Size { get; }
        public void DrawIn(Graphics graphics, Brush byBrush);
        public void ShiftTo(Size shift);
    }
}
