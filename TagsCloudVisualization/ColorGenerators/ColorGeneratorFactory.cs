using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ColorGenerators;

public class ColorGeneratorFactory : IColorGeneratorFactory
{
    private readonly ColorGeneratorSettings settings;

    public ColorGeneratorFactory(ColorGeneratorSettings settings)
    {
        this.settings = settings;
    }

    public IColorGenerator Create()
    {
        return settings.Color switch
        {
            "random" => new RandomColorGenerator(),
            _ => new DefaultColorGenerator(settings.Color),
        };
    }
}