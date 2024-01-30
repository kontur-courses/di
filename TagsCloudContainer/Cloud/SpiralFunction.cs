using System.Drawing;

namespace TagsCloudContainer.Cloud
{
    public class SpiralFunction
    {
        private double angle;
        private readonly Point _pastPoint;
        private readonly double _step;

        public SpiralFunction(Point start, double step)
        {
            _pastPoint = start;
            _step = step;
        }

        public Point GetNextPoint()
        {
            var newX = (int)(_pastPoint.X + _step * angle * Math.Cos(angle));
            var newY = (int)(_pastPoint.Y + _step * angle * Math.Sin(angle));
            angle += Math.PI / 50;

            return new Point(newX, newY);
        }
    }
}