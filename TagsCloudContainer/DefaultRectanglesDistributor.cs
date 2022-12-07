using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class DefaultRectanglesDistributor : IRectanglesDistributor
{
    private readonly ICloudLayouter layouter;
    private readonly Settings settings;
    private readonly IWordsHandler wordsHandler;

    private Dictionary<string, Rectangle> distrubutedRectangles;

    public DefaultRectanglesDistributor(ICloudLayouter layouter, IWordsHandler wordsHandler,
        ISettingsProvider settingsProvider)
    {
        settings = settingsProvider.Settings;
        this.wordsHandler = wordsHandler;
        this.layouter = layouter;
    }

    public Dictionary<string, Rectangle> DistributedRectangles
    {
        get
        {
            if (distrubutedRectangles == null) Distribute();
            return distrubutedRectangles;
        }
        private set => distrubutedRectangles = value;
    }

    private void Distribute()
    {
        DistributedRectangles = new Dictionary<string, Rectangle>();
        foreach (var dist in wordsHandler.WordDistribution)
            DistributedRectangles.Add(dist.Key, layouter.PutNextRectangle(CalculateSizeForWord(dist.Key, dist.Value)));
    }

    private Size CalculateSizeForWord(string word, int frequency)
    {
        var size = word.MeasureString(settings.Font);
        var ratio = MathF.Pow(settings.FrequencyRatio, frequency - 1);
        size.Height *= ratio;
        size.Width *= ratio;
        return Size.Ceiling(size);
    }
}