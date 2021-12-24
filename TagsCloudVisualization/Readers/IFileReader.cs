using ResultProject;

namespace TagsCloudVisualization.Readers
{
    internal interface IFileReader
    {
        TextFormat Format { get; }

        Result<string> ReadFile(string filePath);
    }
}