using System;

namespace TagsCloudContainer.CircularCloudLayouter
{
    public static class DoubleExtension
    {
        public static double AngleToStandardValue(this double angle)
        {
            return angle % (Math.PI * 2);
        }

        public static Quadrant DetermineQuadrantByDirection(this double direction)
        {
            direction = AngleToStandardValue(direction);
            if (direction >= 0 && direction <= Math.PI / 2)
                return Quadrant.First;
            if (direction > Math.PI / 2 && direction <= Math.PI)
                return Quadrant.Second;
            if (direction > Math.PI && direction <= 3 * Math.PI / 2)
                return Quadrant.Third;

            return Quadrant.Fourth;
        }
    }
}