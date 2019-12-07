using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.ShapeGenerator
{
    public interface IShapeGenerator
    {
        Point Center { get; }

        Point GetNextSpiralPoint();

        List<Point> GetNextSpiralPoints(int count);

        void ResetSpiral();
    }
}