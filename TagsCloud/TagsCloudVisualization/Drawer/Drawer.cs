using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public class Drawer : IDrawer
    {
        public void Draw(Graphics graphics, Size size, IEnumerable<IDrawable> drawables)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            var bounds = new Rectangle(Point.Empty, size);
            var shifted = drawables.Select(tag => tag.Shift(Size.Truncate(size / 2f)));
            var validated = shifted.Select(drawable => Validate(drawable, bounds));
            foreach (var drawable in validated) drawable.Draw(graphics);
        }

        private static IDrawable Validate(IDrawable drawable, Rectangle bounds)
        {
            if (!bounds.Contains(drawable.Bounds))
                throw new Exception("Image cannot contain all rectangles");
            return drawable;
        }
    }
}
#pragma warning restore CA1416