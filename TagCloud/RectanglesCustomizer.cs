using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Models;

namespace TagCloud
{
    public static class RectanglesCustomizer
    {
        public static List<ColorTagRectangle> GetRectanglesWithPalette(Palette palette,
            List<TagRectangle> tagRectangles)
        {
            return palette.Name == "RandomColors"
                ? GetRectanglesWithRandomColor(tagRectangles)
                : GetRectanglesWithSimplePalette(palette, tagRectangles);
        }

        private static List<ColorTagRectangle> GetRectanglesWithRandomColor(List<TagRectangle> tagRectangels)
        {
            var random = new Random();
            return tagRectangels.Select(t => new ColorTagRectangle(
                    t.Tag,
                    t.Area,
                    Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255))))
                .ToList();
        }

        private static List<ColorTagRectangle> GetRectanglesWithSimplePalette(Palette palette,
            List<TagRectangle> tagRectangles)
        {
            return tagRectangles.Select(t => new ColorTagRectangle(
                    t.Tag,
                    t.Area,
                    palette.GetNextColor()))
                .ToList();
        }
    }
}