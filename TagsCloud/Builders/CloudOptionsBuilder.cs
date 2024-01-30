using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System.Reflection;
using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloud.Options;
using TagsCloudVisualization;

namespace TagsCloud.Builders;

public class CloudOptionsBuilder
{
    private ColoringStrategy coloringStrategy;
    private Color[] colors;
    private FontFamily fontFamily;
    private ILayout layout;
    private int maxFontSize;
    private MeasurerType measurerType;
    private int minFontSize;
    private SortType sortType;

    public CloudOptionsBuilder SetColoringStrategy(ColoringStrategy strategy)
    {
        coloringStrategy = strategy;
        return this;
    }

    public CloudOptionsBuilder SetMeasurerType(MeasurerType type)
    {
        measurerType = type;
        return this;
    }

    public CloudOptionsBuilder SetColors(HashSet<string> hexes)
    {
        var colorsSet = new HashSet<Color>();

        foreach (var hex in hexes)
            if (Color.TryParseHex(hex, out var color))
                colorsSet.Add(color);

        colors = colorsSet.Count == 0 ? new[] { Color.Black } : colorsSet.ToArray();
        return this;
    }

    public CloudOptionsBuilder SetFontSizeBounds(int lowerBound, int upperBound)
    {
        minFontSize = lowerBound;
        maxFontSize = upperBound;
        return this;
    }

    public CloudOptionsBuilder SetSortingType(SortType type)
    {
        sortType = type;
        return this;
    }

    public CloudOptionsBuilder SetFontFamily(string fontPath)
    {
        var fontCollection = new FontCollection();

        if (File.Exists(fontPath))
        {
            fontCollection.Add(fontPath);
        }
        else
        {
            const string fontName = nameof(TagsCloud) + ".Fonts.Vollkorn-SemiBold.ttf";
            var fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fontName);
            fontCollection.Add(fontStream!);
        }

        fontFamily = fontCollection.Families.First();
        return this;
    }

    public CloudOptionsBuilder SetLayout(
        PointGeneratorType generator,
        PointF center,
        float distanceDelta,
        float angleDelta)
    {
        var pointGenerator = generator switch
        {
            PointGeneratorType.Spiral => new SpiralPointGenerator(distanceDelta, angleDelta),
            _ => throw new NotSupportedException(
                $"{generator} generator type not supported! " +
                $"Candidates are: {string.Join(", ", Enum.GetNames(typeof(PointGeneratorType)))}")
        };

        layout = new Layout(pointGenerator, center);
        return this;
    }

    public ICloudProcessorOptions BuildOptions()
    {
        return new CloudProcessorOptions
        {
            ColoringStrategy = coloringStrategy,
            FontFamily = fontFamily,
            MaxFontSize = maxFontSize,
            MinFontSize = minFontSize,
            MeasurerType = measurerType,
            Sort = sortType,
            Layout = layout,
            Colors = colors
        };
    }
}