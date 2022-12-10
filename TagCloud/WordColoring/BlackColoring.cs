using System.Drawing;

namespace TagCloud.WordColoring
{
    public class BlackColoring : IWordColoring
    {
        public Color GetColor(double factor)
        {
            return Color.Black;
        }
    }
}
