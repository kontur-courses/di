namespace TagsCloudContainer.Abstractions;

public interface ITagPacker
{
    IEnumerable<ITag> GetTags();
}