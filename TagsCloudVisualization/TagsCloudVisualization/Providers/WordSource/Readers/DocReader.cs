using System;
using System.Collections.Generic;
using TagsCloudVisualization.Providers.WordSource.Interfaces;
using TagsCloudVisualization.Results;

namespace TagsCloudVisualization.Providers.WordSource.Readers
{
    public class DocReader : IFileReader
    {
        public Result<List<string>> ReadLines(string path)
        {
            throw new NotImplementedException();
        }
    }
}