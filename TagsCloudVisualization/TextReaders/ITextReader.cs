namespace TagsCloudVisualization.TextReaders
{
    public interface ITextReader
    {
        public string Path { get; }
        public string[] Read();
    }
}
