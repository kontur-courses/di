using ResultProject;

namespace TagsCloudVisualization.Readers
{
    public interface IFileReader
    {
        TextFormat Format { get; }

        Result<string> ReadFile(string filePath);
    }
}