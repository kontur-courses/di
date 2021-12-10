using System.Drawing;

namespace TagCloud.Templates.Colors
{
    public class OneColorGenerator : IColorGenerator
    {
        private readonly Color color;

        public OneColorGenerator(Color color)
        {
            this.color = color;
        }

        public Color GetColor(string word)
        {
            return color;
        }
    }
}