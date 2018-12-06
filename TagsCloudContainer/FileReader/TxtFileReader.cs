using System.IO;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.FileReader
{
    public class TxtFileReader : IFileReader
    {
        private readonly IFilePathProvider provider;

        public TxtFileReader(IFilePathProvider provider)
        {
            this.provider = provider;
        }

        public string[] Read()
        {
            var lines = File.ReadAllLines(provider.WordsFilePath);
            return lines;
        }
    }
}