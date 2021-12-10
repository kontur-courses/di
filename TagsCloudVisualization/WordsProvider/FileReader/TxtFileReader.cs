using System.IO;

namespace TagsCloudVisualization.WordsProvider.FileReader
{
    public class TxtFileReader : IWordsReader
    {
        public bool IsSupportedFileExtension(string extension) => extension == ".txt";

        public string GetFileContent(string path) => File.ReadAllText(path);
    }
}