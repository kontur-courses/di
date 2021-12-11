using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class FrequencyTagPainter : ITagPainter
{
    private readonly Settings settings;

    public FrequencyTagPainter(Settings settings)
    {
        this.settings = settings;
    }

    public IEnumerable<PaintedTag> Paint(IEnumerable<Tag> tags)
    {
        foreach (var tag in tags)
        {
            var alpha = ((int)(255 * tag.Frequency) + 100) % 255;
            var cl = Color.FromArgb(alpha, settings.Palette.Primary);
            yield return new PaintedTag(tag, cl);
        }
    }
}