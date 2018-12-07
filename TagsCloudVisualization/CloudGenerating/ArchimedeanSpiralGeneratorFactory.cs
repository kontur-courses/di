using System.Drawing;

namespace TagsCloudVisualization.CloudGenerating
{
    public class ArchimedeanSpiralGeneratorFactory : ISpiralGeneratorFactory
    {
        private readonly PointF center;
        private readonly float step;
        private readonly float angleDeltaInRadians;

        public ArchimedeanSpiralGeneratorFactory(PointF center, float step, float angleDeltaInRadians)
        {
            this.center = center;
            this.step = step;
            this.angleDeltaInRadians = angleDeltaInRadians;
        }

        public ISpiralGenerator Create()
        {
            return new ArchimedeanSpiralGenerator(center, step, angleDeltaInRadians);
        }
    }
}
