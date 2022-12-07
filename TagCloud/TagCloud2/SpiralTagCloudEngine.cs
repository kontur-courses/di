using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud2.Interfaces;

namespace TagCloud2
{
    /*
     * Реализация облака тегов на основе спирального алгоритма
     * Рекомендуется использовать полотно
     */
    public class SpiralTagCloudEngine : ITagCloudEngine
    {
        private readonly Point _centerPoint;
        private readonly int _skipPoints;
        private readonly float _density;
        private readonly float _angleOffset;
        public List<Rectangle> Rectangles { get; } = new();
        private double _currentSpiralAngle;
        private float _currentSpiralScale;


        /// <param name="centerPoint">Центральная точка, относительно которой будут располагаться теги.</param>
        /// <param name="skipPoints">Сколько точек в спирали пропускать. Увеличивает производительность, уменьшая точность.</param>
        /// <param name="density">Расстояние между линиями спирали. Чем меньше, тем выше плотность.</param>
        /// <param name="angleOffset">Смещение следующей точки спирали.</param>
        public SpiralTagCloudEngine(
            Point centerPoint,
            int skipPoints = 2,
            float density = 0.00001f,
            float angleOffset = (float)(Math.PI / 360))
        {
            if (centerPoint.X <= 0 || centerPoint.Y <= 0)
                throw new ArgumentException(nameof(centerPoint));

            _centerPoint = centerPoint;
            _skipPoints = skipPoints;
            _density = density;
            _angleOffset = angleOffset;
        }

        public Rectangle PutNextRectangle(Size sizeOfRectangle)
        {
            if (sizeOfRectangle.Height <= 0 || sizeOfRectangle.Width <= 0)
                throw new ArgumentException("Sizes must be bigger than 0");

            Point currentPoint;
            bool isIntersectWithSpiral;

            // проверим, есть ли пересечение прямоугольника, построенного
            // на этой точке, с другими прямоугольниками.
            // если есть, то будем получать до тех пор, пока
            // не найдем подходящую точку.
            do
            {
                currentPoint = GetNextSpiralPoint();
                isIntersectWithSpiral = Rectangles
                    .Any(r => r.IntersectsWith(new Rectangle(
                        // так как точка - это центр, нам достаточно
                        // вычесть половины сторон, чтобы получить коорд-ы угла
                        currentPoint - sizeOfRectangle / 2,
                        sizeOfRectangle)));
            } while (isIntersectWithSpiral);

            var foundRectangle = new Rectangle(
                currentPoint - sizeOfRectangle / 2,
                sizeOfRectangle);
            Rectangles.Add(foundRectangle);
            return foundRectangle;
        }

        private Point GetNextSpiralPoint()
        {
            var point = new Point(
                (int)(_centerPoint.X * (1 + _currentSpiralScale * Math.Cos(_currentSpiralAngle))),
                (int)(_centerPoint.Y * (1 + _currentSpiralScale * Math.Sin(_currentSpiralAngle)))
                );
            _currentSpiralAngle += _angleOffset * (_skipPoints + 1);
            _currentSpiralScale += _density * (_skipPoints + 1);
            return point;
        }
    }
}