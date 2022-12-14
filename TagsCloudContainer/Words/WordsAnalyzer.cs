using DeepMorphy;
using TagsCloudContainer.WordsInterfaces;

namespace TagsCloudContainer;

public class WordsAnalyzer : IWordsAnalyzer
{
    private readonly HashSet<string> _boringWords = new();

    private readonly HashSet<string> _spPartToIgnore = new()
    {
        "мест", "предл"
    };

    public List<string> Analyze(List<string> words, HashSet<string> boringWords, HashSet<string> spPartToIgnore)
    {
        _boringWords.UnionWith(boringWords);
        _spPartToIgnore.UnionWith(spPartToIgnore);

        var morph = new MorphAnalyzer(true);
        var result = new List<string>();

        foreach (var word in words)
        {
            var wordData = morph.Parse(word).ToArray()[0];
            var spPart = wordData.BestTag.GramsDic["чр"];
            if (!_spPartToIgnore.Contains(spPart) && !_boringWords.Contains(word))
                result.Add(wordData.BestTag.Lemma);
        }

        return result;
    }
}