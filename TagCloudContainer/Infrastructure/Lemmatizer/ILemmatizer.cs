namespace TagCloud.Infrastructure.Lemmatizer;

public interface ILemmatizer
{
    (bool, Lemma) TryLemmatize(string word);
    IEnumerable<Lemma> GetLemmas(IEnumerable<string> words);
}