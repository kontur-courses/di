using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ExcludeFilter : IFilter
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

    public bool Allows(string word)
    {
        return !wordsToExclude.Contains(word);
    }
}
