using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.ShapeGenerator
{
    public class ArchimedeanShape : IShapeGenerator
    {
        public Point Center { get; }
        private IEnumerator<Point> spiralPoints;
        private readonly double spiralStep;

        public ArchimedeanShape(Point center, double spiralStep)
        {
            this.Center = center;
            this.spiralStep = spiralStep;
            spiralPoints = GetAllSpiralPoints().GetEnumerator();
        }

        private IEnumerable<Point> GetAllSpiralPoints()
        {
            var currentPos = Center;
            var angle = 0.0;

            while (currentPos.X < int.MaxValue ||
                   currentPos.Y < int.MaxValue)
            {
                var nextPos = new Point(
                    Center.X + (int) Math.Round(angle * Math.Sin(angle)),
                    Center.Y + (int) Math.Round(angle * Math.Cos(angle)));
                angle += spiralStep;

                if (currentPos == nextPos && currentPos != Center) continue;

                currentPos = nextPos;
                yield return nextPos;
            }
        }

        public Point GetNextSpiralPoint()
        {
            spiralPoints.MoveNext();
            return spiralPoints.Current;
        }

        public List<Point> GetNextSpiralPoints(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count of elements need be more than zero");

            var result = new List<Point>();
            for (var i = 0; i < count; i++)
                result.Add(GetNextSpiralPoint());

            return result;
        }

        public void ResetSpiral() =>
            spiralPoints = GetAllSpiralPoints().GetEnumerator();
    }
}