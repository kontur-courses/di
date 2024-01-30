using Microsoft.Extensions.DependencyInjection;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

[Injection(ServiceLifetime.Singleton)]
public class CloudProcessor : ICloudProcessor
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
        this.fontMeasurers = fontMeasurers;
        this.painters = painters;
    }

    public void SetPositions(HashSet<WordTagGroup> wordGroups)
    {
        var layout = cloudOptions.Layout;

        foreach (var group in GetSortedGroups(wordGroups))
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
        var (minFontSize, maxFontSize) = (cloudOptions.MinFontSize, cloudOptions.MaxFontSize);
        var (minFrequency, maxFrequency) = GetMinMaxValues(wordGroups);

        var measurer = FindFontMeasurer();

        foreach (var group in wordGroups)
        {
            var fontSize = measurer.GetFontSize(
                group.Count,
                minFrequency,
                maxFrequency,
                minFontSize,
                maxFontSize);

            group.VisualInfo.TextFont = cloudOptions.FontFamily.CreateFont(fontSize, FontStyle.Regular);
        }
    }

    public void SetColors(HashSet<WordTagGroup> wordGroups)
    {
        var painter = painters.Single(painter => painter.Strategy == cloudOptions.ColoringStrategy);
        painter.Colorize(wordGroups, cloudOptions.Colors);
    }

    private static (int minValue, int maxValue) GetMinMaxValues(IEnumerable<WordTagGroup> wordGroups)
    {
        var (minValue, maxValue) = (int.MaxValue, int.MinValue);

        foreach (var currentCount in wordGroups.Select(group => group.Count))
        {
            minValue = currentCount < minValue ? currentCount : minValue;
            maxValue = currentCount > maxValue ? currentCount : maxValue;
        }

        return (minValue, maxValue);
    }

    private IEnumerable<WordTagGroup> GetSortedGroups(IEnumerable<WordTagGroup> wordGroups)
    {
        var sortedGroups = cloudOptions.Sort switch
        {
            SortType.Ascending => wordGroups.OrderBy(group => group.Count),
            SortType.Descending => wordGroups.OrderByDescending(group => group.Count),
            _ => wordGroups
        };

        return sortedGroups.Select(group => group);
    }

    private IFontMeasurer FindFontMeasurer()
    {
        return fontMeasurers.Single(measurer => measurer.Type == cloudOptions.MeasurerType);
    }
}