using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IRectangleComposer
    {
        public ISpiral Spiral { get; set; }
        public List<Rectangle> Rectangles { get; set; }
        public Rectangle GetNextRectangleInCloud(Size rectangleSize);
    }
}
