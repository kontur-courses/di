using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        HashSet<Rectangle> Centering();
    }
}