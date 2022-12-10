using System.Collections.Generic;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public interface IParser
    {
        public string FileType { get; }

        public IEnumerable<string> WordParse(string path);
    }
}