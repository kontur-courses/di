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
            var result = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if ((line ?? string.Empty).Contains(' '))
                {
                    throw new ArgumentException("Each line must contain only one word");
                }

                if (line is "")
                {
                    continue;
                }

                result.Add(line);
            }

            return result;
        }
    }
}