using System.IO;

namespace TagsCloudContainer.FileOpeners
{
    public class TxtReader : IFileReader
    {
        public string ReadFile(string filePath) => File.ReadAllText(filePath);
    }
}