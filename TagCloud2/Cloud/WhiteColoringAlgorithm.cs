using System.Drawing;

namespace TagCloud2.Cloud
{
    public class WhiteColoringAlgorithm : IColoringAlgorithm
    {
        public Color GetColor(Rectangle rectangle)
        {
            return Color.FromArgb(255, 255, 255);
        }
    }
}
