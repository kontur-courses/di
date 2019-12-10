using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.WordPreprocessors
{
    public interface IWordPreprocessor
    {
        ProcessedWord[] WordPreprocessing(string[] text);
    }
}
