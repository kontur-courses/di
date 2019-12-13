using System.Drawing;
using TagsCloudVisualization.Providers.Layouter.Interfaces;

namespace TagsCloudVisualization.Providers.Layouter.Spirals
{
    internal class InLineSpiral : ISpiral
    {
        private readonly Point center;
        private readonly float coefficient;
        private int spiralCounter;

        public InLineSpiral(Point center, float coefficient = 1)
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
            var x = coefficient * spiralCounter;

            var pos = new Point((int) (x + center.X), center.Y);
            return pos;
        }
    }
}