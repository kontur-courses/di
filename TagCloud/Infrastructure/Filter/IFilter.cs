using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloud.Infrastructure.Filter;

public interface IFilter
{
    IEnumerable<string> FilterWords(IEnumerable<string> words);
    IEnumerable<string> FilterWords(IEnumerable<Lemma> lemmas);

    Filter AddCondition(Func<string, bool> filter);

    Filter AddCondition(Func<Lemma, bool> filter);
}