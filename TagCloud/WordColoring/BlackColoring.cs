using System.Drawing;

namespace TagCloud
{
    public class BlackColoring : IWordColoring
    {
        public Color GetColor(double factor)
        {
            return Color.Black;
        }
    }
}
