namespace TagsCloud.Core.WordFilters;

public class MinLengthFilter : IWordFilter
{
	private readonly int minLength;

	public MinLengthFilter(int minLength = 1)
	{
		this.minLength = minLength;
	}

	public bool WordIsValid(string word)
	{
		return word.Length >= minLength;
	}
}