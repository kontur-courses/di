using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Canvases;

namespace TagsCloudVisualization.PointsGenerators
{
    public class ArchimedesSpiral : IPointGenerator
    {
        public Point Center { get; private set; }

        private readonly SpiralParams spiralParams;
        private IEnumerator<Point> spiralPoints;
        private readonly ICanvas canvas;

        public ArchimedesSpiral(SpiralParams spiralParams, ICanvas canvas)
        {
            if (Math.Abs(spiralParams.AngleStep) < 1e-3)
                throw new ArgumentException("Angle step must be not equal zero");
            
            if (spiralParams.SpiralParameter == 0)
                throw new ArgumentException("Spiral parameter must be not equal zero");
            
            this.spiralParams = spiralParams;
            this.canvas = canvas;
            spiralPoints = GetSpiralPoints().GetEnumerator();
        }

        private IEnumerable<Point> GetSpiralPoints()
        {
            var imageSize = canvas.GetImageSize();
            Center = new Point(imageSize.Width / 2, imageSize.Height / 2);
            yield return Center;

            var angle = 0.0f;
            var currentPoint = Center;

            while (true)
            {
                int x;
                int y;
                checked
                {
                    try
                    {
                        x = spiralParams.SpiralParameter * (int)Math.Round(angle * Math.Cos(angle)) + Center.X;
                        y = spiralParams.SpiralParameter * (int)Math.Round(angle * Math.Sin(angle)) + Center.Y;
                    }
                    catch (OverflowException)
                    {
                        throw new OverflowException(
                            "Int32 overflow occurred when generating a new point to put the rectangle");
                    }
                }

                var nextPoint = new Point(x, y);
                if (!nextPoint.Equals(currentPoint))
                    yield return nextPoint;

                currentPoint = nextPoint;
                angle += spiralParams.AngleStep;
            }
        }

        public Point GetNextPoint()
        {
            spiralPoints.MoveNext();
            return spiralPoints.Current;
        }

        public void StartOver()
        {
            spiralPoints = GetSpiralPoints().GetEnumerator();
        }
    }
}