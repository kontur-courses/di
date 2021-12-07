using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Parsers
{
    public interface IParser
    {
        public IEnumerable<string> ParseWords(string filePath);
    }
}