using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public class RandomSpiral : ISpiral
    {
        private Quarter previuosQuarter;
        private Point currentPoint;
        private readonly Dictionary<Quarter, Func<Point, bool>> quarterCheckers;
        private const int TempMin = -10000; 
        private const int TempMax = 10000;

        public RandomSpiral(Point cloudCenter)
        {
            previuosQuarter = Quarter.Forth;
            currentPoint = cloudCenter;
            quarterCheckers = new Dictionary<Quarter, Func<Point, bool>>
            {
                {Quarter.Forth, p => p.X > cloudCenter.X && p.Y < cloudCenter.Y},
                {Quarter.First, p => p.X < cloudCenter.X && p.Y < cloudCenter.Y},
                {Quarter.Second, p => p.X < cloudCenter.X && p.Y > cloudCenter.Y},
                {Quarter.Third, p => p.X > cloudCenter.X && p.Y > cloudCenter.Y}
            };
        }
        public Point GetNextCurvePoint()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            while (IsntPointAtNextQuarter())
                currentPoint = new Point(rnd.Next(TempMin, TempMax), rnd.Next(TempMin, TempMax));
            previuosQuarter = GetNextQuarter();
            return currentPoint;
        }

        private bool IsntPointAtNextQuarter()
        {
            return !quarterCheckers[previuosQuarter](currentPoint);
        }

        private Quarter GetNextQuarter()
        {
            switch (previuosQuarter)
            {
                case Quarter.Forth: return Quarter.First;
                case Quarter.First: return Quarter.Second;
                case Quarter.Second: return Quarter.Third;
                default: return Quarter.Forth;
            }
        }
    }
}
