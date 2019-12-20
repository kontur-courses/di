using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudVisualization.Providers.WordSource.Interfaces;
using TagsCloudVisualization.Results;

namespace TagsCloudVisualization.Providers.WordSource.Readers
{
    internal class TxtReader : IFileReader
    {
        public Result<List<string>> ReadLines(string path)
        {
            if (!File.Exists(path))
                return Result.Fail<List<string>>($"File not found on path : {path}");
            return Result.Of(() => File.ReadLines(path, Encoding.UTF8).ToList());
        }
    }
}