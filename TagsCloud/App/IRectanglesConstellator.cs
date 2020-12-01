using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.App
{
    public interface IRectanglesConstellator
    {
        string Name { get; }
        int MaxX { get; }
        int MinX { get; }
        int MaxY { get; }
        int MinY { get; }
        int Width { get; }
        int Height { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
        void Clear();
    }
}
