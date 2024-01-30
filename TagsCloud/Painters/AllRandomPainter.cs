using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Painters;

[Injection(ServiceLifetime.Singleton)]
public class AllRandomPainter : IPainter
{
    private readonly Random random = new();

    public ColoringStrategy Strategy => ColoringStrategy.AllRandom;

    public void Colorize(HashSet<WordTagGroup> wordGroups, Color[] colors)
    {
        foreach (var group in wordGroups)
        {
            var index = random.Next(0, colors.Length);
            group.VisualInfo.TextColor = colors[index];
        }
    }
}