using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloud.Infrastructure.Filter;

public class Filter : IFilter
{
    private readonly List<Func<Lemma, bool>> lemmaFilters;
    private readonly List<Func<string, bool>> stringFilters;

    public Filter() : this(new List<Func<string, bool>>(), new List<Func<Lemma, bool>>()) { }
    public Filter(List<Func<string, bool>> stringFilters) : this(stringFilters, new List<Func<Lemma, bool>>()) { }
    public Filter(List<Func<Lemma, bool>> lemmaFilters) : this(new List<Func<string, bool>>(), lemmaFilters) { }

    public Filter(List<Func<string, bool>> stringFilters, List<Func<Lemma, bool>> lemmaFilters)
    {
        this.stringFilters = stringFilters;
        this.lemmaFilters = lemmaFilters;
    }

    public IEnumerable<string> FilterWords(IEnumerable<string> words)
    {
        return words.Where(word => stringFilters.All(x => x.Invoke(word)));
    }

    public IEnumerable<string> FilterWords(IEnumerable<Lemma> lemmas)
    {
        return lemmas
            .Where(lemma => stringFilters.All(x => x.Invoke(lemma.Word)) && lemmaFilters.All(x => x.Invoke(lemma)))
            .Select(x => x.Word);
    }

    public Filter AddCondition(Func<string, bool> filter)
    {
        stringFilters.Add(filter);
        return this;
    }

    public Filter AddCondition(Func<Lemma, bool> filter)
    {
        lemmaFilters.Add(filter);
        return this;
    }
}