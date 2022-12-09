namespace TagsCloud.Core.TagContainersCreators;

public interface ITagContainersCreator
{
	public IEnumerable<TagContainer> Create(int? count = null);
}