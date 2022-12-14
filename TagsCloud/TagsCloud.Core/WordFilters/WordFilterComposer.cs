namespace TagsCloud.Core.WordFilters;

public class WordFilterComposer : IWordFiltersComposer
{
	private readonly IEnumerable<IWordFilter> filters;

	public WordFilterComposer(IEnumerable<IWordFilter> filters)
	{
		this.filters = filters;
	}

	public IEnumerable<string> Filter(IEnumerable<string> words)
	{
		return words.Where(word => filters.All(filter => filter.WordIsValid(word)));
	}
}