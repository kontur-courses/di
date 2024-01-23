using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ColorGenerators;

public class ColorGenerator : IColorGenerator
{
    private readonly IColorGenerator generator;

    public ColorGenerator(FontSettings settings)
    {
        generator = settings.Color switch
        {
            "random" => new RandomColorGenerator(),
            _ => new DefaultColorGenerator(settings.Color), 
        };
    }

    public Color GetColor()
    {
        return generator.GetColor();
    }
}
