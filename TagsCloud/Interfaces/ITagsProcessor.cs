using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
	public interface ITagsProcessor
	{
		IEnumerable<Tag> GetTags();
	}
}