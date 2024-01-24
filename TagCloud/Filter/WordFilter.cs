namespace TagCloud.Filter;

public class WordFilter : IFilter
{
    private readonly List<Func<string, bool>> filters = [];

    public IEnumerable<string> FilterWords(IEnumerable<string> words)
    {
        return words.Where(word => filters.All(f => f(word)));
    }

    public IFilter UsingFilter(Func<string, bool> filter)
    {
        filters.Add(filter);

        return this;
    }
}