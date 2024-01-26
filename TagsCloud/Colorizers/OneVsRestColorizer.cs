using SixLabors.ImageSharp;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Colorizers;

[ColorizerName(ColoringStrategy.OneVsRest)]
public class OneVsRestColorizer : ColorizerBase
{
    public OneVsRestColorizer(IList<Color> colors) : base(colors)
    {
        if (colors.Count != 2)
            throw new ArgumentException("Must be exactly 2 colors!");
    }

    public override void Colorize(IDictionary<CloudTag, int> frequencyStatistics)
    {
        var maxFrequency = frequencyStatistics.Values.Max();

        foreach (var pair in frequencyStatistics)
        {
            var textColor = pair.Value == maxFrequency ? colors[0] : colors[1];
            pair.Key.TextColor = textColor;
        }
    }
}