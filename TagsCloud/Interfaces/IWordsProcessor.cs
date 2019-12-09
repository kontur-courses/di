using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
	public interface IWordsProcessor
	{
		IEnumerable<Word> GetWordsWithFrequencies();
	}
}