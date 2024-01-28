using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

public class CloudProcessor
{
    private readonly ICloudProcessorOptions cloudOptions;
    private readonly IEnumerable<IFontMeasurer> fontMeasurers;
    private readonly IEnumerable<IPainter> painters;

    public CloudProcessor(
        ICloudProcessorOptions cloudOptions,
        IEnumerable<IPainter> painters,
        IEnumerable<IFontMeasurer> fontMeasurers)
    {
        this.cloudOptions = cloudOptions;
        this.painters = painters;
        this.fontMeasurers = fontMeasurers;
    }

    public void SetPositions(HashSet<WordTagGroup> wordGroups)
    {
        var layout = cloudOptions.Layout;
        wordGroups = GetSortedGroups(wordGroups);

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
        var minFrequency = wordGroups.Min(group => group.Count);

        var measurer = FindFontMeasurer();

        foreach (var group in wordGroups)
        {
            var fontSize = measurer.GetFontSize(
                group.Count,
                maxFrequency,
                minFrequency,
                maxFontSize,
                minFontSize);

            group.VisualInfo.TextFont = cloudOptions.FontFamily.CreateFont(fontSize, FontStyle.Regular);
        }
    }

    public void SetColors(HashSet<WordTagGroup> wordGroups)
    {
        var painter = painters
            .SingleOrDefault(colorizer => colorizer.Strategy == cloudOptions.ColoringStrategy);

        painter!.Colorize(wordGroups, cloudOptions.Colors);
    }

    private IFontMeasurer FindFontMeasurer()
    {
        return fontMeasurers.SingleOrDefault(measurer => measurer.Type == cloudOptions.MeasurerType);
    }

    private HashSet<WordTagGroup> GetSortedGroups(HashSet<WordTagGroup> wordGroups)
    {
        return cloudOptions.Sort switch
        {
            SortType.Ascending => wordGroups.OrderBy(group => group.Count).ToHashSet(),
            SortType.Descending => wordGroups.OrderByDescending(group => group.Count).ToHashSet(),
            _ => wordGroups
        };
    }
}