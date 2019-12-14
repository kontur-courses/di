using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Infrastructure.Common
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        HashSet<Rectangle> Centering();
    }
}