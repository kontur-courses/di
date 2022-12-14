namespace TagsCloud.Core.WordFilters;

public interface IWordFilter
{
	public bool WordIsValid(string word);
}