using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTest.Builders
{
    internal class PointSpiralBuilder
    {
        private Point center;
        private uint densityParameter = 1;
        private uint degreesDeltaParameter = 1;

        private PointSpiralBuilder()
        {
        }

        public static PointSpiralBuilder APointSpiral()
        {
            return new PointSpiralBuilder();
        }

        public PointSpiralBuilder WithCenter(Point centerPoint)
        {
            center = centerPoint;
            return this;
        }
        
        public PointSpiralBuilder WithDensityParameter(uint density)
        {
            densityParameter = density;
            return this;
        }
        
        public PointSpiralBuilder WithDegreesDelta(uint degreesDelta)
        {
            degreesDeltaParameter = degreesDelta;
            return this;
        }
        
        public PointSpiral Build()
        {
            return new PointSpiral(center, densityParameter, degreesDeltaParameter);
        }
    }
}