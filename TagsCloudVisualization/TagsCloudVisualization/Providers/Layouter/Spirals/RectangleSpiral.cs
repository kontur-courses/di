using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagsCloudVisualization.Providers.Layouter.Interfaces;

namespace TagsCloudVisualization.Providers.Layouter.Spirals
{
    class RectangleSpiral:ISpiral
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
            var x = coefficient * spiralCounter;
            var y = coefficient * spiralCounter;

            var pos = new Point((int) (x + center.X), (int) (y + center.Y));
            return pos;
        }
    }
}
