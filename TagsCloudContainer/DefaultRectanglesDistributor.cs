using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class DefaultRectanglesDistributor : IRectanglesDistributor
{
    private readonly Settings settings;

    public DefaultRectanglesDistributor(ICloudLayouter layouter, IWordsHandler wordsHandler,
        ISettingsProvider settingsProvider)
    {
        settings = settingsProvider.Settings;
        DistributedRectangles = new Dictionary<string, Rectangle>();
        foreach (var dist in wordsHandler.WordDistribution)
            DistributedRectangles.Add(dist.Key, layouter.PutNextRectangle(CalculateSizeForWord(dist.Key, dist.Value)));
    }

    public Dictionary<string, Rectangle> DistributedRectangles { get; }

    private Size CalculateSizeForWord(string word, int frequency)
    {
        var size = word.MeasureString(settings.Font);
        var ratio = MathF.Pow(settings.FrequencyRatio, frequency - 1);
        size.Height *= ratio;
        size.Width *= ratio;
        return Size.Ceiling(size);
    }
}