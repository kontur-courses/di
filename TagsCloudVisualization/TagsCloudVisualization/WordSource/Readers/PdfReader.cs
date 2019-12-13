using System;
using System.Collections.Generic;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Readers
{
    class PdfReader:IFileReader<string>
    {
        public IEnumerable<string> ReadLines(string path)
        {
            throw new NotImplementedException();
        }
    }
}
