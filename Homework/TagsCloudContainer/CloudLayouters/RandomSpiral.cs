using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public class RandomSpiral : ISpiral
    {
        private Point cloudCenter;
        private Quarter previuosQuarter;
        private Point currentPoint;
        private Dictionary<Quarter, Func<Point, bool>> quarterCheckers;
        private int tempMin = -10000; 
        private int tempMax = 10000;

        public RandomSpiral(Point cloudCenter)
        {
            this.cloudCenter = cloudCenter;
            this.previuosQuarter = Quarter.Forth;
            this.currentPoint = cloudCenter;
            quarterCheckers = new Dictionary<Quarter, Func<Point, bool>>()
            {
                {Quarter.Forth, p => p.X > cloudCenter.X && p.Y < cloudCenter.Y},
                {Quarter.First, p => p.X < cloudCenter.X && p.Y < cloudCenter.Y},
                {Quarter.Second, p => p.X < cloudCenter.X && p.Y > cloudCenter.Y},
                {Quarter.Third, p => p.X > cloudCenter.X && p.Y > cloudCenter.Y},
            };
        }
        public Point GetNextCurvePoint()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            while (IsntPointAtNextQuarter())
                currentPoint = new Point(rnd.Next(tempMin, tempMax), rnd.Next(tempMin, tempMax));
            previuosQuarter = GetNextQuarter();
            return currentPoint;
        }

        private bool IsntPointAtNextQuarter()
        {
            return !quarterCheckers[previuosQuarter](currentPoint);
        }

        private Quarter GetNextQuarter()
        {
            if (previuosQuarter == Quarter.Forth) return Quarter.First;
            if (previuosQuarter == Quarter.First) return Quarter.Second;
            if (previuosQuarter == Quarter.Second) return Quarter.Third;
            return Quarter.Forth;
        }
    }
}
