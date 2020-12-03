using System.IO;

namespace TagsCloud.App
{
    public class FileReader
    {
        private readonly FileReaderProvider fileReaderProvider;

        public FileReader(FileReaderProvider fileReaderProvider)
        {
            this.fileReaderProvider = fileReaderProvider;
        }

        public string[] ReadLines(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return fileReaderProvider.GetFileReader(extension).ReadAllLines(filePath);
        }
    }
}