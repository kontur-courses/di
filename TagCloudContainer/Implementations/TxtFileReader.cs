using System.Collections.Generic;
using System.IO;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    public class TxtFileReader : IWordProvider
    {
        private readonly string txtFileName;

        public TxtFileReader(string txtFileName)
        {
            this.txtFileName = txtFileName;
        }

        public IEnumerable<string> GetWords()
        {
            return File.Exists(txtFileName) ? File.ReadLines(txtFileName) : new List<string>();
        }
    }
}