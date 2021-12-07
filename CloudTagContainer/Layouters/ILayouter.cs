using System.Collections.Generic;
using System.Drawing;

namespace CloudTagContainer
{
    public interface ILayouter
    {
        Point Center { get; set; }
        Rectangle PutNextRectangle(Size size);
    }
}