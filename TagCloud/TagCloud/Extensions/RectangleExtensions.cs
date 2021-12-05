using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class RectangleExtensions
    {
        /// <summary>
        /// Находит расстояние от точки внутри прямоугольника до каждой его стороны
        /// </summary>
        /// <param name="rect">Прямоугольник</param>
        /// <param name="point">Точка внутри прямоугольника</param>
        /// <returns>Возвращает расстояния в порядке left, top, right, bottom (все расстояния положительные)</returns>
        public static List<int> GetDistancesToInnerPoint(this Rectangle rect, Point point)
        {
            var left = point.X - rect.Left;
            var top = point.Y - rect.Top;
            var right = rect.Right - point.X;
            var bottom = rect.Bottom - point.Y;

            var distances = new List<int> { left, top, right, bottom };
            if (distances.Any(d => d < 0))
                throw new ArgumentException("Точка расположена вне прямоугольника");
            return distances;
        }

        /// <summary>
        /// Возвращает новый прямоугольник полученный путем пересечения двух прямоугольников
        /// </summary>
        /// <exception cref="ArgumentException">Если прямоугольники не пересекаются</exception>
        public static Rectangle GetIntersection(this Rectangle first, Rectangle second)
        {
            if (!first.IntersectsWith(second))
                throw new ArgumentException("Прямоугольники не имеют пересечения");
            first.Intersect(second);
            return first;
        }

        /// <summary>
        /// Находит пересечение двух прямоугольников, если они пересекаются
        /// </summary>
        /// <param name="intersection">Новый прямоугольник полученный путем пересечения двух прямоугольников</param>
        /// <returns>true, если прямоугольники пересекаются, в противном случае — false.</returns>
        public static bool TryGetIntersection(this Rectangle first, Rectangle second, out Rectangle? intersection)
        {
            if (!first.IntersectsWith(second))
            {
                intersection = Rectangle.Empty;
                return false;
            }
            first.Intersect(second);
            intersection = first;
            return true;
        }

        public static Rectangle UnionRange(this Rectangle rectangle, IEnumerable<Rectangle> others)
        {
            var union = rectangle;
            foreach (var r in others)
                union = Rectangle.Union(union, r);
            return union;
        }
    }
}
