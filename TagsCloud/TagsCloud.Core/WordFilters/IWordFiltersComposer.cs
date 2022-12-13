namespace TagsCloud.Core.WordFilters;

public interface IWordFiltersComposer
{
	public IEnumerable<string> Filter(IEnumerable<string> words);
}