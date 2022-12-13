namespace TagsCloud.Core.TagContainersProviders;

public interface ITagContainersProvider
{
	public IEnumerable<TagContainer> GetContainers(int? count = null);
}