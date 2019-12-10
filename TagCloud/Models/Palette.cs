using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Models
{
    public class Palette
    {
        private int count { get; set; }
        public List<Color> Colors { get; }
        public string Name { get; }

        public Palette(string name, List<Color> colors)
        {
            count = 0;
            Colors = colors;
            Name = name;
        }

        public Palette(string name, params Color[] colors)
        {
            count = 0;
            Colors = colors.ToList();
            Name = name;
        }

        public Color GetNextColor()
        {
            if (count == Colors.Count)
                count = 0;
            var result = Colors[count];
            count++;
            return result;
        }
    }
}
