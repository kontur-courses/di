using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Models
{
    public class Palette
    {
        public Palette(string name, List<Color> colors)
        {
            Count = 0;
            Colors = colors;
            Name = name;
        }

        public Palette(string name, params Color[] colors)
        {
            Count = 0;
            Colors = colors.ToList();
            Name = name;
        }

        private int Count { get; set; }
        public List<Color> Colors { get; }
        public string Name { get; }

        public Color GetNextColor()
        {
            if (Count == Colors.Count)
                Count = 0;
            var result = Colors[Count];
            Count++;
            return result;
        }
    }
}