using System.Collections.Generic;
using System.Drawing;

namespace CloudTagContainer
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}