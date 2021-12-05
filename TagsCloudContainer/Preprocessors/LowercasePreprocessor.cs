using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class LowercasePreprocessor : IPreprocessor
{
    public IEnumerable<string> Preprocess(IEnumerable<string> words)
    {
        foreach (var word in words)
            yield return word.ToLower();
    }
}