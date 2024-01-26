using System.Drawing;

namespace TagsCloud.ColorGenerators;

public interface IColorGenerator
{
    Color GetTagColor();
}