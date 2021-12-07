using TagCloudContainer.Infrastructure.Lemmatizer;

namespace TagCloudContainer.Infrastructure.Filter;

static class AuxiliaryPartOfSpechCondition
{
    public static readonly IReadOnlySet<PartOfSpeech> AuxiliaryParts = new HashSet<PartOfSpeech> 
        { 
            PartOfSpeech.Pronoun, 
            PartOfSpeech.Preposition, 
            PartOfSpeech.Conjunction, 
            PartOfSpeech.Interjection
        };

    public static bool Filter(Lemma lemma)
    {
        return !AuxiliaryParts.Contains(lemma.PartOfSpeech);
    }
}