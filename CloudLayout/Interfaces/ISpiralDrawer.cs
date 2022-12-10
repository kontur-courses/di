using System.Drawing;

namespace CloudLayout.Interfaces
{
    public interface ISpiralDrawer
    {
        List<PointF> GetSpiralPoints(Point center);
    }
}