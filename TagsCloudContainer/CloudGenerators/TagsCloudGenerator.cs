using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.TextMeasures;

namespace TagsCloudContainer.CloudGenerators;

public class TagsCloudGenerator: ITagsCloudGenerator
{
    private readonly ITagTextMeasurer tagTextProvider;
    private ICloudLayouterProvider cloudLayouterProvider;

    public TagsCloudGenerator(ICloudLayouterProvider cloudLayouterProvider, ITagTextMeasurer tagTextProvider)
    {
        this.cloudLayouterProvider = cloudLayouterProvider;
        this.tagTextProvider = tagTextProvider;
    }

    public ITagCloud Generate(AnalyzeData analyzeData)
    {
        var cloudLayouter = cloudLayouterProvider.Get();    
        var sorted = SortWords(analyzeData);

        var tags = new List<Tag>();
        foreach (var word in sorted)
        {
            var size = tagTextProvider.Measure(word);
            var rect = cloudLayouter.PutNextRectangle(size);
            tags.Add(new Tag(rect, word));
        }

        return new TagsCloud
        {
            Tags = tags
        };
    }

    private IEnumerable<string> SortWords(AnalyzeData analyzeData)
    {
        return analyzeData.WordData
            .OrderByDescending(x => x.Frequency)
            .Select(x => x.Word);
    }
}