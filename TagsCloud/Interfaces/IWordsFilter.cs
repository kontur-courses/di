using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
	public interface IWordsFilter
	{
		IEnumerable<string> GetWords();
	}
}