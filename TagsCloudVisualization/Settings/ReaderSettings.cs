namespace TagsCloudVisualization.Settings
{
    public class ReaderSettings
    {
        public string FileWithWords { get; }

        public ReaderSettings(string pathToFileWithWords)
        {
            FileWithWords = pathToFileWithWords;
        }
    }
}