﻿using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Layouter
{
    public static class RectangleExtensions
    {
        public static bool IsIntersectsWithAnyRect(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(r => r.Intersects(rectangle));
        }
    }
}