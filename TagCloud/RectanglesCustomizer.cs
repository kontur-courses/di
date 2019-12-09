﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Models;

namespace TagCloud
{
    public static class RectanglesCustomizer
    {
        public static List<ColorTagRectangle> GetReactanglesWithRandomColor(List<TagRectangle> tagRectangels)
        {
            var random = new Random();
            return tagRectangels.Select(t => new ColorTagRectangle(
                    t.Tag,
                    t.Area,
                    Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255))))
                .ToList();
        }
    }
}
