using System;
using System.Collections.Generic;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Readers
{
    public class DocReader : IFileReader<string>
    {
        public IEnumerable<string> ReadLines(string path)
        {
            throw new NotImplementedException();
        }
    }
}