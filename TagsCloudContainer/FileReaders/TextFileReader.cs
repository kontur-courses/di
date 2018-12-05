using System.IO;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.FileReaders
{
    public class TextFileReader : IFileReader
    {
        public TextFileReader(FileSettings fileSettings)
        {
            fileName = fileSettings.Filename;
        }

        private string fileName { get; }

        public string Read()
        {
            return File.ReadAllText(fileName);
        }
    }
}