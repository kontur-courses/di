using System.IO;

namespace TagsCloudTextProcessing.Readers
{
    public class TxtTextReader : ITextReader
    {
        public string ReadText(string path) => File.ReadAllText(path);
    }
}