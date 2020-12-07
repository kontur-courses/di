using System;
using System.Drawing;
using TagCloud.Infrastructure.Settings;

namespace TagCloud.Infrastructure.Layout.Strategies
{
    public class SpiralStrategy : ILayoutStrategy
    {
        private readonly Func<ISpiralSettingsProvider> settingProvider;

        public SpiralStrategy(Func<ISpiralSettingsProvider> settingProvider)
        {
            this.settingProvider = settingProvider;
        }

        public Point GetPoint(Func<Point, bool> isValidPoint)
        {
            var angle = 0;

            var obtainedPoint = settingProvider().Center;
            while (!isValidPoint(obtainedPoint))
            {
                var possiblePoint = settingProvider().Center +
                                    new Size((int) (Math.Sin(angle) * angle), (int) (Math.Cos(angle) * angle));
                obtainedPoint = possiblePoint;
                angle += settingProvider().Increment;
            }

            return OptimizePoint(obtainedPoint, isValidPoint);
        }

        private Point OptimizePoint(Point obtainedPoint, Func<Point, bool> isValidPoint)
        {
            var possiblePosition = obtainedPoint;
            while (isValidPoint(possiblePosition))
            {
                obtainedPoint = possiblePosition;
                var optimizationOffset = new Size(
                    Math.Sign(settingProvider().Center.X - obtainedPoint.X),
                    Math.Sign(settingProvider().Center.Y - obtainedPoint.Y));
                if (optimizationOffset.IsEmpty)
                    return obtainedPoint;
                possiblePosition = obtainedPoint + optimizationOffset;
            }

            return obtainedPoint;
        }
    }
}