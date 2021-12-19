using System.Drawing;

namespace TagCloud2
{
    public interface IColoringAlgorithm
    {
        public Color GetColor(Rectangle rectangle);
    }
}
