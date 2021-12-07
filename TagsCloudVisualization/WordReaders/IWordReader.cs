namespace TagsCloudVisualization.WordReaders
{
    public interface IWordReader
    {
        string Read();
        bool HasWord();
    }
}