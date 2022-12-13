using System.Drawing;

namespace TagCloud.WordColoring
{
    public class BlackColoring : IWordColoring
    {
        public string Name => "Black";

        public Color GetColor(double factor)
        {
            return Color.Black;
        }
    }
}
