using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
	public interface IWordFiltersApplyer
	{
		IEnumerable<string> GetWords();
	}
}