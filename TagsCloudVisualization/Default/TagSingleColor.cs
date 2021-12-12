using System.Drawing;

namespace TagsCloudVisualization.Default
{
    public class TagSingleColor : ITokenColorChooser
    {
        private Color color;

        public TagSingleColor(Color color)
        {
            this.color = color;
        }

        public Color GetTokenColor(Token token)
        {
            return color;
        }
    }
}