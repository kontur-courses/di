using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.DataReader
{
    internal class TxtFileReader : IDataReader
    {
        private readonly string filename;
        public TxtFileReader(string filename)
        {
            this.filename = filename;
        }
        public IEnumerable<string> ReadLines()
        {
            using var reader = new StreamReader(filename);
            while (!reader.EndOfStream) yield return reader.ReadLine();
        }
    }
}