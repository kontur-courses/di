using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ITagCloud
    {
        List<Rectangle> Rectangles { get; }
        void GenerateTagCloud(string[] words);
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}