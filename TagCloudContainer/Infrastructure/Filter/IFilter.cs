using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloud.Infrastructure.Filter;

public interface IFilter
{
    IEnumerable<string> FilterWords(IEnumerable<string> words);
    IEnumerable<string> FilterWords(IEnumerable<Lemma> lemmas);

    TagCloud.Infrastructure.Filter.Filter AddCondition(Func<string, bool> filter);

    TagCloud.Infrastructure.Filter.Filter AddCondition(Func<Lemma, bool> filter);
}