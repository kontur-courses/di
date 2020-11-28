using System.Drawing;

namespace WordCloudGenerator
{
    public interface IPalette
    {
        public Color GetNextColor();
    }
}