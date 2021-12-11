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

    public string Preprocess(string word)
    {
        if (!wordsToExclude.Contains(word))
            return word;
        return string.Empty;
    }
}
