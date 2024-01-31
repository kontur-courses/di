namespace TagsCloudContainer.TextAnalysers;

public interface ITextPreprocessor
{
    public AnalyzeData Preprocess(string text);
}