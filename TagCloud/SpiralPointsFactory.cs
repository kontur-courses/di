using System.Drawing;
using TagsCloudVisualization;

namespace TagCloud
{
    public class SpiralPointsFactory : IPointsFactory
    {
        public IPoints Get(Point cloudCenter)
        {
            return new SpiralPoints(cloudCenter);
        }
    }
}