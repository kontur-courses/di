using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class SimpleWordsReader : IWordsReader
    {
        private string FileName { get; }

        public SimpleWordsReader(IConfiguration configuration)
        {
            FileName = configuration.PathToWordsFile;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadLines(FileName);
        }
    }
}