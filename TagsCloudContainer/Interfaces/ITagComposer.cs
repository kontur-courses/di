using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Interfaces;

public interface ITagComposer
{
    IEnumerable<Tag> ComposeTags(IEnumerable<string> words);
}
