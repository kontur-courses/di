using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.TextMeasures;

namespace TagsCloudContainer.CloudGenerators;

public class TagsCloudGenerator: ITagsCloudGenerator
{
    private readonly ITagTextMeasurer tagTextProvider;
    private readonly ICloudLayouterProvider cloudLayouterProvider;

    public TagsCloudGenerator(ICloudLayouterProvider cloudLayouterProvider, ITagTextMeasurer tagTextProvider)
    {
        this.cloudLayouterProvider = cloudLayouterProvider;
        this.tagTextProvider = tagTextProvider;
    }

    public ITagCloud Generate(IEnumerable<WordDetails> wordsDetails)
    {
        var cloudLayouter = cloudLayouterProvider.Get();    
        var sorted = SortWords(wordsDetails);

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

    private IEnumerable<string> SortWords(IEnumerable<WordDetails> wordsDetails)
    {
        return wordsDetails
            .OrderByDescending(x => x.Frequency)
            .Select(x => x.Word);
    }
}