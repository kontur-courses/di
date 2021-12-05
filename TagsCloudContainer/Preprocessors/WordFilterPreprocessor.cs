using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class WordFilterPreprocessor : IPreprocessor
{
    private HashSet<string> wordsToExclude = new()
    {
        "and",
        "to",
        "the",
        "also",
        "a",
        "of"
    };

    public IEnumerable<string> Preprocess(IEnumerable<string> words)
    {
        foreach (var word in words)
        {
            if (!wordsToExclude.Contains(word))
                yield return word;
        }
    }
}
