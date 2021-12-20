using System.Drawing;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualizationTest.Builders
{
    public class CircularCloudLayouterBuilder
    {
        private readonly PointSpiralBuilder pointSpiralBuilder;

        private CircularCloudLayouterBuilder()
        {
            pointSpiralBuilder = PointSpiralBuilder.APointSpiral();
        }
        
        public static CircularCloudLayouterBuilder ACircularCloudLayouter()
        {
            return new CircularCloudLayouterBuilder();
        }

        public CircularCloudLayouterBuilder WithCenterAt(Point position)
        {
            pointSpiralBuilder.WithCenter(position);
            return this;
        }
        
        public CircularCloudLayouterBuilder WithDensityParameter(uint density)
        {
            pointSpiralBuilder.WithDensityParameter(density);
            return this;
        }
        
        public CircularCloudLayouterBuilder WithDegreesDelta(uint degreesDeltaParameter)
        {
            pointSpiralBuilder.WithDegreesDelta(degreesDeltaParameter);
            return this;
        }

        public CircularCloudLayouter Build()
        {
            return new CircularCloudLayouter(pointSpiralBuilder.Build());
        }
    }
}