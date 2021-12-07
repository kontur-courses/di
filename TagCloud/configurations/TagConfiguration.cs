using System.Drawing;

namespace TagCloud.configurations
{
    public class TagConfiguration : ITagConfiguration
    {
        private readonly Color color;
        private readonly Font font;

        public TagConfiguration(Color color, Font font)
        {
            this.color = color;
            this.font = font;
        }

        public Color GetColor() => color;

        public Font GetFont() => font;
    }
}