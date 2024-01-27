using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Painters;

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