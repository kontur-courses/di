using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public interface ICloudLayouter
    {
        public IReadOnlyCollection<Rectangle> Rectangles { get; }
        public Rectangle PutNextRectangle(Size size);
    }
}