using System.IO;

namespace TagsCloudVisualization.WordsFileReading
{
    public interface IFileReader
    {
        TextReader ReadText(string fileName);
        string[] SupportedTypes();
    }
}
