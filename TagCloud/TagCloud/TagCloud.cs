using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public abstract class TagCloud : ITagCloud
    {
        public TagCloud()
        {
            Rectangles = new List<Rectangle>();
        }

        public abstract void GenerateTagCloud(string[] words);

        public abstract Rectangle PutNextRectangle(Size rectangleSize);

        public List<Rectangle> Rectangles { get; }
    }
}