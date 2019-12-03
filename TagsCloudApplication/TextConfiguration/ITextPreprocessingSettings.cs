namespace TextConfiguration
{
    public interface ITextPreprocessingSettings
    {
        bool TryPreprocessWord(string word, out string preprocessedWord);
    }
}
