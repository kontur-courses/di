using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.RectanglesTransformer
{
    public interface IRectanglesTransformer
    {
        IList<Rectangle> TransformRectangles(IEnumerable<Rectangle> rectangles, Size imageSize);
    }
}
