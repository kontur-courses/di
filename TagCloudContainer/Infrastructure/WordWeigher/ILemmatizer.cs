namespace TagCloudContainer.Infrastructure.WordWeigher;

public interface ILemmatizer
{
    bool TryLemmatize(string word, out string lemma);
}