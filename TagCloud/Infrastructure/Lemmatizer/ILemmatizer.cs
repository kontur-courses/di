namespace TagCloud.Infrastructure.Lemmatizer;

public interface ILemmatizer
{
    IEnumerable<Lemma> GetLemmas(IEnumerable<string> words);
}