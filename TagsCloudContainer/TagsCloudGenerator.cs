using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer;

public class TagsCloudGenerator: ITagsCloudGenerator
{
    private readonly ICloudLayouter cloudLayouter;
    private readonly CloudData cloudData;
    private readonly FontSizeProvider fontProvider;
    
    public TagsCloudGenerator(CloudData cloudData, FontSizeProvider fontProvider, ICloudLayouter cloudLayouter)
    {
        this.cloudData = cloudData;
        this.fontProvider = fontProvider;
        this.cloudLayouter = cloudLayouter;
    }

    public IEnumerable<Tag> Generate()
    {
        var sorted = SortWords();

        var tags = new List<Tag>();
        foreach (var word in sorted)
        {
            var size = fontProvider.GetSizeByWord(word);
            var rect = cloudLayouter.PutNextRectangle(size);
            tags.Add(new Tag(rect, word));
        }

        return tags;
    }

    private IEnumerable<string> SortWords()
    {
        return cloudData.WordsFrequency
            .OrderByDescending(pair => pair.Value)
            .Select(pair => pair.Key);
    }
}