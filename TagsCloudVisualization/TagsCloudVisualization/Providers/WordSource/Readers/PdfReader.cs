using System;
using System.Collections.Generic;
using TagsCloudVisualization.Providers.WordSource.Interfaces;
using TagsCloudVisualization.Results;

namespace TagsCloudVisualization.Providers.WordSource.Readers
{
    internal class PdfReader : IFileReader
    {
        public Result<List<string>> ReadLines(string path)
        {
            throw new NotImplementedException();
        }
    }
}