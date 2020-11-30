using System.Drawing;

namespace TagCloudCreator
{
    public class BlackColorSelector : IColorSelector
    {
        public string Name => "Black color";

        public Color GetColor(DrawingWord word)
        {
            return Color.Black;
        }
    }
}