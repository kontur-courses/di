namespace TagsCloudContainer.TextAnalysers;

public interface ITextPreprocessor
{
    public WordDetails[] Preprocess(string text);
}