using System.Drawing;

namespace CloudLayouter.Interfaces
{
    public interface ISpiral
    {
        Point Center { get; }
        Point GetNewSpiralPoint();
    }
}