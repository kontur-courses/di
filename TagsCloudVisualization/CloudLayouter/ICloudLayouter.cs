using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Spirals;

namespace TagsCloudVisualization.CloudLayouter
{
    public  interface ICloudLayouter
    {
        public List<Rectangle> Rectangles { get; }
        public ISpiral Spiral { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}