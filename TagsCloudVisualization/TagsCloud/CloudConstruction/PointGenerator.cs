using System;
using System.Drawing;
using TagsCloudVisualization.TagsCloud.CircularCloud;

namespace TagsCloudVisualization.TagsCloud.CloudConstruction
{
    public class PointGenerator
    {
        public const double StepAngle = Math.PI / 36;
        public const double ParameterArchimedesSpiral = 10 / (2 * Math.PI);
        public double Angle { get; set; }
        public CircularCloudLayouter Cloud { get; set; }

        public PointGenerator(CircularCloudLayouter cloud)
        {
            Cloud = cloud;
        }
        public Point GetNextPointArchimedesSpiral(Size size)
        {
            var distance = ParameterArchimedesSpiral * Angle;
            var location = new Point((int)(Cloud.Center.X + distance * Math.Cos(Angle)) - size.Width / 2,
                (int)(Cloud.Center.Y - distance * Math.Sin(Angle)) - size.Height / 2);
            Angle += StepAngle;
            return location;
        }
    }
}