using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public interface IColoringAlgorithm
{
    public Color[] GetColors(int count);
}