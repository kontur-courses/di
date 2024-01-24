using SixLabors.ImageSharp;
using TagsCloud.CustomAttributes;
using TagsCloudVisualization;

namespace TagsCloud.Colorizers;

[ColorizerName("OneVsRest")]
public class OneVsRestColorizer : ColorizerBase
{
    public OneVsRestColorizer(Color[] colors) : base(colors)
    {
        if (colors.Length != 2)
            throw new ArgumentException("Must be exactly 2 colors!");
    }

    public override void Colorize(Dictionary<CloudTag, int> frequencyStatistics)
    {
        var maxFrequency = frequencyStatistics.Values.Max();

        foreach (var pair in frequencyStatistics)
        {
            var textColor = pair.Value == maxFrequency ? colors[0] : colors[1];
            pair.Key.TextColor = textColor;
        }
    }
}