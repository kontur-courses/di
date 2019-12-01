using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudContainer
{
    public interface ICircularCloudLayouter
    {
        List<Rectangle> Layout { get; }

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}