namespace TagsCloudVisualization.Settings
{
    public class FontSettings
    {
        public int MaxSize { get; }
        public string Family { get; }

        public FontSettings(int maxSize, string family)
        {
            MaxSize = maxSize;
            Family = family;
        }
    }
}