using System.Drawing;

namespace Visualization.Layouters
{
    public interface ILayouter
    {
        Point Center { get; set; }
        Rectangle PutNextRectangle(Size size);
    }
}