using System;
using TagsCloudVisualization;

namespace TagCloud
{
    internal abstract class TagCloudOptions
    {
        protected TagCloudOptions(ISpiralGenerator spiral, Point center)
        {
            this.Spiral = spiral;
            this.Center = center;
        }

        public delegate TagCloudOptions Factory(ISpiralGenerator spiral, Point center);


        public ISpiralGenerator Spiral { get; private set; }
        public Point Center { get; private set; }
        public double SizeCoefficient => 3;
    }
}