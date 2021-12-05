using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class TrimPreprocessor : IPreprocessor
{
    public IEnumerable<string> Preprocess(IEnumerable<string> words)
    {
        foreach (var word in words)
            yield return word.Trim();
    }
}
