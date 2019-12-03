using System.Drawing;

namespace TagsCloudVisualization
{
    public class SimpleColorGenerator : IColorGenerator
    {
        private readonly Color color;

        public SimpleColorGenerator(Color color)
        {
            this.color = color;
        }

        public Color GenerateTextColor(string text, Rectangle rectangle)
        {
            return color;
        }
    }
}
