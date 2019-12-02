using System;
using System.IO;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsParsers
{
    public class DefaultWordsParser : IWordsParser
    {
        public string[] ParseFromFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            return File
            .ReadAllLines(filePath)
            .Where(w => w.Length > 0)
            .ToArray();
        }
    }
}