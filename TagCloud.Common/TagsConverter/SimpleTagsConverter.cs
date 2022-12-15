using System.Drawing;
using TagCloud.Common.Extensions;
using TagCloud.Common.Layouter;
using TagCloud.Common.WeightCounter;

namespace TagCloud.Common.TagsConverter;

public class SimpleTagsConverter : ITagsConverter
{
    private ICloudLayouter layouter;
    private IWeightCounter weightCounter;

    public SimpleTagsConverter(ICloudLayouter layouter, IWeightCounter weightCounter)
    {
        this.layouter = layouter;
        this.weightCounter = weightCounter;
    }

    public IEnumerable<Tag> ConvertToTags(IEnumerable<string> words, int minFontSize)
    {
        var tags = new List<Tag>();
        var wordsWithWeights = weightCounter.CountWeights(words);
        var maxWeight = wordsWithWeights.Values.Max();
        foreach (var (word, weight) in wordsWithWeights)
        {
            var font = new Font("Arial", maxWeight / (maxWeight - weight + 1) + minFontSize);
            var size = CalculateSize(word, font);
            var tag = new Tag(layouter.PutNextRectangle(size), word, font);
            tags.Add(tag);
        }

        layouter.ClearRectanglesLayout();
        return tags;
    }

    private Size CalculateSize(string word, Font font)
    {
        var graphics = Graphics.FromImage(new Bitmap(1, 1));
        var sizeF = graphics.MeasureString(word, font);
        return sizeF.ConvertToSize();
    }
}