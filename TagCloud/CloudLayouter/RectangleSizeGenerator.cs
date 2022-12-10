using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.CloudLayouter
{
    public static class RectangleSizeGenerator
    {
        private static readonly Random Random = new Random();

        public static IReadOnlyList<Size> GetConstantSizes(int count, Size size)
        {
            var result = new List<Size>();

            for (var i = 0; i < count; i++)
                result.Add(size);

            return result;
        }
    }
}