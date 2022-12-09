using TagsCloud.Core.Interfaces;

namespace TagsCloud.Core;

public class TempReader : IWordReader
{
	public IEnumerable<string> ReadWords()
	{
		return new List<string>() { "a", "ab", "ab", "aaa", "aaa", "abc", "bbbbbb", "ccc", "ccc", "dd", "dd", "dd", "dd", "dd", "123" };
	}
}