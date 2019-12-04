using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize, Point center);
        void Clear();
    }
}
