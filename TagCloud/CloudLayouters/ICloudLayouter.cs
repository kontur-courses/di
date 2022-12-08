using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.CloudLayouters
{
    public interface ICloudLayouter
    {
        Point Center { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
