using System.Drawing;
using TagCloud.Interfaces;
using TagsCloudVisualization;

namespace TagCloud
{
    public class SpiralPointsFactory : IPointsFactory
    {
        public IPoints Get(Point cloudCenter) =>
            new SpiralPoints(cloudCenter);
    }
}