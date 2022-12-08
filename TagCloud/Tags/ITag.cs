﻿using System.Drawing;

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
