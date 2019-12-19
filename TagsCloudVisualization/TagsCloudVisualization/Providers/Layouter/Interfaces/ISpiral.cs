using System.Drawing;

namespace TagsCloudVisualization.Providers.Layouter.Interfaces
{
    public interface ISpiral
    {
        Point GetSpiralNext();

        Point GetSpiralCurrent();
    }
}