using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public interface ILayouter
    {
        IList<Rectangle> GetRectangles(IEnumerable<Size> sizes);
    }
}
