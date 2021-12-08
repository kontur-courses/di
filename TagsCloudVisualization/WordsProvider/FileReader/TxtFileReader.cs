using System.IO;

namespace TagsCloudVisualization.WordsProvider.FileReader
{
    public class TxtFileReader : IWordsReader
    {
        public bool CanRead(string extension) => extension == ".txt";

        public string Read(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}