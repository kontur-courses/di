using System.Drawing;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Options;
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

    public IEnumerable<Tag> ConvertToTags(IEnumerable<string> words, VisualizationOptions options)
    {
        var tags = new List<Tag>();
        var wordsWithWeights = weightCounter.CountWeights(words);
        var maxWeight = wordsWithWeights.Values.Max();
        foreach (var (word, weight) in wordsWithWeights)
        {
            var font = new Font("Arial", maxWeight/(maxWeight-weight + 1) + options.MinFontSize);
            //TODO задавать правильные роазмеры прямоугольников и текста 
            var size = new Size((int) font.Size * word.Length, font.Height);
            var tag = new Tag(layouter.PutNextRectangle(size), word, font);
            tags.Add(tag);
        }

        layouter.ClearRectanglesLayout();
        return tags;
    }
    
}