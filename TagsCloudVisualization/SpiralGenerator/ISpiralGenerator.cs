using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ISpiralGenerator
    {
        Point Center { get; }

        Point GetNextSpiralPoint();

        List<Point> GetNextSpiralPoints(int count);

        void ResetSpiral();
    }
}