using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Drawable;

namespace TagsCloudVisualization.DrawableContainers
{
    public class DrawableContainer : IDrawableContainer
    {
        private readonly List<IDrawable> drawables = new();
        private int left = int.MaxValue;
        private int right = int.MinValue;
        private int bottom = int.MinValue;
        private int top = int.MaxValue;
        
        public void AddDrawable(IDrawable tag)
        {
            right = Math.Max(right, tag.Bounds.Right);
            bottom = Math.Max(bottom, tag.Bounds.Bottom);
            left = Math.Min(left, tag.Bounds.Left);
            top = Math.Min(top, tag.Bounds.Top);
            drawables.Add(tag);
        }

        public IEnumerable<IDrawable> GetDrawables() => drawables;

        public Size GetMinCanvasSize() => new (right - left, bottom - top);
    }
}