﻿using System.Drawing;

namespace TagCloud.CloudLayouters
{
    public interface ICloudLayouter
    {
        public Point Center { get; }
        public Rectangle PutNextRectangle(Size rectangleSize);
        public void Reset();
    }
}
