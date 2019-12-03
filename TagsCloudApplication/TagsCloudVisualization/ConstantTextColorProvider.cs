using System.Drawing;

namespace TagsCloudVisualization
{
    public class ConstantTextColorProvider : ITextColorProvider
    {
        private readonly Color color;

        public ConstantTextColorProvider(Color color)
        {
            this.color = color;
        }

        public Color GetTextColor(string text, Rectangle rectangle)
        {
            return color;
        }
    }
}
