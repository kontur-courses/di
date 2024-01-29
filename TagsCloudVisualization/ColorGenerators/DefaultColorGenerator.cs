using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ColorGenerators;

public class DefaultColorGenerator : IColorGenerator
{
    private readonly Color color;
    private readonly bool isMatch;

    public DefaultColorGenerator(ColorGeneratorSettings settings)
    {
        isMatch = TryGetColorFromString(settings.Color, out color);
    }

    public Color GetColor()
    {
        return color;
    }

    public bool TryGetColorFromString(string color, out Color resultColor)
    {
        resultColor = Color.Black;
        try
        {
            resultColor = Color.FromName(color);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Match()
    {
        return isMatch;
    }
}