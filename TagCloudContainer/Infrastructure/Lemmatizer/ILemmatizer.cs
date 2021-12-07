namespace TagCloudContainer.Infrastructure.Lemmatizer;

public interface ILemmatizer
{
    bool TryLemmatize(string word, out Lemma lemma);
    IEnumerable<Lemma> GetLemmas(IEnumerable<string> words);
}