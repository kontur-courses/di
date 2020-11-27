using System.Drawing;

namespace TagCloud.Core.ColoringAlgorithms
{
    public interface IColoringAlgorithm
    {
        public Color GetNextColor(Tag tag);
    }
}