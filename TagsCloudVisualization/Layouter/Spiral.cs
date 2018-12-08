using System.Drawing;

namespace TagsCloudVisualization.Layouter
{
    public class Spiral : Polar
    {
        private readonly double step;
        public Point Center { get; }

        public Spiral(Point center, double step, double angle)
        {
            this.step = step;
            Center = center;
            Angle = angle;
        }

        public Point GetNextPoint()
        {
            Radius = step * Angle;
            var resultPoint = Center.Add(ToCartesian());
            Angle++;
            return resultPoint;
        }
    }
}
