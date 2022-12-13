using System.IO;

namespace TagsCloudVisualization.FileReaders
{
    public interface IFileReader
    {
        public string FilePath { get; }

        public bool TryReadAllText(out string text);
    }
}
