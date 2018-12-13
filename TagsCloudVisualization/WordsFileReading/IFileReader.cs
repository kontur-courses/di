using System.Collections.Generic;


namespace TagsCloudVisualization.WordsFileReading
{
    public interface IFileReader
    {
        IEnumerable<string> ReadAllWords(string fileName);
        string[] SupportedTypes();
    }
}
