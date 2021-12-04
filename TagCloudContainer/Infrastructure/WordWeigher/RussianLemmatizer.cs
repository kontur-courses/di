using DeepMorphy;

namespace TagCloudContainer.Infrastructure.WordWeigher;

public class RussianLemmatizer : ILemmatizer
{
    public static readonly IReadOnlySet<string> AuxiliaryParts = new HashSet<string>() { "npro", "prep", "conj", "intj", "punct" };
    private readonly MorphAnalyzer morph = new(true, true, false);

    public bool TryLemmatize(string word, out string lemma)
    {
        var morphInfo = morph.Parse(word).First();

        if (morphInfo.BestTag.HasLemma && !AuxiliaryParts.Contains(morphInfo["post"].BestGramKey))
        {
            lemma = morphInfo.BestTag.Lemma;
            return true;
        }

        lemma = null;
        return false;
    }
}