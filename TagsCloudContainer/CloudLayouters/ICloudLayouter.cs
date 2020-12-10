using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudLayouter
    {
        List<Rectangle> Rectangles { get; set; }
        Rectangle PutNextRectangle(Size rectangleSize);
        void ChangeCenter(Point newCenter);
    }
}