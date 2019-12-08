using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class Palette
    {
        public Color BackgroundColor { get; set; }
        public IEnumerable<Color> WordsColors { get; set; }

        public Palette()
        {
            BackgroundColor = Color.Black;
            WordsColors = new List<Color>{Color.OrangeRed, Color.Crimson, Color.Chartreuse};
        }
    }
}
