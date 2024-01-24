using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Factories;

public class CloudTagFactory : CloudTagFactoryBase
{
    private const int minFontSize = 10;
    private const int maxFontSize = 75;

    private readonly Dictionary<CloudTag, int> frequencyStatistics;

    public CloudTagFactory(CloudTagFactoryOptions options, List<string> words)
        : base(options, words)
    {
        frequencyStatistics = words
            .GroupBy(word => new CloudTag { InnerText = word })
            .ToDictionary(group => group.Key, group => group.Count());
    }

    public override CloudTagFactoryBase AdjustFonts()
    {
        var maxFrequency = frequencyStatistics.Values.Max();

        foreach (var pair in frequencyStatistics)
        {
            var fontSize = minFontSize + (float)pair.Value / maxFrequency * (maxFontSize - minFontSize);
            pair.Key.TextFont = options.FontFamily.CreateFont((int)fontSize, FontStyle.Regular);
        }

        return this;
    }

    public override CloudTagFactoryBase AdjustColors()
    {
        options.Colorizer.Colorize(frequencyStatistics);
        return this;
    }

    public override CloudTagFactoryBase AdjustPositions()
    {
        var layout = new Layout(options.LayoutOptions.Function, options.LayoutOptions.Center);

        foreach (var pair in frequencyStatistics)
        {
            var textOptions = new TextOptions(pair.Key.TextFont);
            var fontRect = TextMeasurer.MeasureAdvance(pair.Key.InnerText, textOptions);
            pair.Key.BoundRectangle = layout.PutNextRectangle(new SizeF(fontRect.Width, fontRect.Height));
        }

        return this;
    }

    public override List<CloudTag> Build()
    {
        return frequencyStatistics.Keys.ToList();
    }
}