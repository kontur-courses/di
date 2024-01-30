using SixLabors.ImageSharp;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface IPainter
{
    ColoringStrategy Strategy { get; }
    void Colorize(HashSet<WordTagGroup> wordGroups, Color[] colors);
}