using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.WordProcessing
{
    public class TxtReader : IWordProvider
    {
        private readonly string filePath;

        public TxtReader(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(filePath);
        }
    }
}