using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    internal interface IPointsProvider
    {
        public IEnumerable<Point> Points();
        public void Reset();
    }
}
