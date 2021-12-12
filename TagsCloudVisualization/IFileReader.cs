using System.IO;

namespace TagsCloudVisualization
{
    public interface IFileReader
    {
        bool CanReadFile(FileInfo file);
        string ReadFile(FileInfo file);
    }
}