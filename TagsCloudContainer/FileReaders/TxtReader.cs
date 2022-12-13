using System.IO;
using TagsCloudContainer.FileOpeners;

namespace TagsCloudContainer.FileReaders
{
    public class TxtReader : IFileReader
    {
        public string ReadFile(string filePath) => File.ReadAllText(filePath);
    }
}