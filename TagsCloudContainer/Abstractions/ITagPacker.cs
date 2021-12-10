using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface ITagPacker : IService
{
    IEnumerable<ITag> GetTags();
}