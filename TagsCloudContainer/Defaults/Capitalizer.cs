using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Capitalizer : IWordNormalizer
{
    public string Normalize(string word)
    {
        return $"{char.ToUpper(word[0])}{word[1..]}";
    }
}
