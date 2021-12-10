using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults;

public class LowerNormalizer : IWordNormalizer
{
    public string Normalize(string word)
    {
        return word.ToLower();
    }
}
