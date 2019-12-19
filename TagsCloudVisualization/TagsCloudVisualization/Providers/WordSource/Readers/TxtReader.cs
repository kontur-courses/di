using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Readers
{
    internal class TxtReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException($"File not found {path}");
            return File.ReadLines(path, Encoding.UTF8);
        }
    }
}