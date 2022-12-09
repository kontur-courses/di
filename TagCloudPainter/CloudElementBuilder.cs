using TagCloudPainter.Common;
using TagCloudPainter.Interfaces;

namespace TagCloudPainter;

public class CloudElementBuilder : ITagCloudElementsBuilder
{
    private readonly ICloudLayouter _cloudLayouter;
    private readonly ITagParser _tagParser;
    private readonly IWordSizer _wordSizer;

    public CloudElementBuilder(ITagParser tagParser, IWordSizer wordSizer, ICloudLayouter cloudLayouter)
    {
        _tagParser = tagParser;
        _wordSizer = wordSizer;
        _cloudLayouter = cloudLayouter;
    }

    public IEnumerable<Tag> GetTags(string path)
    {
        var result = new List<Tag>();
        var dict = _tagParser.ParseTags(path);
        foreach (var tag in dict.Keys)
        {
            var size = _wordSizer.GetTagSize(tag, dict[tag]);
            var rectangle = _cloudLayouter.PutNextRectangle(size);
            result.Add(new Tag(tag, rectangle));
        }

        return result;
    }
}