using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class DefaultRectanglesDistributor : IRectanglesDistributor
{
    public List<Rectangle> DistributedRectangles { get; }

    public DefaultRectanglesDistributor(ICloudLayouter layouter, IWordsHandler wordsHandler)
    {
        foreach (var dist in wordsHandler.WordDistribution)
            layouter.PutNextRectangle(CalculateSizeForWord(dist.Key, dist.Value));

        DistributedRectangles = new List<Rectangle>(layouter.PlacedRectangles);
    }

    private Size CalculateSizeForWord(string word, int frequency)
    {
        var font = new Font("Courier New", 10.0F);
        var size = word.MeasureString(font);
        return new Size((int) size.Width * 100, (int) size.Height * 100);
    }
}