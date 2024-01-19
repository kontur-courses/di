using System.Drawing;
using TagCloudTests;

namespace TagCloud;

public class ConstantColorSelector : IColorSelector
{
    private Color color;
    public ConstantColorSelector(Color color)
    {
        this.color = color;
    }

    public Color PickColor() => color;
}