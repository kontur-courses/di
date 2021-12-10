using DeepMorphy;

namespace TagCloud.Infrastructure.Lemmatizer;

public class RussianLemmatizer : ILemmatizer
{
    private static readonly IReadOnlyDictionary<string, PartOfSpeech> PartOfSpeeches = new Dictionary<string, PartOfSpeech>
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
            if (string.IsNullOrEmpty(word))
                continue;

            var (isLemmatized, lemma) = TryLemmatize(word.ToLowerInvariant());

            if (isLemmatized)
                yield return lemma;
        }
    }

    private (bool, Lemma) TryLemmatize(string word)
    {
        var morphInfo = morph.Parse(word).First();

        if (!morphInfo.BestTag.HasLemma)
            return (false, new Lemma(word, PartOfSpeech.Unknown));

        var partOfSpeech = PartOfSpeeches.ContainsKey(morphInfo.BestTag["post"])
            ? PartOfSpeeches[morphInfo.BestTag["post"]]
            : PartOfSpeech.Unknown;

        return (true, new Lemma(morphInfo.BestTag.Lemma, partOfSpeech));
    }
}