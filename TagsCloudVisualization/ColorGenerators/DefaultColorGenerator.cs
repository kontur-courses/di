using System.Drawing;

namespace TagsCloudVisualization.ColorGenerators;

public class DefaultColorGenerator : IColorGenerator
{
    private readonly Color color;

    public DefaultColorGenerator(string color)
    {
        this.color = GetColorFromString(color);
    }

    public Color GetColor()
    {
        return color;
    }

    public Color GetColorFromString(string color)
    {
        try
        {
            return Color.FromName(color);
        }
        catch
        {
            throw new ArgumentException($"Color with name {color} doesn't supported");
        }
    }
}