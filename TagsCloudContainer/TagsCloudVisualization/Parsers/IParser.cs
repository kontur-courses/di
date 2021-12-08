using System.Collections.Generic;

namespace TagsCloudVisualization.Parsers
{
    public interface IParser
    {
        public IEnumerable<string> ParseWords(string filePath);
    }
}