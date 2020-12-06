using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudLayouter
    {
        Point Center { get; set; }
        List<Rectangle> Rectangles { get; set; }
        Rectangle PutNextRectangle(Size rectangleSize);
        void ChangeCenter(Point newCenter);
        void Reset();
    }
}