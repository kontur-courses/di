namespace TagsCloudContainer.Reading
{
    public class ReadingSettings
    {
        public string InputPath { get; }

        public ReadingSettings(string inputPath)
        {
            InputPath = inputPath;
        }
    }
}