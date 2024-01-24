namespace TagCloud.Filter;

public interface IFilter
{
    IEnumerable<string> FilterWords(IEnumerable<string> words);

    IFilter UsingFilter(Func<string, bool> filter);
}