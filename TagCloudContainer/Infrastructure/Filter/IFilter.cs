using TagCloudContainer.Infrastructure.Lemmatizer;

namespace TagCloudContainer.Infrastructure.Filter;

public interface IFilter
{
    IEnumerable<string> FilterWords(IEnumerable<string> words);
    IEnumerable<string> FilterWords(IEnumerable<Lemma> lemmas);

    Filter AddCondition(Func<string, bool> filter);

    Filter AddCondition(Func<Lemma, bool> filter);
}