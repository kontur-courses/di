using System.IO;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.FileReaders
{
    public class TextFileReader : IFileReader
    {
        private string fileName { get; set; }

        public TextFileReader(FileSettings fileSettings)
        {
            this.fileName = fileSettings.Filename;
        }

        public string Read()
        {
            return File.ReadAllText(fileName);
        }
    }
}