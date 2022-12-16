using TagCloudPainter.Common;

namespace TagCloudPainter.Builders;

public interface ITagCloudElementsBuilder
{
    IEnumerable<Tag> GetTags(Dictionary<string, int> dictionary);
}