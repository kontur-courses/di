using System.Drawing;

namespace WordCloudGenerator
{
    public interface IPalette
    {
        public Color BackgroundColor { get; }
        public Color GetNextColor();
    }
}