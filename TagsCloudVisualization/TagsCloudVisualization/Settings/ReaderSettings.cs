namespace TagsCloudVisualization.Settings
{
    public class ReaderSettings
    {
        public ReaderSettings(string path, int maxObjectsCount, string badWords)
        {
            this.Path = path;
            this.MaxObjectsCount = maxObjectsCount;
            this.BadWords = badWords;
        }

        public string BadWords { get; }
        public string Path { get; }
        public int MaxObjectsCount { get; }
    }
}