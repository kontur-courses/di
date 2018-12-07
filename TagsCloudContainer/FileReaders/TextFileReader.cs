using System.IO;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.FileReaders
{
    public class TextFileReader : IFileReader
    {
        private readonly FileSettings fileSettings;

        public TextFileReader(FileSettings fileSettings)
        {
            this.fileSettings = fileSettings;
        }

        public string Read()
        {
            return File.ReadAllText(fileSettings.Filename);
        }
    }
}