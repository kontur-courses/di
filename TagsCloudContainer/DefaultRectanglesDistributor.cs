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
        foreach (var dist in wordsHandler.WordDistribution)
            layouter.PutNextRectangle(CalculateSizeForWord(dist.Key, dist.Value));

        DistributedRectangles = new List<Rectangle>(layouter.PlacedRectangles);
    }

    public List<Rectangle> DistributedRectangles { get; }

    private Size CalculateSizeForWord(string word, int frequency)
    {
        var size = word.MeasureString(settings.Font);
        var ratio = MathF.Pow(settings.FrequencyRatio, frequency);
        return new Size((int) (size.Width * ratio), (int) (size.Height * ratio));
    }
}