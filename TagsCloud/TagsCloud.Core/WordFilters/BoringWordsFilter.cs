using TagsCloud.Core.WordReaders;

namespace TagsCloud.Core.WordFilters;

public class BoringWordsFilter : IWordFilter
{
	private readonly HashSet<string> boringWords;

	public BoringWordsFilter(IWordReader boringWordsReader)
	{
		boringWords = new HashSet<string>();

		foreach (var word in boringWordsReader.ReadWords()) boringWords.Add(word);
	}

	public bool WordIsValid(string word)
	{
		return !boringWords.Contains(word.ToLower());
	}
}