using System.Drawing;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.WeightCounter;

namespace TagsCloudContainer.TagsConverter;

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
        foreach (var (word, weight) in wordsWithWeights)
        {
            //TODO задавать правильные роазмеры прямоугольников и текста внутри
            tags.Add(new Tag(layouter.PutNextRectangle(new Size(minFontSize + weight, word.Length + weight)),
                word, weight));
        }

        layouter.ClearRectanglesLayout();
        return tags;
    }
}