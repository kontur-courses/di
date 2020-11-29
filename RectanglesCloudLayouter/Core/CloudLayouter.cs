using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectanglesCloudLayouter.Interfaces;
using RectanglesCloudLayouter.SpecialMethods;

namespace RectanglesCloudLayouter.Core
{
    public class CloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> _rectangles;
        private readonly ISpiral _spiral;
        public int CloudRadius { get; private set; }

        public CloudLayouter(Point center)
        {
            _rectangles = new List<Rectangle>();
            _spiral = new ArchimedeanSpiral(center);
        }

        public Rectangle GetCurrentRectangle => _rectangles.Last();

        public int RectanglesCount => _rectangles.Count;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Size width and height must be positive");
            var rectangle = new Rectangle(_spiral.GetNewSpiralPoint(), rectangleSize);
            while (RectanglesIntersection.IsAnyIntersectWithRectangles(rectangle, _rectangles))
                rectangle = new Rectangle(_spiral.GetNewSpiralPoint(), rectangleSize);
            _rectangles.Add(rectangle);
            UpdateCloudRadius(rectangle);
            return rectangle;
        }

        private void UpdateCloudRadius(Rectangle currentRectangle)
        {
            var maxDistance = new[]
            {
                PointsDistance.GetCeilingDistanceBetweenPoints(currentRectangle.Location, _spiral.Center),
                PointsDistance.GetCeilingDistanceBetweenPoints(
                    currentRectangle.Location + new Size(currentRectangle.Width, 0),
                    _spiral.Center),
                PointsDistance.GetCeilingDistanceBetweenPoints(
                    currentRectangle.Location + new Size(0, currentRectangle.Height),
                    _spiral.Center),
                PointsDistance.GetCeilingDistanceBetweenPoints(currentRectangle.Location + currentRectangle.Size,
                    _spiral.Center)
            }.Max();
            if (maxDistance > CloudRadius)
                CloudRadius = maxDistance;
        }
    }
}