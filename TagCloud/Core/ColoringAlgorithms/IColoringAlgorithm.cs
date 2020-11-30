using System.Drawing;

namespace TagCloud.Core.ColoringAlgorithms
{
    public interface IColoringAlgorithm
    {
        public string Name { get; }
        public Color GetNextColor(Tag tag);
    }
}