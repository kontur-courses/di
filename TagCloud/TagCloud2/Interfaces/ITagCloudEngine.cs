using System.Collections.Generic;
using System.Drawing;

namespace TagCloud2.Interfaces
{
    public interface ITagCloudEngine
    {
        public Rectangle PutNextRectangle(Size sizeOfRectangle);
        public List<Rectangle> Rectangles { get; }
    }
}