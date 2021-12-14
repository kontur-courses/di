using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Drawing
{
    internal class SimplePalette : IPalette
    {
        private List<Color>? _colors;
        public Color BackgroundColor { get; private set; }

        public Color GetNextColor()
        {
            var rnd = new Random();
            return _colors == null || _colors.Count == 0
                ? Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256))
                : _colors[rnd.Next(_colors.Count)];
        }

        public IPalette WithWordColors(List<Color> colors)
        {
            _colors = colors;
            return this;
        }

        public IPalette WithBackGroundColor(Color color)
        {
            BackgroundColor = color;
            return this;
        }
    }
}