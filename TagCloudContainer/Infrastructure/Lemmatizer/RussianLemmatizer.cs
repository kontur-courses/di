using DeepMorphy;

namespace TagCloudContainer.Infrastructure.Lemmatizer;

public class RussianLemmatizer : ILemmatizer
{
    public static readonly IReadOnlyDictionary<string, PartOfSpeech> PartOfSpeeches = new Dictionary<string, PartOfSpeech>
    {
        { "noun", PartOfSpeech.Noun },
        { "adjf", PartOfSpeech.Adjective },
        { "adjs", PartOfSpeech.ShortAdjective },
        { "verb", PartOfSpeech.Verb },
        { "infn", PartOfSpeech.Verb },
        { "prtf", PartOfSpeech.Participle },
        { "prts", PartOfSpeech.Participle },
        { "grnd", PartOfSpeech.Gerund },
        { "advb", PartOfSpeech.Adverb },
        { "npro", PartOfSpeech.Pronoun },
        { "prep", PartOfSpeech.Preposition },
        { "conj", PartOfSpeech.Conjunction },
        { "prcl", PartOfSpeech.Particle },
        { "intj", PartOfSpeech.Interjection },
        { "numb", PartOfSpeech.Numeral }
    };

    private readonly MorphAnalyzer morph = new(true, true, false);

    public IEnumerable<Lemma> GetLemmas(IEnumerable<string> words)
    {
        foreach (var word in words)
        {
            if (TryLemmatize(word, out var lemma))
                yield return lemma;
        }
    }

    public bool TryLemmatize(string word, out Lemma lemma)
    {
        lemma = null;

        if (string.IsNullOrEmpty(word))
            return false;

        var morphInfo = morph.Parse(word).First();

        if (!morphInfo.BestTag.HasLemma)
        {
            lemma = new Lemma(word, PartOfSpeech.Unknown);
            return false;
        }

        lemma = new Lemma(morphInfo.BestTag.Lemma, PartOfSpeeches.TryGetValue(morphInfo.BestTag["post"], out var partOfSpeech) ? partOfSpeech : PartOfSpeech.Unknown);
        return true;
    }
}