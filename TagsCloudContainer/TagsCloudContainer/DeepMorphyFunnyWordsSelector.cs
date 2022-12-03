using DeepMorphy;

namespace TagsCloudContainer;

public class DeepMorphyFunnyWordsSelector : IFunnyWordsSelector
{
    private static readonly HashSet<string> FunnySpeechPieces = new()
    {
        "числ", "сущ", "прил", "гл", "деепр", "цифра", "рим_цифр", "прич"
    };

    private readonly MorphAnalyzer morphAnalyzer;

    public DeepMorphyFunnyWordsSelector(MorphAnalyzer morphAnalyzer)
    {
        this.morphAnalyzer = morphAnalyzer;
    }

    public IReadOnlyCollection<CloudWord> RecognizeFunnyCloudWords(IReadOnlyCollection<string> allWords)
    {
        var morphInfos = morphAnalyzer.Parse(allWords);

        var funnyMorphInfos = morphInfos.Where(x => FunnySpeechPieces.Contains(x.BestTag["чр"]));

        var funnyNormalizedWords = funnyMorphInfos.Select(x => x.BestTag.HasLemma ? x.BestTag.Lemma : x.Text);

        var funnyWordsLookup = funnyNormalizedWords.GroupBy(x => x);

        var funnyCloudWords = funnyWordsLookup.Select(x => new CloudWord(x.Key, x.Count()));

        return funnyCloudWords.ToArray();
    }
}