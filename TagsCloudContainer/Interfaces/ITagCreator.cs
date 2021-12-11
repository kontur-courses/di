using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Interfaces;

public interface ITagCreator
{
    IEnumerable<Tag> CreateTags(IEnumerable<string> words);
}
