namespace TagsCloud.Core.TagContainersCreators;

public interface ITagContainersProvider
{
	public IEnumerable<TagContainer> GetContainers(int? count = null);
}