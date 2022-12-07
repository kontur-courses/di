using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public interface IColoringAlgorithm
{
    public Color GetNextColor();
}