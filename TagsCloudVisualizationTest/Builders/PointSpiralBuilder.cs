using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTest.Builders
{
    public class PointSpiralBuilder
    {
        private Point center;
        private double densityParameter = 1;
        private int degreesDeltaParameter = 1;

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
        
        public PointSpiralBuilder WithDensityParameter(double density)
        {
            densityParameter = density;
            return this;
        }
        
        public PointSpiralBuilder WithDegreesDelta(int degreesDelta)
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