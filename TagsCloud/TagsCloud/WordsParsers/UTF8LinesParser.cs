using System;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsParsers
{
    public class UTF8LinesParser : IWordsParser
    {
        public string FactorialId => "UTF8LinesParser";

        public string[] ParseFromFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            return File
            .ReadAllLines(filePath, Encoding.UTF8)
            .Where(w => w.Length > 0)
            .ToArray();
        }
    }
}