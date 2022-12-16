using TagCloudPainter.Common;
using TagCloudPainter.Layouters;
using TagCloudPainter.Sizers;

namespace TagCloudPainter.Builders;

public class CloudElementBuilder : ITagCloudElementsBuilder
{
    private readonly ICloudLayouter _cloudLayouter;
    private readonly IWordSizer _wordSizer;

    public CloudElementBuilder(IWordSizer wordSizer, ICloudLayouter cloudLayouter)
    {
        _wordSizer = wordSizer;
        _cloudLayouter = cloudLayouter;
    }

    public IEnumerable<Tag> GetTags(Dictionary<string, int> dict)
    {
        if (dict is null || dict.Count == 0)
            throw new ArgumentNullException();

        var result = new List<Tag>();
        foreach (var (word, count) in dict)
        {
            if (string.IsNullOrWhiteSpace(word) || count < 1)
                throw new ArgumentNullException();

            var size = _wordSizer.GetTagSize(word, count);
            var rectangle = _cloudLayouter.PutNextRectangle(size);
            result.Add(new Tag(word, rectangle, count));
        }

        return result;
    }
}