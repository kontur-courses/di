namespace TagsCloud.Core.WordFilters;

public class TempFilter : IWordFilter
{
	public IEnumerable<string> Filter(IEnumerable<string> words)
	{
		return words.Select(word => word.ToLower());
	}
}