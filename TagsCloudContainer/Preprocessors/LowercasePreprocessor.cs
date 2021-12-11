using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class LowercasePreprocessor : IPreprocessor
{
    public string Preprocess(string word)
    {
        return word.ToLower();
    }
}