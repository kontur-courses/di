using System.Drawing;
using TagsCloudVisualization.Providers.Layouter.Interfaces;

namespace TagsCloudVisualization.Providers.Layouter.Spirals
{
    internal class RectangleSpiral : ISpiral
    {
        private readonly Point center;
        private readonly float coefficient;
        private int spiralCounter;

        public RectangleSpiral(Point center, float coefficient = 1)
        {
            this.coefficient = coefficient;
            this.center = center;
            spiralCounter = 0;
        }

        public Point GetSpiralNext()
        {
            spiralCounter++;
            return GetSpiralCurrent();
        }

        public Point GetSpiralCurrent()
        {
            var coordinate = coefficient * spiralCounter;
            var pos = new Point((int) (coordinate + center.X), (int) (coordinate + center.Y));
            return pos;
        }
    }
}