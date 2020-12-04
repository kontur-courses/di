using System.Drawing;

namespace TagCloudCreator
{
    public class SingleColorSelector : IColorSelector
    {
        private readonly Color color;

        public SingleColorSelector(Color color)
        {
            this.color = color;
        }

        public string Name => color.Name + " color";

        public Color GetColor(DrawingWord word)
        {
            return Color.Black;
        }
    }
}