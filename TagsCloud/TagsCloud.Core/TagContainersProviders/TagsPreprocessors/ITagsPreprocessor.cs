namespace TagsCloud.Core.TagContainersProviders.TagsPreprocessors;

public interface ITagsPreprocessor
{
    public IEnumerable<Tag> GetTags(int? count);
}