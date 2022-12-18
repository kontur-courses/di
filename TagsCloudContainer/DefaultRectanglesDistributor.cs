using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class DefaultRectanglesDistributor : IRectanglesDistributor
{
    private readonly ICloudLayouter layouter;
    private readonly Settings settings;
    private readonly Dictionary<string, int> wordDistribution;

    private Result<Dictionary<string, Rectangle>> distrubutedRectangles;

    public DefaultRectanglesDistributor(IWordsHandler wordDistribution,
        ISettingsProvider settingsProvider, ICloudLayouter cloudLayouter)
    {
        settings = settingsProvider.Settings;
        var wordDistributionResult = wordDistribution.WordDistribution;
        if (wordDistributionResult.Successful) this.wordDistribution = wordDistributionResult.Value;
        else distrubutedRectangles = new Result<Dictionary<string, Rectangle>>(wordDistributionResult.Exception);
            layouter = cloudLayouter;
    }

    public Result<Dictionary<string, Rectangle>> DistributedRectangles
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
        var distributedRectangles = new Dictionary<string, Rectangle>();
        foreach (var dist in wordDistribution)
            distributedRectangles.Add(dist.Key, layouter.PutNextRectangle(CalculateSizeForWord(dist.Key, dist.Value)));
        this.distrubutedRectangles = new Result<Dictionary<string, Rectangle>>(distributedRectangles);
    }

    private Size CalculateSizeForWord(string word, int frequency)
    {
        var sizeAdd = settings.FrequencyGrowth * (frequency - 1);
        var font = new Font(settings.Font.FontFamily, settings.Font.Size + sizeAdd, settings.Font.Style);
        var size = word.MeasureString(font);
        return Size.Ceiling(size);
    }
}