using System.Drawing;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class TagPainter : ITagPainter
{
    private Dictionary<WordType, Color> wordColors;

    public TagPainter(Settings settings)
    {
        wordColors = settings.Palette.WordColors;
    }

    public IEnumerable<PaintedTag> Paint(IEnumerable<Tag> tags)
    {
        foreach (var tag in tags)
        {
            if (wordColors.TryGetValue(tag.WordType, out var color))
                yield return new PaintedTag(tag, color);
            if (!wordColors.ContainsKey(WordType.Default))
                throw new ArgumentException("No default color received!");

            yield return new PaintedTag(tag, wordColors[WordType.Default]);
        }
    }
}