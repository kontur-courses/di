﻿using System.Drawing;

namespace TagsCloudVisualization.TagCloud
{
    public interface ICloudLayouter
    {
        public Point Center { get; }
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}