using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Builders;

public class CloudTagListBuilder : CloudTagListBuilderBase
{
    private const int minFontSize = 30;
    private const int maxFontSize = 100;

    private readonly Dictionary<CloudTag, int> frequencyStatistics;

    public CloudTagListBuilder(IFactoryOptions options, List<WordToStatus> words) : base(options, words)
    {
        frequencyStatistics = words
                              .Where(word => !word.IsTrash)
                              .Select(word => word.Word)
                              .GroupBy(word => new CloudTag { InnerText = word })
                              .ToDictionary(
                                  group => group.Key,
                                  group => group.Count());
    }

    public override CloudTagListBuilderBase AdjustFonts()
    {
        var maxFrequency = frequencyStatistics.Values.Max();

        foreach (var pair in frequencyStatistics)
        {
            var fontSize = minFontSize + (float)pair.Value / maxFrequency * (maxFontSize - minFontSize);
            pair.Key.TextFont = options.FontFamily.CreateFont((int)fontSize, FontStyle.Regular);
        }

        return this;
    }

    public override CloudTagListBuilderBase AdjustColors()
    {
        options.Colorizer.Colorize(frequencyStatistics);
        return this;
    }

    public override CloudTagListBuilderBase AdjustPositions()
    {
        var layout = options.Layout;

        foreach (var pair in frequencyStatistics)
        {
            var textOptions = new TextOptions(pair.Key.TextFont);
            var fontRect = TextMeasurer.MeasureAdvance(pair.Key.InnerText, textOptions);
            var rectangle = layout.PutNextRectangle(new SizeF(fontRect.Width, fontRect.Height));

            if (Math.Abs(fontRect.Width - rectangle.Width) > 1e-3)
                pair.Key.IsRotated = true;

            pair.Key.BoundRectangle = rectangle;
        }

        return this;
    }

    public override List<CloudTag> Build()
    {
        return frequencyStatistics.Keys.ToList();
    }
}