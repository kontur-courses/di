using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class DefaultRectanglesDistributor : IRectanglesDistributor
{
    private readonly ICloudLayouter layouter;
    private readonly Settings settings;
    private readonly IWordsHandler wordsHandler;

    private Dictionary<string, Rectangle> distrubutedRectangles;

    public DefaultRectanglesDistributor(IWordsHandler wordsHandler,
        ISettingsProvider settingsProvider, ICloudLayouter cloudLayouter)
    {
        settings = settingsProvider.Settings;
        this.wordsHandler = wordsHandler;
        layouter = cloudLayouter;
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
        var sizeAdd = settings.FrequencyGrowth * (frequency - 1);
        var font = new Font(settings.Font.FontFamily, settings.Font.Size + sizeAdd, settings.Font.Style);
        var size = word.MeasureString(font);
        return Size.Ceiling(size);
    }
}