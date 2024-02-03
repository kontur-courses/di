using System.Drawing;
using TagsCloud.Entities;

namespace TagsCloud.ColorGenerators;

public interface IColorGenerator
{
    Color GetTagColor(Tag tag);
}