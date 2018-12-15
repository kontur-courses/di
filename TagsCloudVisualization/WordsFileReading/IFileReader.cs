using System.Collections.Generic;

namespace TagsCloudVisualization.WordsFileReading
{
    public interface IFileReader
    {
        string ReadText(string fileName);
        string[] SupportedTypes();
    }
}
