using DeepMorphy;
using TagCloud.Abstractions;

namespace TagCloud;

public class MorphWordsProcessor : IWordsProcessor
{
    private readonly IEnumerable<string> boredPartsOfSpeech;
    private readonly MorphAnalyzer morph;

    public MorphWordsProcessor(IEnumerable<string> boredPartsOfSpeech)
    {
        this.boredPartsOfSpeech = boredPartsOfSpeech;
        morph = new MorphAnalyzer(true);
    }

    public IEnumerable<string> Process(IEnumerable<string> words)
    {
        var infos = morph.Parse(words).ToArray();
        words = infos
            .Where(i => i.Tags.Any())
            .Where(i => !boredPartsOfSpeech.Any(b => i.BestTag.Has(b)))
            .Select(i => i.BestTag.Lemma);
        return words;
    }
}