using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Extensions
{
    internal static class RectangleExtensions
    {
        /// <summary>
        ///     Находит расстояние от точки внутри прямоугольника до каждой его стороны
        /// </summary>
        /// <returns>Возвращает расстояния в порядке left, top, right, bottom (все расстояния положительные)</returns>
        public static List<int> GetDistancesToInnerPoint(this Rectangle rect, Point point)
        {
            var left = point.X - rect.Left;
            var top = point.Y - rect.Top;
            var right = rect.Right - point.X;
            var bottom = rect.Bottom - point.Y;

            var distances = new List<int> {left, top, right, bottom};
            if (distances.Any(d => d < 0))
                throw new ArgumentException("Точка расположена вне прямоугольника");
            return distances;
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