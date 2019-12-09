using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class Palette
    {
        public Color BackgroundColor { get; set; }
        public Color[] WordsColors { get; set; }

        public Palette()
        {
            BackgroundColor = Color.Black;
            WordsColors = new[] {Color.OrangeRed, Color.Crimson, Color.Chartreuse};
        }
    }
}
