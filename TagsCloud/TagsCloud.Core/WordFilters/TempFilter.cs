namespace TagsCloud.Core.WordFilters;

public class LengthFilter : IWordFilter
{
	public bool WordIsValid(string words)
	{
		return words.Length > 3;
	}
}