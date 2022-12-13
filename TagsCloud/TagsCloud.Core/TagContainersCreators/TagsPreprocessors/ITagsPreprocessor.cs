namespace TagsCloud.Core.TagContainersCreators.TagsPreprocessors;

public interface ITagsPreprocessor
{
    public IEnumerable<Tag> GetTags(int? count);
}