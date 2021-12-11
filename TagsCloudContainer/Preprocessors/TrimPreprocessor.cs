using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class TrimPreprocessor : IPreprocessor
{
    public string Preprocess(string word)
    {
        return word.Trim();
    }
}
