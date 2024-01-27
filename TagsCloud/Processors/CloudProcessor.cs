using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

public class CloudProcessor
{
    private readonly ICloudProcessorOptions cloudOptions;
    private readonly IPainter[] painters;

    public CloudProcessor(
        ICloudProcessorOptions cloudOptions,
        IEnumerable<IPainter> painters)
    {
        this.cloudOptions = cloudOptions;
        this.painters = painters.ToArray();
    }

    public void SetPositions(HashSet<WordTagGroup> wordGroups)
    {
        var layout = cloudOptions.Layout;

        foreach (var group in wordGroups)
        {
            var textOptions = new TextOptions(group.VisualInfo.TextFont);
            var fontRect = TextMeasurer.MeasureAdvance(group.WordInfo.Text, textOptions);
            var rectangle = layout.PutNextRectangle(new SizeF(fontRect.Width, fontRect.Height));

            if (!fontRect.Width.IsEqualTo(rectangle.Width))
                group.VisualInfo.IsRotated = true;

            group.VisualInfo.BoundsRectangle = rectangle;
        }
    }

    public void SetFonts(HashSet<WordTagGroup> wordGroups)
    {
        var minFontSize = cloudOptions.MinFontSize;
        var maxFontSize = cloudOptions.MaxFontSize;

        var maxFrequency = wordGroups.Max(group => group.Count);

        foreach (var group in wordGroups)
        {
            var fontSize = minFontSize + (float)group.Count / maxFrequency * (maxFontSize - minFontSize);
            group.VisualInfo.TextFont = cloudOptions.FontFamily.CreateFont((int)fontSize, FontStyle.Regular);
        }
    }

    public void SetColors(HashSet<WordTagGroup> wordGroups)
    {
        var painter = painters
            .SingleOrDefault(colorizer => colorizer.Strategy == cloudOptions.ColoringStrategy);

        painter!.Colorize(wordGroups, cloudOptions.Colors);
    }
}