using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloud.App
{
    public class TxtFileReader : IFileReader
    {
        public HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"txt"};

        public string[] ReadLines(string fileName)
        {
            var fileType = fileName.Split('.')[^1];
            if (!AvailableFileTypes.Contains(fileType))
                throw new ArgumentException($"Incorrect type {fileType}");
            return File.ReadAllLines(fileName);
        }
    }
}