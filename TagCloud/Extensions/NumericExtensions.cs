using System;

namespace TagCloud.Extensions
{
    public static class NumericExtensions
    {
        public static double ToRadians(this float value)
        {
            return Math.PI / 180 * value;
        }
    }
}