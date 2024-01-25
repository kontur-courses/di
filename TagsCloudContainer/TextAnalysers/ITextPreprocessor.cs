using TagsCloudContainer.Settings;

namespace TagsCloudContainer.TextAnalysers;

public interface ITextPreprocessor
{
    public CloudData Preprocess(string text);
}