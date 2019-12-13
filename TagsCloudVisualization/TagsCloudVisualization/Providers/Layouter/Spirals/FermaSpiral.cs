using System;
using System.Drawing;
using TagsCloudVisualization.Providers.Layouter.Interfaces;

namespace TagsCloudVisualization.Providers.Layouter.Spirals
{
    public class FermaSpiral : ISpiral
    {
        private readonly Point center;
        private readonly float coefficient;
        private int spiralCounter;

        public FermaSpiral(Point center, float coefficient = 1)
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
            var x = coefficient * MathF.Pow(spiralCounter, 1f / 2) * MathF.Cos(spiralCounter);
            var y = coefficient * MathF.Pow(spiralCounter, 1f / 2) * MathF.Sin(spiralCounter);
            var pos = new Point((int) (x + center.X), (int) (y + center.Y));
            return pos;
        }
    }
}