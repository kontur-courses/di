using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Drawable;

namespace TagsCloudVisualization.Extensions
{
    internal static class DrawableExtensions
    {
        public static Size GetMinCanvasSize(this IEnumerable<IDrawable> rectangles)
        {
            var (left, right) = (int.MaxValue, int.MinValue);
            var (bottom, top) = (int.MinValue, int.MaxValue);
            foreach (var rectangle in rectangles)
            {
                right = Math.Max(right, rectangle.Bounds.Right);
                bottom = Math.Max(bottom, rectangle.Bounds.Bottom);
                left = Math.Min(left, rectangle.Bounds.Left);
                top = Math.Min(top, rectangle.Bounds.Top);
            }

            return new Size(right - left, bottom - top);
        }
    }
}