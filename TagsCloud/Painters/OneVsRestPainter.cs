using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Painters;

[Injection(ServiceLifetime.Singleton)]
public class OneVsRestPainter : IPainter
{
    public ColoringStrategy Strategy => ColoringStrategy.OneVsRest;

    public void Colorize(HashSet<WordTagGroup> wordGroups, Color[] colors)
    {
        if (colors.Length != 2)
            throw new ArgumentException("Must be exactly 2 colors!");

        var maxFrequency = wordGroups.Max(group => group.Count);

        foreach (var group in wordGroups)
        {
            var textColor = group.Count == maxFrequency ? colors[0] : colors[1];
            group.VisualInfo.TextColor = textColor;
        }
    }
}