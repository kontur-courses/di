using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class RectangleGenerator
    {
        private readonly Size minRectangle;
        private readonly Size maxRectangle;

        private readonly Random random;

        public RectangleGenerator(Size minRectangle, Size maxRectangle)
        {
            this.minRectangle = minRectangle;
            this.maxRectangle = maxRectangle;
            random = new Random();
        }

        public Size GetRandomRectangle()
        {
            var width = random.Next(minRectangle.Width, maxRectangle.Width);
            var height = random.Next(minRectangle.Height, maxRectangle.Height);

            return new Size(width, height);
        }
    }
}