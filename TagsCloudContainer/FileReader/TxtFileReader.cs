using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public IEnumerable<string> Read()
        {
            var lines = File.ReadAllLines(provider.WordsFilePath).Where(str => str != "");
            return lines;
        }
    }
}