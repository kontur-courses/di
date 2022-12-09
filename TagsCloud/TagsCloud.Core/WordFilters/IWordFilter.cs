namespace TagsCloud.Core.WordFilters;

public interface IWordFilter
{
    public IEnumerable<string> Filter(IEnumerable<string> words);
}