using System;
using System.Drawing;

namespace TagsCloudContainer.App.Layouter
{
    public class CloudLayouterSettings
    {
        public Point Center { get; set; } = new Point(600, 350);
        public double OffsetPoint { get; set; } = 0.01;
        public double SpiralStep { get; set; } = -0.3;
        public bool IsOffsetToCenter { get; set; } = false;
    }
}