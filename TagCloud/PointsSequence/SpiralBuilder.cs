using System.Drawing;

namespace TagCloud.PointsSequence
{
    public class SpiralBuilder
    {
        private readonly Spiral spiral;

        public SpiralBuilder()
        {
            spiral = new Spiral();
        }

        public SpiralBuilder WithStepLength(double stepLength)
        {
            spiral.SetStepLength(stepLength);
            return this;
        }

        public SpiralBuilder WithCenterIn(Point center)
        {
            spiral.SetCenter(center);
            return this;
        }

        public Spiral Build()
        {
            return spiral;
        }
    }
}