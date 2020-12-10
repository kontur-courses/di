using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer;

namespace TagsCloudContainer
{
    class CircularCloudLayouter : ICloudLayouter
    {
        public List<Rectangle> Rectangles { get; set; }
        private const int RollBackPixelsCount = 10; // Оптимальное значение величины отката радиуса спирали в пикселях
        private Spiral _spiral;
        private bool _buildingStarted;

        public CircularCloudLayouter()
        {
            Rectangles = new List<Rectangle>();
            var center = new Point(100, 100);
            _spiral = new Spiral(center, this);
            _buildingStarted = false;
        }

        public void ChangeCenter(Point newCenter)
        {
            if (!_buildingStarted)
                _spiral = new Spiral(newCenter, this);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            _buildingStarted = true;
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException(
                    $"Invalid rectangleSize = ({rectangleSize.Width}, {rectangleSize.Height})", nameof(rectangleSize));
            _spiral.RollBackRadius(RollBackPixelsCount);
            while (true)
            {
                var currentCoordinate = _spiral.GetNextPosition(rectangleSize);
                var currentRectangle = new Rectangle(currentCoordinate, rectangleSize);

                if (!CheckIntersections(currentRectangle))
                {
                    _spiral.ShiftRectangle(ref currentRectangle);
                    Rectangles.Add(currentRectangle);
                    return currentRectangle;
                }
            }
        }

        public bool CheckIntersections(Rectangle rectangle)
        {
            return Rectangles.Any(otherRectangle => rectangle.IntersectsWith(otherRectangle));
        }
    }
}
