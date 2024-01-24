using SixLabors.ImageSharp;
using TagsCloud.CustomAttributes;
using TagsCloudVisualization;

namespace TagsCloud.Colorizers;

[ColorizerName("AllRandom")]
public class AllRandomColorizer : ColorizerBase
{
    private readonly Random random;

    public AllRandomColorizer(Color[] colors) : base(colors)
    {
        random = new Random();
    }

    public override void Colorize(Dictionary<CloudTag, int> frequencyStatistics)
    {
        foreach (var pair in frequencyStatistics)
        {
            var index = random.Next(0, colors.Length);
            pair.Key.TextColor = colors[index];
        }
    }
}