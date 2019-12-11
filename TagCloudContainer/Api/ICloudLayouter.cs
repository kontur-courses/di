using System.Collections.Generic;
using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface ICloudLayouter 
    {
        Rectangle PutNextRectangle(Size rectangleSize, List<Rectangle> containter);
    }
}