using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ISpiral
    {
        Point GetSpiralNext();

        Point GetSpiralCurrent();
    }
}