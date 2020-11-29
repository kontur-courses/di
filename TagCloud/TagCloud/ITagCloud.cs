using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ITagCloud
    {
        List<Rectangle> Rectangles { get; }
        void GenerateTagCloud();
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}