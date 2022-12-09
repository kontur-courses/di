using TagCloudPainter.Common;

namespace TagCloudPainter.Interfaces;

public interface ITagCloudElementsBuilder
{
    IEnumerable<Tag> GetTags(string path);
}