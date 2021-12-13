using System.Collections.Generic;
using System.Drawing;

namespace Visualization
{
    public interface ILayouter
    {
        Point Center { get; set; }
        Rectangle PutNextRectangle(Size size);
    }
}