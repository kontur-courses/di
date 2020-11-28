using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.FileReader
{
    internal class TxtFileReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string filename)
        {
            using var reader = new StreamReader(filename);
            while (!reader.EndOfStream) yield return reader.ReadLine();
        }
    }
}