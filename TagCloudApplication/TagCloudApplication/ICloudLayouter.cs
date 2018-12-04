using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        Size GetTagCloudSize();
    }
}