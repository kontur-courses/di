using SixLabors.ImageSharp;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Colorizers;

[ColorizerName(ColoringStrategy.AllRandom)]
public class AllRandomColorizer : ColorizerBase
{
    private readonly Random random = new();

    public AllRandomColorizer(IList<Color> colors) : base(colors)
    {
    }

    public override void Colorize(IDictionary<CloudTag, int> frequencyStatistics)
    {
        foreach (var pair in frequencyStatistics)
        {
            var index = random.Next(0, colors.Count);
            pair.Key.TextColor = colors[index];
        }
    }
}