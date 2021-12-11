namespace TagsCloudVisualization.Readers
{
    public interface IFileReader
    {
        TextFormat Format { get; }

        string ReadFile(string filePath);
    }
}