using System.Drawing;

namespace TagsCloudVisualization.ColorGenerators;

public class ColorGenerator : IColorGenerator
{
    private readonly IColorGenerator generator;

    public ColorGenerator(string color)
    {
        generator = color switch
        {
            "random" => new RandomColorGenerator(),
            _ => new DefaultColorGenerator(color), 
        };
    }

    public Color GetColor()
    {
        return generator.GetColor();
    }
}
