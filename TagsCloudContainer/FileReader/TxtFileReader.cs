using System.IO;

namespace TagsCloudContainer
{
    public class TxtFileReader : IFileReader
    {
        private readonly IFilePathProvider provider;
        private readonly WordsContainer container;

        public TxtFileReader(IFilePathProvider provider, WordsContainer container)
        {
            this.provider = provider;
            this.container = container;
        }

        public string[] Read()
        {
            var lines = File.ReadAllLines(provider.WordsFilePath);
            container.RawWords = lines;
            return lines;
        }
    }
}