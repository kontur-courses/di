namespace TagsCloudVisualization.Settings
{
    public class WordsPreprocessorSettings
    {
        public string[] BoringWords { get; }

        public WordsPreprocessorSettings(string[] boringWords)
        {
            BoringWords = boringWords;
        }
    }
}