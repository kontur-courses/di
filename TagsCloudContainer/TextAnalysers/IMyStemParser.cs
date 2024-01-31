namespace TagsCloudContainer.TextAnalysers;

public interface IMyStemParser
{
    public bool CanParse(string wordInfo);

    public WordDetails Parse(string wordInfo);
}