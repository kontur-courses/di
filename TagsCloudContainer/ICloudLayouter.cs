using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudLayouter
    {
        public Point Center { get; set; }
        public List<Rectangle> Rectangles { get; }
        public Rectangle PutNextRectangle(Size rectangleSize);
        public void ChangeCenter(Point newCenter);
    }
}