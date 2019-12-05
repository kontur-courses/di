using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Parsing
{
    public class TxtParser : IFileParser
    {
        public IEnumerable<string> ParseFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                yield return reader.ReadLine();
            }
        }
    }
}