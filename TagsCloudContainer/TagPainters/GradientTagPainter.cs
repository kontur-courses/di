using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class GradientTagPainter : ITagPainter
{
    private readonly Settings settings;

    public GradientTagPainter(Settings settings)
    {
        this.settings = settings;
    }

    public IEnumerable<PaintedTag> Paint(IEnumerable<Tag> tags)
    {
        var primary = settings.Palette.Primary;
        var secondary = Color.FromArgb(255 - primary.R,
            255 - primary.G, 255 - primary.B);
        var deltaR = secondary.R - primary.R;
        var deltaG = secondary.G - primary.G;
        var deltaB = secondary.B - primary.B;

        foreach (var tag in tags)
        {
            var color = Color.FromArgb(
                primary.R + (int)(deltaR * tag.Frequency),
                primary.G + (int)(deltaG * tag.Frequency), 
                primary.B + (int)(deltaB * tag.Frequency));
            yield return new PaintedTag(tag, color);
        }
    }
}