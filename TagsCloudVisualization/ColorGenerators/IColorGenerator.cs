using System.Drawing;

namespace TagsCloudVisualization.ColorGenerators;

public interface IColorGenerator
{
    bool Match();
    Color GetColor();
}