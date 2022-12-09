namespace TagsCloud.Core.TagContainersCreators.TagsPreproessors;

public interface ITagsPreprocessor
{
    public IEnumerable<Tag> GetTags(int? count);
}