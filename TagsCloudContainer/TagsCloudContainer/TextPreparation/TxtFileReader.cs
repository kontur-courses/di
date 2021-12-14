using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.TextPreparation
{
    public class TxtFileReader : IFileReader
    {
        public List<string> GetAllWords(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentException();
            }

            using var reader = new StreamReader(filePath);
            var lines = reader.ReadToEnd().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            if (lines.Any(line => (line).Contains(' ')))
            {
                throw new ArgumentException("Each line must contain only one word");
            }

            return lines.Where(line => !string.IsNullOrEmpty(line)).ToList();
        }
    }
}