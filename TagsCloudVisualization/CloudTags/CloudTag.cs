﻿using System.Drawing;

namespace TagsCloudVisualization
{
    public class CloudTag : ICloudTag
    {
        public CloudTag(Rectangle rectangle, string text)
        {
            Rectangle = rectangle;
            Text = text;
        }

        public Rectangle Rectangle { get; }
        public string Text { get; }
    }
}