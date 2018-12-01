using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface IDrawer
    {
        void GenerateImage(IEnumerable<Rectangle> rectangles, string imageName);
    }
}