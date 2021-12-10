using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Parsers
{
    public class TxtParser : IParser
    {
        public IEnumerable<string> ParseWords(string filePath)
        {
            using var reader = new StreamReader(filePath);

            return reader.ReadToEnd()
                .Split(new[] {Environment.NewLine, " "}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}